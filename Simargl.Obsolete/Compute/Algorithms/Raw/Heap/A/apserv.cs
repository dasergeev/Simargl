#pragma warning disable CS8618
#pragma warning disable CS0162
#pragma warning disable CS8600
#pragma warning disable CS8631
#pragma warning disable CS8602
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public partial class apserv
{

    /*************************************************************************
    APSERV overrides
    *************************************************************************/
#if ALGLIB_NO_FAST_KERNELS == false
    /*************************************************************************
    Maximum concurrency on given system, with given compilation settings
    *************************************************************************/
    public static int maxconcurrency(xparams _params)
    {
        return System.Environment.ProcessorCount;
    }
#endif


    /*************************************************************************
    Buffers for internal functions which need buffers:
    * check for size of the buffer you want to use.
    * if buffer is too small, resize it; leave unchanged, if it is larger than
      needed.
    * use it.

    We can pass this structure to multiple functions;  after first run through
    functions buffer sizes will be finally determined,  and  on  a next run no
    allocation will be required.
    *************************************************************************/

    /// <summary>
    /// 
    /// </summary>
    public class apbuffers :
        apobject
    {
        /// <summary>
        /// 
        /// </summary>
        public bool[] ba0 = null!;

        /// <summary>
        /// 
        /// </summary>
        public int[] ia0 = null!;

        /// <summary>
        /// 
        /// </summary>
        public int[] ia1 = null!;

        /// <summary>
        /// 
        /// </summary>
        public int[] ia2 = null!;

        /// <summary>
        /// 
        /// </summary>
        public int[] ia3 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[] ra0 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[] ra1 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[] ra2 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[] ra3 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[,] rm0 = null!;

        /// <summary>
        /// 
        /// </summary>
        public double[,] rm1 = null!;

        public apbuffers()
        {
            init();
        }
        public override void init()
        {
            ba0 = new bool[0];
            ia0 = new int[0];
            ia1 = new int[0];
            ia2 = new int[0];
            ia3 = new int[0];
            ra0 = new double[0];
            ra1 = new double[0];
            ra2 = new double[0];
            ra3 = new double[0];
            rm0 = new double[0, 0];
            rm1 = new double[0, 0];
        }

        public override apobject make_copy()
        {
            apbuffers _result = new apbuffers();
            _result.ba0 = (bool[])ba0.Clone();
            _result.ia0 = (int[])ia0.Clone();
            _result.ia1 = (int[])ia1.Clone();
            _result.ia2 = (int[])ia2.Clone();
            _result.ia3 = (int[])ia3.Clone();
            _result.ra0 = (double[])ra0.Clone();
            _result.ra1 = (double[])ra1.Clone();
            _result.ra2 = (double[])ra2.Clone();
            _result.ra3 = (double[])ra3.Clone();
            _result.rm0 = (double[,])rm0.Clone();
            _result.rm1 = (double[,])rm1.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class sboolean : apobject
    {
        public bool val;
        public sboolean()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            sboolean _result = new sboolean();
            _result.val = val;
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class sbooleanarray : apobject
    {
        public bool[] val;
        public sbooleanarray()
        {
            init();
        }
        public override void init()
        {
            val = new bool[0];
        }
        public override apobject make_copy()
        {
            sbooleanarray _result = new sbooleanarray();
            _result.val = (bool[])val.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class sinteger : apobject
    {
        public int val;
        public sinteger()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            sinteger _result = new sinteger();
            _result.val = val;
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class sintegerarray : apobject
    {
        public int[] val;
        public sintegerarray()
        {
            init();
        }
        public override void init()
        {
            val = new int[0];
        }
        public override apobject make_copy()
        {
            sintegerarray _result = new sintegerarray();
            _result.val = (int[])val.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class sreal : apobject
    {
        public double val;
        public sreal()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            sreal _result = new sreal();
            _result.val = val;
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class srealarray : apobject
    {
        public double[] val;
        public srealarray()
        {
            init();
        }
        public override void init()
        {
            val = new double[0];
        }
        public override apobject make_copy()
        {
            srealarray _result = new srealarray();
            _result.val = (double[])val.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class scomplex : apobject
    {
        public complex val;
        public scomplex()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            scomplex _result = new scomplex();
            _result.val = val;
            return _result;
        }
    };


    /*************************************************************************
    Structure which is used to workaround limitations of ALGLIB parallellization
    environment.

      -- ALGLIB --
         Copyright 12.04.2009 by Bochkanov Sergey
    *************************************************************************/
    public class scomplexarray : apobject
    {
        public complex[] val;
        public scomplexarray()
        {
            init();
        }
        public override void init()
        {
            val = new complex[0];
        }
        public override apobject make_copy()
        {
            scomplexarray _result = new scomplexarray();
            _result.val = (complex[])val.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Thread-safe pool used to store/retrieve/recycle N-length boolean arrays.

      -- ALGLIB --
         Copyright 06.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public class nbpool : apobject
    {
        public int n;
        public int temporariescount;
        public smp.shared_pool sourcepool;
        public smp.shared_pool temporarypool;
        public sbooleanarray seed0;
        public sbooleanarray seedn;
        public nbpool()
        {
            init();
        }
        public override void init()
        {
            sourcepool = new smp.shared_pool();
            temporarypool = new smp.shared_pool();
            seed0 = new sbooleanarray();
            seedn = new sbooleanarray();
        }
        public override apobject make_copy()
        {
            nbpool _result = new nbpool();
            _result.n = n;
            _result.temporariescount = temporariescount;
            _result.sourcepool = (smp.shared_pool)sourcepool.make_copy();
            _result.temporarypool = (smp.shared_pool)temporarypool.make_copy();
            _result.seed0 = (sbooleanarray)seed0.make_copy();
            _result.seedn = (sbooleanarray)seedn.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Thread-safe pool used to store/retrieve/recycle N-length integer arrays.

      -- ALGLIB --
         Copyright 06.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public class nipool : apobject
    {
        public int n;
        public int temporariescount;
        public smp.shared_pool sourcepool;
        public smp.shared_pool temporarypool;
        public sintegerarray seed0;
        public sintegerarray seedn;
        public nipool()
        {
            init();
        }
        public override void init()
        {
            sourcepool = new smp.shared_pool();
            temporarypool = new smp.shared_pool();
            seed0 = new sintegerarray();
            seedn = new sintegerarray();
        }
        public override apobject make_copy()
        {
            nipool _result = new nipool();
            _result.n = n;
            _result.temporariescount = temporariescount;
            _result.sourcepool = (smp.shared_pool)sourcepool.make_copy();
            _result.temporarypool = (smp.shared_pool)temporarypool.make_copy();
            _result.seed0 = (sintegerarray)seed0.make_copy();
            _result.seedn = (sintegerarray)seedn.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Thread-safe pool used to store/retrieve/recycle N-length real arrays.

      -- ALGLIB --
         Copyright 06.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public class nrpool : apobject
    {
        public int n;
        public int temporariescount;
        public smp.shared_pool sourcepool;
        public smp.shared_pool temporarypool;
        public srealarray seed0;
        public srealarray seedn;
        public nrpool()
        {
            init();
        }
        public override void init()
        {
            sourcepool = new smp.shared_pool();
            temporarypool = new smp.shared_pool();
            seed0 = new srealarray();
            seedn = new srealarray();
        }
        public override apobject make_copy()
        {
            nrpool _result = new nrpool();
            _result.n = n;
            _result.temporariescount = temporariescount;
            _result.sourcepool = (smp.shared_pool)sourcepool.make_copy();
            _result.temporarypool = (smp.shared_pool)temporarypool.make_copy();
            _result.seed0 = (srealarray)seed0.make_copy();
            _result.seedn = (srealarray)seedn.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Counter used to compute average value of a set of numbers

      -- ALGLIB --
         Copyright 06.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public class savgcounter : apobject
    {
        public double rsum;
        public double rcnt;
        public double prior;
        public savgcounter()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            savgcounter _result = new savgcounter();
            _result.rsum = rsum;
            _result.rcnt = rcnt;
            _result.prior = prior;
            return _result;
        }
    };


    /*************************************************************************
    Counter used to compute a quantile of a set of numbers. Internally stores
    entire set, recomputes quantile with O(N) algo every time we req

      -- ALGLIB --
         Copyright 06.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public class squantilecounter : apobject
    {
        public int cnt;
        public double[] elems;
        public double prior;
        public squantilecounter()
        {
            init();
        }
        public override void init()
        {
            elems = new double[0];
        }
        public override apobject make_copy()
        {
            squantilecounter _result = new squantilecounter();
            _result.cnt = cnt;
            _result.elems = (double[])elems.Clone();
            _result.prior = prior;
            return _result;
        }
    };




    public const int maxtemporariesinnpool = 1000;


    /*************************************************************************
    Internally calls SetErrorFlag() with condition:

        Abs(Val-RefVal)>Tol*Max(Abs(RefVal),S)

    This function is used to test relative error in Val against  RefVal,  with
    relative error being replaced by absolute when scale  of  RefVal  is  less
    than S.

    This function returns value of COND.
    *************************************************************************/
    public static void seterrorflagdiff(ref bool flag,
        double val,
        double refval,
        double tol,
        double s,
        xparams _params)
    {
        ap.seterrorflag(ref flag, (double)(Math.Abs(val - refval)) > (double)(tol * Math.Max(Math.Abs(refval), s)), "apserv.ap:238");
    }


    /*************************************************************************
    The function always returns False.
    It may be used sometimes to prevent spurious warnings.

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool alwaysfalse(xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    The function "touches" boolean - it is used  to  avoid  compiler  messages
    about unused variables (in rare cases when we do NOT want to remove  these
    variables).

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void touchboolean(ref bool a,
        xparams _params)
    {
    }


    /*************************************************************************
    The function "touches" integer - it is used  to  avoid  compiler  messages
    about unused variables (in rare cases when we do NOT want to remove  these
    variables).

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void touchint(ref int a,
        xparams _params)
    {
    }


    /*************************************************************************
    The function "touches" real   -  it is used  to  avoid  compiler  messages
    about unused variables (in rare cases when we do NOT want to remove  these
    variables).

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void touchreal(ref double a,
        xparams _params)
    {
    }


    /*************************************************************************
    The function performs zero-coalescing on real value.

    NOTE: no check is performed for B<>0

      -- ALGLIB --
         Copyright 18.05.2015 by Bochkanov Sergey
    *************************************************************************/
    public static double coalesce(double a,
        double b,
        xparams _params)
    {
        double result = 0;

        result = a;
        if ((double)(a) == (double)(0.0))
        {
            result = b;
        }
        return result;
    }


    /*************************************************************************
    The function performs zero-coalescing on integer value.

    NOTE: no check is performed for B<>0

      -- ALGLIB --
         Copyright 18.05.2015 by Bochkanov Sergey
    *************************************************************************/
    public static int coalescei(int a,
        int b,
        xparams _params)
    {
        int result = 0;

        result = a;
        if (a == 0)
        {
            result = b;
        }
        return result;
    }


    /*************************************************************************
    The function convert integer value to real value.

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double inttoreal(int a,
        xparams _params)
    {
        double result = 0;

        result = a;
        return result;
    }


    /*************************************************************************
    The function calculates binary logarithm.

    NOTE: it costs twice as much as Ln(x)

      -- ALGLIB --
         Copyright 17.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double logbase2(double x,
        xparams _params)
    {
        double result = 0;

        result = Math.Log(x) / Math.Log(2);
        return result;
    }


    /*************************************************************************
    This function compares two numbers for approximate equality, with tolerance
    to errors as large as tol.


      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool approxequal(double a,
        double b,
        double tol,
        xparams _params)
    {
        bool result = new bool();

        result = (double)(Math.Abs(a - b)) <= (double)(tol);
        return result;
    }


    /*************************************************************************
    This function compares two numbers for approximate equality, with tolerance
    to errors as large as max(|a|,|b|)*tol.


      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool approxequalrel(double a,
        double b,
        double tol,
        xparams _params)
    {
        bool result = new bool();

        result = (double)(Math.Abs(a - b)) <= (double)(Math.Max(Math.Abs(a), Math.Abs(b)) * tol);
        return result;
    }


    /*************************************************************************
    This  function  generates  1-dimensional  general  interpolation task with
    moderate Lipshitz constant (close to 1.0)

    If N=1 then suborutine generates only one point at the middle of [A,B]

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void taskgenint1d(double a,
        double b,
        int n,
        ref double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        double h = 0;

        x = new double[0];
        y = new double[0];

        ap.assert(n >= 1, "TaskGenInterpolationEqdist1D: N<1!");
        x = new double[n];
        y = new double[n];
        if (n > 1)
        {
            x[0] = a;
            y[0] = 2 * math.randomreal() - 1;
            h = (b - a) / (n - 1);
            for (i = 1; i <= n - 1; i++)
            {
                if (i != n - 1)
                {
                    x[i] = a + (i + 0.2 * (2 * math.randomreal() - 1)) * h;
                }
                else
                {
                    x[i] = b;
                }
                y[i] = y[i - 1] + (2 * math.randomreal() - 1) * (x[i] - x[i - 1]);
            }
        }
        else
        {
            x[0] = 0.5 * (a + b);
            y[0] = 2 * math.randomreal() - 1;
        }
    }


    /*************************************************************************
    This function generates  1-dimensional equidistant interpolation task with
    moderate Lipshitz constant (close to 1.0)

    If N=1 then suborutine generates only one point at the middle of [A,B]

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void taskgenint1dequidist(double a,
        double b,
        int n,
        ref double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        double h = 0;

        x = new double[0];
        y = new double[0];

        ap.assert(n >= 1, "TaskGenInterpolationEqdist1D: N<1!");
        x = new double[n];
        y = new double[n];
        if (n > 1)
        {
            x[0] = a;
            y[0] = 2 * math.randomreal() - 1;
            h = (b - a) / (n - 1);
            for (i = 1; i <= n - 1; i++)
            {
                x[i] = a + i * h;
                y[i] = y[i - 1] + (2 * math.randomreal() - 1) * h;
            }
        }
        else
        {
            x[0] = 0.5 * (a + b);
            y[0] = 2 * math.randomreal() - 1;
        }
    }


    /*************************************************************************
    This function generates  1-dimensional Chebyshev-1 interpolation task with
    moderate Lipshitz constant (close to 1.0)

    If N=1 then suborutine generates only one point at the middle of [A,B]

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void taskgenint1dcheb1(double a,
        double b,
        int n,
        ref double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        x = new double[0];
        y = new double[0];

        ap.assert(n >= 1, "TaskGenInterpolation1DCheb1: N<1!");
        x = new double[n];
        y = new double[n];
        if (n > 1)
        {
            for (i = 0; i <= n - 1; i++)
            {
                x[i] = 0.5 * (b + a) + 0.5 * (b - a) * Math.Cos(Math.PI * (2 * i + 1) / (2 * n));
                if (i == 0)
                {
                    y[i] = 2 * math.randomreal() - 1;
                }
                else
                {
                    y[i] = y[i - 1] + (2 * math.randomreal() - 1) * (x[i] - x[i - 1]);
                }
            }
        }
        else
        {
            x[0] = 0.5 * (a + b);
            y[0] = 2 * math.randomreal() - 1;
        }
    }


    /*************************************************************************
    This function generates  1-dimensional Chebyshev-2 interpolation task with
    moderate Lipshitz constant (close to 1.0)

    If N=1 then suborutine generates only one point at the middle of [A,B]

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void taskgenint1dcheb2(double a,
        double b,
        int n,
        ref double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        x = new double[0];
        y = new double[0];

        ap.assert(n >= 1, "TaskGenInterpolation1DCheb2: N<1!");
        x = new double[n];
        y = new double[n];
        if (n > 1)
        {
            for (i = 0; i <= n - 1; i++)
            {
                x[i] = 0.5 * (b + a) + 0.5 * (b - a) * Math.Cos(Math.PI * i / (n - 1));
                if (i == 0)
                {
                    y[i] = 2 * math.randomreal() - 1;
                }
                else
                {
                    y[i] = y[i - 1] + (2 * math.randomreal() - 1) * (x[i] - x[i - 1]);
                }
            }
        }
        else
        {
            x[0] = 0.5 * (a + b);
            y[0] = 2 * math.randomreal() - 1;
        }
    }


    /*************************************************************************
    This function checks that all values from X[] are distinct. It does more
    than just usual floating point comparison:
    * first, it calculates max(X) and min(X)
    * second, it maps X[] from [min,max] to [1,2]
    * only at this stage actual comparison is done

    The meaning of such check is to ensure that all values are "distinct enough"
    and will not cause interpolation subroutine to fail.

    NOTE:
        X[] must be sorted by ascending (subroutine ASSERT's it)

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool aredistinct(double[] x,
        int n,
        xparams _params)
    {
        bool result = new bool();
        double a = 0;
        double b = 0;
        int i = 0;
        bool nonsorted = new bool();

        ap.assert(n >= 1, "APSERVAreDistinct: internal error (N<1)");
        if (n == 1)
        {

            //
            // everything is alright, it is up to caller to decide whether it
            // can interpolate something with just one point
            //
            result = true;
            return result;
        }
        a = x[0];
        b = x[0];
        nonsorted = false;
        for (i = 1; i <= n - 1; i++)
        {
            a = Math.Min(a, x[i]);
            b = Math.Max(b, x[i]);
            nonsorted = nonsorted || (double)(x[i - 1]) >= (double)(x[i]);
        }
        ap.assert(!nonsorted, "APSERVAreDistinct: internal error (not sorted)");
        for (i = 1; i <= n - 1; i++)
        {
            if ((double)((x[i] - a) / (b - a) + 1) == (double)((x[i - 1] - a) / (b - a) + 1))
            {
                result = false;
                return result;
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that two boolean values are the same (both  are  True 
    or both are False).

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool aresameboolean(bool v1,
        bool v2,
        xparams _params)
    {
        bool result = new bool();

        result = (v1 && v2) || (!v1 && !v2);
        return result;
    }


    /*************************************************************************
    Resizes X and fills by zeros

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void setlengthzero(ref double[] x,
        int n,
        xparams _params)
    {
        int i = 0;

        ap.assert(n >= 0, "SetLengthZero: N<0");
        x = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            x[i] = 0;
        }
    }


    /*************************************************************************
    If Length(X)<N, resizes X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void bvectorsetlengthatleast(ref bool[] x,
        int n,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new bool[n];
        }
    }


    /*************************************************************************
    If Length(X)<N, resizes X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void ivectorsetlengthatleast(ref int[] x,
        int n,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new int[n];
        }
    }


    /*************************************************************************
    If Length(X)<N, resizes X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rvectorsetlengthatleast(ref double[] x,
        int n,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new double[n];
        }
    }


    /*************************************************************************
    If Cols(X)<N or Rows(X)<M, resizes X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixsetlengthatleast(ref double[,] x,
        int m,
        int n,
        xparams _params)
    {
        if (m > 0 && n > 0)
        {
            if (ap.rows(x) < m || ap.cols(x) < n)
            {
                x = new double[m, n];
            }
        }
    }


    /*************************************************************************
    If Cols(X)<N or Rows(X)<M, resizes X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void bmatrixsetlengthatleast(ref bool[,] x,
        int m,
        int n,
        xparams _params)
    {
        if (m > 0 && n > 0)
        {
            if (ap.rows(x) < m || ap.cols(x) < n)
            {
                x = new bool[m, n];
            }
        }
    }


    /*************************************************************************
    Grows X, i.e. changes its size in such a way that:
    a) contents is preserved
    b) new size is at least N
    c) new size can be larger than N, so subsequent grow() calls can return
       without reallocation

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void bvectorgrowto(ref bool[] x,
        int n,
        xparams _params)
    {
        bool[] oldx = new bool[0];
        int i = 0;
        int n2 = 0;


        //
        // Enough place
        //
        if (ap.len(x) >= n)
        {
            return;
        }

        //
        // Choose new size
        //
        n = Math.Max(n, (int)Math.Round(1.8 * ap.len(x) + 1));

        //
        // Grow
        //
        n2 = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new bool[n];
        for (i = 0; i <= n - 1; i++)
        {
            if (i < n2)
            {
                x[i] = oldx[i];
            }
            else
            {
                x[i] = false;
            }
        }
    }


    /*************************************************************************
    Grows X, i.e. changes its size in such a way that:
    a) contents is preserved
    b) new size is at least N
    c) new size can be larger than N, so subsequent grow() calls can return
       without reallocation

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void ivectorgrowto(ref int[] x,
        int n,
        xparams _params)
    {
        int[] oldx = new int[0];
        int i = 0;
        int n2 = 0;


        //
        // Enough place
        //
        if (ap.len(x) >= n)
        {
            return;
        }

        //
        // Choose new size
        //
        n = Math.Max(n, (int)Math.Round(1.8 * ap.len(x) + 1));

        //
        // Grow
        //
        n2 = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new int[n];
        for (i = 0; i <= n - 1; i++)
        {
            if (i < n2)
            {
                x[i] = oldx[i];
            }
            else
            {
                x[i] = 0;
            }
        }
    }


    /*************************************************************************
    Grows X, i.e. appends rows in such a way that:
    a) contents is preserved
    b) new row count is at least N
    c) new row count can be larger than N, so subsequent grow() calls can return
       without reallocation
    d) new matrix has at least MinCols columns (if less than specified amount
       of columns is present, new columns are added with undefined contents);
       MinCols can be 0 or negative value = ignored

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgrowrowsto(ref double[,] a,
        int n,
        int mincols,
        xparams _params)
    {
        double[,] olda = new double[0, 0];
        int i = 0;
        int j = 0;
        int n2 = 0;
        int m = 0;


        //
        // Enough place?
        //
        if (ap.rows(a) >= n && ap.cols(a) >= mincols)
        {
            return;
        }

        //
        // Sizes and metrics
        //
        if (ap.rows(a) < n)
        {
            n = Math.Max(n, (int)Math.Round(1.8 * ap.rows(a) + 1));
        }
        n2 = Math.Min(ap.rows(a), n);
        m = ap.cols(a);

        //
        // Grow
        //
        ap.swap(ref a, ref olda);
        a = new double[n, Math.Max(m, mincols)];
        for (i = 0; i <= n2 - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                a[i, j] = olda[i, j];
            }
        }
    }


    /*************************************************************************
    Grows X, i.e. appends cols in such a way that:
    a) contents is preserved
    b) new col count is at least N
    c) new col count can be larger than N, so subsequent grow() calls can return
       without reallocation
    d) new matrix has at least MinRows row (if less than specified amount
       of rows is present, new rows are added with undefined contents);
       MinRows can be 0 or negative value = ignored

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgrowcolsto(ref double[,] a,
        int n,
        int minrows,
        xparams _params)
    {
        double[,] olda = new double[0, 0];
        int i = 0;
        int j = 0;
        int n2 = 0;
        int m = 0;


        //
        // Enough place?
        //
        if (ap.cols(a) >= n && ap.rows(a) >= minrows)
        {
            return;
        }

        //
        // Sizes and metrics
        //
        if (ap.cols(a) < n)
        {
            n = Math.Max(n, (int)Math.Round(1.8 * ap.cols(a) + 1));
        }
        n2 = Math.Min(ap.cols(a), n);
        m = ap.rows(a);

        //
        // Grow
        //
        ap.swap(ref a, ref olda);
        a = new double[Math.Max(m, minrows), n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n2 - 1; j++)
            {
                a[i, j] = olda[i, j];
            }
        }
    }


    /*************************************************************************
    Grows X, i.e. changes its size in such a way that:
    a) contents is preserved
    b) new size is at least N
    c) new size can be larger than N, so subsequent grow() calls can return
       without reallocation

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rvectorgrowto(ref double[] x,
        int n,
        xparams _params)
    {
        double[] oldx = new double[0];
        int i = 0;
        int n2 = 0;


        //
        // Enough place
        //
        if (ap.len(x) >= n)
        {
            return;
        }

        //
        // Choose new size
        //
        n = Math.Max(n, (int)Math.Round(1.8 * ap.len(x) + 1));

        //
        // Grow
        //
        n2 = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            if (i < n2)
            {
                x[i] = oldx[i];
            }
            else
            {
                x[i] = 0;
            }
        }
    }


    /*************************************************************************
    Resizes X and:
    * preserves old contents of X
    * fills new elements by zeros

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void ivectorresize(ref int[] x,
        int n,
        xparams _params)
    {
        int[] oldx = new int[0];
        int i = 0;
        int n2 = 0;

        n2 = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new int[n];
        for (i = 0; i <= n - 1; i++)
        {
            if (i < n2)
            {
                x[i] = oldx[i];
            }
            else
            {
                x[i] = 0;
            }
        }
    }


    /*************************************************************************
    Resizes X and:
    * preserves old contents of X
    * fills new elements by zeros

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rvectorresize(ref double[] x,
        int n,
        xparams _params)
    {
        double[] oldx = new double[0];
        int i = 0;
        int n2 = 0;

        n2 = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            if (i < n2)
            {
                x[i] = oldx[i];
            }
            else
            {
                x[i] = 0;
            }
        }
    }


    /*************************************************************************
    Resizes X and:
    * preserves old contents of X
    * fills new elements by zeros

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixresize(ref double[,] x,
        int m,
        int n,
        xparams _params)
    {
        double[,] oldx = new double[0, 0];
        int i = 0;
        int j = 0;
        int m2 = 0;
        int n2 = 0;

        m2 = ap.rows(x);
        n2 = ap.cols(x);
        ap.swap(ref x, ref oldx);
        x = new double[m, n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (i < m2 && j < n2)
                {
                    x[i, j] = oldx[i, j];
                }
                else
                {
                    x[i, j] = 0.0;
                }
            }
        }
    }


    /*************************************************************************
    Resizes X and:
    * preserves old contents of X
    * fills new elements by zeros

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void imatrixresize(ref int[,] x,
        int m,
        int n,
        xparams _params)
    {
        int[,] oldx = new int[0, 0];
        int i = 0;
        int j = 0;
        int m2 = 0;
        int n2 = 0;

        m2 = ap.rows(x);
        n2 = ap.cols(x);
        ap.swap(ref x, ref oldx);
        x = new int[m, n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (i < m2 && j < n2)
                {
                    x[i, j] = oldx[i, j];
                }
                else
                {
                    x[i, j] = 0;
                }
            }
        }
    }


    /*************************************************************************
    appends element to X

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void ivectorappend(ref int[] x,
        int v,
        xparams _params)
    {
        int[] oldx = new int[0];
        int i = 0;
        int n = 0;

        n = ap.len(x);
        ap.swap(ref x, ref oldx);
        x = new int[n + 1];
        for (i = 0; i <= n - 1; i++)
        {
            x[i] = oldx[i];
        }
        x[n] = v;
    }


    /*************************************************************************
    This function checks that length(X) is at least N and first N values  from
    X[] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfinitevector(double[] x,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        double v = 0;

        ap.assert(n >= 0, "APSERVIsFiniteVector: internal error (N<0)");
        if (n == 0)
        {
            result = true;
            return result;
        }
        if (ap.len(x) < n)
        {
            result = false;
            return result;
        }
        v = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = 0.01 * v + x[i];
        }
        result = math.isfinite(v);
        return result;
    }


    /*************************************************************************
    This function checks that length(X) is at least N and first N values  from
    X[] are finite or NANs

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfiniteornanvector(double[] x,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        double v = 0;

        ap.assert(n >= 0, "APSERVIsFiniteVector: internal error (N<0)");
        if (n == 0)
        {
            result = true;
            return result;
        }
        if (ap.len(x) < n)
        {
            result = false;
            return result;
        }

        //
        // Is it entirely finite?
        //
        v = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = 0.01 * v + x[i];
        }
        if (math.isfinite(v))
        {
            result = true;
            return result;
        }

        //
        // OK, check that either finite or nan
        //
        for (i = 0; i <= n - 1; i++)
        {
            if (!math.isfinite(x[i]) && !Double.IsNaN(x[i]))
            {
                result = false;
                return result;
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that first N values from X[] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfinitecvector(complex[] z,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;

        ap.assert(n >= 0, "APSERVIsFiniteCVector: internal error (N<0)");
        for (i = 0; i <= n - 1; i++)
        {
            if (!math.isfinite(z[i].x) || !math.isfinite(z[i].y))
            {
                result = false;
                return result;
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that size of X is at least MxN and values from
    X[0..M-1,0..N-1] are finite.

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool apservisfinitematrix(double[,] x,
        int m,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteMatrix: internal error (N<0)");
        ap.assert(m >= 0, "APSERVIsFiniteMatrix: internal error (M<0)");
        if (m == 0 || n == 0)
        {
            result = true;
            return result;
        }
        if (ap.rows(x) < m || ap.cols(x) < n)
        {
            result = false;
            return result;
        }
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (!math.isfinite(x[i, j]))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that all values from X[0..M-1,0..N-1] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool apservisfinitecmatrix(complex[,] x,
        int m,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteCMatrix: internal error (N<0)");
        ap.assert(m >= 0, "APSERVIsFiniteCMatrix: internal error (M<0)");
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (!math.isfinite(x[i, j].x) || !math.isfinite(x[i, j].y))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that all values from X[0..M-1,0..N-1] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfinitecmatrix(complex[,] x,
        int m,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;

        ap.assert(n >= 0, "IsFiniteCMatrix: internal error (N<0)");
        ap.assert(m >= 0, "IsFiniteCMatrix: internal error (M<0)");
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (!math.isfinite(x[i, j].x) || !math.isfinite(x[i, j].y))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that size of X is at least NxN and all values from
    upper/lower triangle of X[0..N-1,0..N-1] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfinitertrmatrix(double[,] x,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j1 = 0;
        int j2 = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteRTRMatrix: internal error (N<0)");
        if (n == 0)
        {
            result = true;
            return result;
        }
        if (ap.rows(x) < n || ap.cols(x) < n)
        {
            result = false;
            return result;
        }
        for (i = 0; i <= n - 1; i++)
        {
            if (isupper)
            {
                j1 = i;
                j2 = n - 1;
            }
            else
            {
                j1 = 0;
                j2 = i;
            }
            for (j = j1; j <= j2; j++)
            {
                if (!math.isfinite(x[i, j]))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that all values from upper/lower triangle of
    X[0..N-1,0..N-1] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool apservisfinitectrmatrix(complex[,] x,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j1 = 0;
        int j2 = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteCTRMatrix: internal error (N<0)");
        for (i = 0; i <= n - 1; i++)
        {
            if (isupper)
            {
                j1 = i;
                j2 = n - 1;
            }
            else
            {
                j1 = 0;
                j2 = i;
            }
            for (j = j1; j <= j2; j++)
            {
                if (!math.isfinite(x[i, j].x) || !math.isfinite(x[i, j].y))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that all values from upper/lower triangle of
    X[0..N-1,0..N-1] are finite

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool isfinitectrmatrix(complex[,] x,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j1 = 0;
        int j2 = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteCTRMatrix: internal error (N<0)");
        for (i = 0; i <= n - 1; i++)
        {
            if (isupper)
            {
                j1 = i;
                j2 = n - 1;
            }
            else
            {
                j1 = 0;
                j2 = i;
            }
            for (j = j1; j <= j2; j++)
            {
                if (!math.isfinite(x[i, j].x) || !math.isfinite(x[i, j].y))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function checks that all values from X[0..M-1,0..N-1] are  finite  or
    NaN's.

      -- ALGLIB --
         Copyright 18.06.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool apservisfiniteornanmatrix(double[,] x,
        int m,
        int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;

        ap.assert(n >= 0, "APSERVIsFiniteOrNaNMatrix: internal error (N<0)");
        ap.assert(m >= 0, "APSERVIsFiniteOrNaNMatrix: internal error (M<0)");
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (!(math.isfinite(x[i, j]) || Double.IsNaN(x[i, j])))
                {
                    result = false;
                    return result;
                }
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    Safe sqrt(x^2+y^2)

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static double safepythag2(double x,
        double y,
        xparams _params)
    {
        double result = 0;
        double w = 0;
        double xabs = 0;
        double yabs = 0;
        double z = 0;

        xabs = Math.Abs(x);
        yabs = Math.Abs(y);
        w = Math.Max(xabs, yabs);
        z = Math.Min(xabs, yabs);
        if ((double)(z) == (double)(0))
        {
            result = w;
        }
        else
        {
            result = w * Math.Sqrt(1 + math.sqr(z / w));
        }
        return result;
    }


    /*************************************************************************
    Safe sqrt(x^2+y^2)

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static double safepythag3(double x,
        double y,
        double z,
        xparams _params)
    {
        double result = 0;
        double w = 0;

        w = Math.Max(Math.Abs(x), Math.Max(Math.Abs(y), Math.Abs(z)));
        if ((double)(w) == (double)(0))
        {
            result = 0;
            return result;
        }
        x = x / w;
        y = y / w;
        z = z / w;
        result = w * Math.Sqrt(math.sqr(x) + math.sqr(y) + math.sqr(z));
        return result;
    }


    /*************************************************************************
    Safe division.

    This function attempts to calculate R=X/Y without overflow.

    It returns:
    * +1, if abs(X/Y)>=MaxRealNumber or undefined - overflow-like situation
          (no overlfow is generated, R is either NAN, PosINF, NegINF)
    *  0, if MinRealNumber<abs(X/Y)<MaxRealNumber or X=0, Y<>0
          (R contains result, may be zero)
    * -1, if 0<abs(X/Y)<MinRealNumber - underflow-like situation
          (R contains zero; it corresponds to underflow)

    No overflow is generated in any case.

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static int saferdiv(double x,
        double y,
        ref double r,
        xparams _params)
    {
        int result = 0;

        r = 0;


        //
        // Two special cases:
        // * Y=0
        // * X=0 and Y<>0
        //
        if ((double)(y) == (double)(0))
        {
            result = 1;
            if ((double)(x) == (double)(0))
            {
                r = Double.NaN;
            }
            if ((double)(x) > (double)(0))
            {
                r = Double.PositiveInfinity;
            }
            if ((double)(x) < (double)(0))
            {
                r = Double.NegativeInfinity;
            }
            return result;
        }
        if ((double)(x) == (double)(0))
        {
            r = 0;
            result = 0;
            return result;
        }

        //
        // make Y>0
        //
        if ((double)(y) < (double)(0))
        {
            x = -x;
            y = -y;
        }

        //
        //
        //
        if ((double)(y) >= (double)(1))
        {
            r = x / y;
            if ((double)(Math.Abs(r)) <= (double)(math.minrealnumber))
            {
                result = -1;
                r = 0;
            }
            else
            {
                result = 0;
            }
        }
        else
        {
            if ((double)(Math.Abs(x)) >= (double)(math.maxrealnumber * y))
            {
                if ((double)(x) > (double)(0))
                {
                    r = Double.PositiveInfinity;
                }
                else
                {
                    r = Double.NegativeInfinity;
                }
                result = 1;
            }
            else
            {
                r = x / y;
                result = 0;
            }
        }
        return result;
    }


    /*************************************************************************
    This function calculates "safe" min(X/Y,V) for positive finite X, Y, V.
    No overflow is generated in any case.

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static double safeminposrv(double x,
        double y,
        double v,
        xparams _params)
    {
        double result = 0;
        double r = 0;

        if (y >= 1)
        {

            //
            // Y>=1, we can safely divide by Y
            //
            r = x / y;
            result = v;
            if (v > r)
            {
                result = r;
            }
            else
            {
                result = v;
            }
        }
        else
        {

            //
            // Y<1, we can safely multiply by Y
            //
            if (x < v * y)
            {
                result = x / y;
            }
            else
            {
                result = v;
            }
        }
        return result;
    }


    /*************************************************************************
    This function makes periodic mapping of X to [A,B].

    It accepts X, A, B (A>B). It returns T which lies in  [A,B] and integer K,
    such that X = T + K*(B-A).

    NOTES:
    * K is represented as real value, although actually it is integer
    * T is guaranteed to be in [A,B]
    * T replaces X

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static void apperiodicmap(ref double x,
        double a,
        double b,
        ref double k,
        xparams _params)
    {
        k = 0;

        ap.assert((double)(a) < (double)(b), "APPeriodicMap: internal error!");
        k = (int)Math.Floor((x - a) / (b - a));
        x = x - k * (b - a);
        while ((double)(x) < (double)(a))
        {
            x = x + (b - a);
            k = k - 1;
        }
        while ((double)(x) > (double)(b))
        {
            x = x - (b - a);
            k = k + 1;
        }
        x = Math.Max(x, a);
        x = Math.Min(x, b);
    }


    /*************************************************************************
    Returns random normal number using low-quality system-provided generator

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double randomnormal(xparams _params)
    {
        double result = 0;
        double u = 0;
        double v = 0;
        double s = 0;

        while (true)
        {
            u = 2 * math.randomreal() - 1;
            v = 2 * math.randomreal() - 1;
            s = math.sqr(u) + math.sqr(v);
            if ((double)(s) > (double)(0) && (double)(s) < (double)(1))
            {

                //
                // two Sqrt's instead of one to
                // avoid overflow when S is too small
                //
                s = Math.Sqrt(-(2 * Math.Log(s))) / Math.Sqrt(s);
                result = u * s;
                break;
            }
        }
        return result;
    }


    /*************************************************************************
    Generates random unit vector using low-quality system-provided generator.
    Reallocates array if its size is too short.

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void randomunit(int n,
        ref double[] x,
        xparams _params)
    {
        int i = 0;
        double v = 0;
        double vv = 0;

        ap.assert(n > 0, "RandomUnit: N<=0");
        if (ap.len(x) < n)
        {
            x = new double[n];
        }
        do
        {
            v = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                vv = randomnormal(_params);
                x[i] = vv;
                v = v + vv * vv;
            }
        }
        while ((double)(v) <= (double)(0));
        v = 1 / Math.Sqrt(v);
        for (i = 0; i <= n - 1; i++)
        {
            x[i] = x[i] * v;
        }
    }


    /*************************************************************************
    This function is used to swap two integer values
    *************************************************************************/
    public static void swapi(ref int v0,
        ref int v1,
        xparams _params)
    {
        int v = 0;

        v = v0;
        v0 = v1;
        v1 = v;
    }


    /*************************************************************************
    This function is used to swap two real values
    *************************************************************************/
    public static void swapr(ref double v0,
        ref double v1,
        xparams _params)
    {
        double v = 0;

        v = v0;
        v0 = v1;
        v1 = v;
    }


    /*************************************************************************
    This function is used to swap two rows of the matrix; if NCols<0, automatically
    determined from the matrix size.
    *************************************************************************/
    public static void swaprows(double[,] a,
        int i0,
        int i1,
        int ncols,
        xparams _params)
    {
        int j = 0;
        double v = 0;

        if (i0 == i1)
        {
            return;
        }
        if (ncols < 0)
        {
            ncols = ap.cols(a);
        }
        for (j = 0; j <= ncols - 1; j++)
        {
            v = a[i0, j];
            a[i0, j] = a[i1, j];
            a[i1, j] = v;
        }
    }


    /*************************************************************************
    This function is used to swap two cols of the matrix; if NRows<0, automatically
    determined from the matrix size.
    *************************************************************************/
    public static void swapcols(double[,] a,
        int j0,
        int j1,
        int nrows,
        xparams _params)
    {
        int i = 0;
        double v = 0;

        if (j0 == j1)
        {
            return;
        }
        if (nrows < 0)
        {
            nrows = ap.rows(a);
        }
        for (i = 0; i <= nrows - 1; i++)
        {
            v = a[i, j0];
            a[i, j0] = a[i, j1];
            a[i, j1] = v;
        }
    }


    /*************************************************************************
    This function is used to swap two "entries" in 1-dimensional array composed
    from D-element entries
    *************************************************************************/
    public static void swapentries(double[] a,
        int i0,
        int i1,
        int entrywidth,
        xparams _params)
    {
        int offs0 = 0;
        int offs1 = 0;
        int j = 0;
        double v = 0;

        if (i0 == i1)
        {
            return;
        }
        offs0 = i0 * entrywidth;
        offs1 = i1 * entrywidth;
        for (j = 0; j <= entrywidth - 1; j++)
        {
            v = a[offs0 + j];
            a[offs0 + j] = a[offs1 + j];
            a[offs1 + j] = v;
        }
    }


    /*************************************************************************
    This function is used to swap two "entries" in 1-dimensional array composed
    from D-element entries
    *************************************************************************/
    public static void swapentriesb(bool[] a,
        int i0,
        int i1,
        int entrywidth,
        xparams _params)
    {
        int offs0 = 0;
        int offs1 = 0;
        int j = 0;
        bool v = new bool();

        if (i0 == i1)
        {
            return;
        }
        offs0 = i0 * entrywidth;
        offs1 = i1 * entrywidth;
        for (j = 0; j <= entrywidth - 1; j++)
        {
            v = a[offs0 + j];
            a[offs0 + j] = a[offs1 + j];
            a[offs1 + j] = v;
        }
    }


    /*************************************************************************
    This function is used to swap two elements of the vector
    *************************************************************************/
    public static void swapelements(double[] a,
        int i0,
        int i1,
        xparams _params)
    {
        double v = 0;

        if (i0 == i1)
        {
            return;
        }
        v = a[i0];
        a[i0] = a[i1];
        a[i1] = v;
    }


    /*************************************************************************
    This function is used to swap two elements of the vector
    *************************************************************************/
    public static void swapelementsi(int[] a,
        int i0,
        int i1,
        xparams _params)
    {
        int v = 0;

        if (i0 == i1)
        {
            return;
        }
        v = a[i0];
        a[i0] = a[i1];
        a[i1] = v;
    }


    /*************************************************************************
    This function is used to return maximum of three real values
    *************************************************************************/
    public static double maxreal3(double v0,
        double v1,
        double v2,
        xparams _params)
    {
        double result = 0;

        result = v0;
        if ((double)(result) < (double)(v1))
        {
            result = v1;
        }
        if ((double)(result) < (double)(v2))
        {
            result = v2;
        }
        return result;
    }


    /*************************************************************************
    This function is used to increment value of integer variable
    *************************************************************************/
    public static void inc(ref int v,
        xparams _params)
    {
        v = v + 1;
    }


    /*************************************************************************
    This function is used to decrement value of integer variable
    *************************************************************************/
    public static void dec(ref int v,
        xparams _params)
    {
        v = v - 1;
    }


    /*************************************************************************
    This function is used to increment value of integer variable; name of  the
    function suggests that increment is done in multithreaded setting  in  the
    thread-unsafe manner (optional progress reports which do not need guaranteed
    correctness)
    *************************************************************************/
    public static void threadunsafeinc(ref int v,
        xparams _params)
    {
        v = v + 1;
    }


    /*************************************************************************
    This function is used to increment value of integer variable; name of  the
    function suggests that increment is done in multithreaded setting  in  the
    thread-unsafe manner (optional progress reports which do not need guaranteed
    correctness)
    *************************************************************************/
    public static void threadunsafeincby(ref int v,
        int k,
        xparams _params)
    {
        v = v + k;
    }


    /*************************************************************************
    This function performs two operations:
    1. decrements value of integer variable, if it is positive
    2. explicitly sets variable to zero if it is non-positive
    It is used by some algorithms to decrease value of internal counters.
    *************************************************************************/
    public static void countdown(ref int v,
        xparams _params)
    {
        if (v > 0)
        {
            v = v - 1;
        }
        else
        {
            v = 0;
        }
    }


    /*************************************************************************
    This function returns +1 or -1 depending on sign of X.
    x=0 results in +1 being returned.
    *************************************************************************/
    public static double possign(double x,
        xparams _params)
    {
        double result = 0;

        if ((double)(x) >= (double)(0))
        {
            result = 1;
        }
        else
        {
            result = -1;
        }
        return result;
    }


    /*************************************************************************
    This function returns product of two real numbers. It is convenient when
    you have to perform typecast-and-product of two INTEGERS.
    *************************************************************************/
    public static double rmul2(double v0,
        double v1,
        xparams _params)
    {
        double result = 0;

        result = v0 * v1;
        return result;
    }


    /*************************************************************************
    This function returns product of three real numbers. It is convenient when
    you have to perform typecast-and-product of three INTEGERS.
    *************************************************************************/
    public static double rmul3(double v0,
        double v1,
        double v2,
        xparams _params)
    {
        double result = 0;

        result = v0 * v1 * v2;
        return result;
    }


    /*************************************************************************
    This function returns product of four real numbers. It is convenient when
    you have to perform typecast-and-product of four INTEGERS.
    *************************************************************************/
    public static double rmul4(double v0,
        double v1,
        double v2,
        double v3,
        xparams _params)
    {
        double result = 0;

        result = v0 * v1 * v2 * v3;
        return result;
    }


    /*************************************************************************
    This function returns (A div B) rounded up; it expects that A>0, B>0, but
    does not check it.
    *************************************************************************/
    public static int idivup(int a,
        int b,
        xparams _params)
    {
        int result = 0;

        result = a / b;
        if (a % b > 0)
        {
            result = result + 1;
        }
        return result;
    }


    /*************************************************************************
    This function returns min(i0,i1)
    *************************************************************************/
    public static int imin2(int i0,
        int i1,
        xparams _params)
    {
        int result = 0;

        result = i0;
        if (i1 < result)
        {
            result = i1;
        }
        return result;
    }


    /*************************************************************************
    This function returns min(i0,i1,i2)
    *************************************************************************/
    public static int imin3(int i0,
        int i1,
        int i2,
        xparams _params)
    {
        int result = 0;

        result = i0;
        if (i1 < result)
        {
            result = i1;
        }
        if (i2 < result)
        {
            result = i2;
        }
        return result;
    }


    /*************************************************************************
    This function returns max(i0,i1)
    *************************************************************************/
    public static int imax2(int i0,
        int i1,
        xparams _params)
    {
        int result = 0;

        result = i0;
        if (i1 > result)
        {
            result = i1;
        }
        return result;
    }


    /*************************************************************************
    This function returns max(i0,i1,i2)
    *************************************************************************/
    public static int imax3(int i0,
        int i1,
        int i2,
        xparams _params)
    {
        int result = 0;

        result = i0;
        if (i1 > result)
        {
            result = i1;
        }
        if (i2 > result)
        {
            result = i2;
        }
        return result;
    }


    /*************************************************************************
    This function returns max(r0,r1,r2)
    *************************************************************************/
    public static double rmax3(double r0,
        double r1,
        double r2,
        xparams _params)
    {
        double result = 0;

        result = r0;
        if ((double)(r1) > (double)(result))
        {
            result = r1;
        }
        if ((double)(r2) > (double)(result))
        {
            result = r2;
        }
        return result;
    }


    /*************************************************************************
    This function returns max(|r0|,|r1|,|r2|)
    *************************************************************************/
    public static double rmaxabs3(double r0,
        double r1,
        double r2,
        xparams _params)
    {
        double result = 0;

        r0 = Math.Abs(r0);
        r1 = Math.Abs(r1);
        r2 = Math.Abs(r2);
        result = r0;
        if ((double)(r1) > (double)(result))
        {
            result = r1;
        }
        if ((double)(r2) > (double)(result))
        {
            result = r2;
        }
        return result;
    }


    /*************************************************************************
    'bounds' value: maps X to [B1,B2]

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double boundval(double x,
        double b1,
        double b2,
        xparams _params)
    {
        double result = 0;

        if ((double)(x) <= (double)(b1))
        {
            result = b1;
            return result;
        }
        if ((double)(x) >= (double)(b2))
        {
            result = b2;
            return result;
        }
        result = x;
        return result;
    }


    /*************************************************************************
    'bounds' value: maps X to [B1,B2]

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static int iboundval(int x,
        int b1,
        int b2,
        xparams _params)
    {
        int result = 0;

        if (x <= b1)
        {
            result = b1;
            return result;
        }
        if (x >= b2)
        {
            result = b2;
            return result;
        }
        result = x;
        return result;
    }


    /*************************************************************************
    'bounds' value: maps X to [B1,B2]

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double rboundval(double x,
        double b1,
        double b2,
        xparams _params)
    {
        double result = 0;

        if ((double)(x) <= (double)(b1))
        {
            result = b1;
            return result;
        }
        if ((double)(x) >= (double)(b2))
        {
            result = b2;
            return result;
        }
        result = x;
        return result;
    }


    /*************************************************************************
    Boolean case-2: returns V0 if Cond=True, V1 otherwise
    *************************************************************************/
    public static bool bcase2(bool cond,
        bool v0,
        bool v1,
        xparams _params)
    {
        bool result = new bool();

        if (cond)
        {
            result = v0;
        }
        else
        {
            result = v1;
        }
        return result;
    }


    /*************************************************************************
    Integer case-2: returns V0 if Cond=True, V1 otherwise
    *************************************************************************/
    public static int icase2(bool cond,
        int v0,
        int v1,
        xparams _params)
    {
        int result = 0;

        if (cond)
        {
            result = v0;
        }
        else
        {
            result = v1;
        }
        return result;
    }


    /*************************************************************************
    Real case-2: returns V0 if Cond=True, V1 otherwise
    *************************************************************************/
    public static double rcase2(bool cond,
        double v0,
        double v1,
        xparams _params)
    {
        double result = 0;

        if (cond)
        {
            result = v0;
        }
        else
        {
            result = v1;
        }
        return result;
    }


    /*************************************************************************
    Returns number of non-zeros
    *************************************************************************/
    public static int countnz1(double[] v,
        int n,
        xparams _params)
    {
        int result = 0;
        int i = 0;

        result = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if (!(v[i] == 0))
            {
                result = result + 1;
            }
        }
        return result;
    }


    /*************************************************************************
    Returns number of non-zeros
    *************************************************************************/
    public static int countnz2(double[,] v,
        int m,
        int n,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;

        result = 0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (!(v[i, j] == 0))
                {
                    result = result + 1;
                }
            }
        }
        return result;
    }


    /*************************************************************************
    Allocation of serializer: complex value
    *************************************************************************/
    public static void alloccomplex(serializer s,
        complex v,
        xparams _params)
    {
        s.alloc_entry();
        s.alloc_entry();
    }


    /*************************************************************************
    Serialization: complex value
    *************************************************************************/
    public static void serializecomplex(serializer s,
        complex v,
        xparams _params)
    {
        s.serialize_double(v.x);
        s.serialize_double(v.y);
    }


    /*************************************************************************
    Unserialization: complex value
    *************************************************************************/
    public static complex unserializecomplex(serializer s,
        xparams _params)
    {
        complex result = 0;

        result.x = s.unserialize_double();
        result.y = s.unserialize_double();
        return result;
    }


    /*************************************************************************
    Allocation of serializer: real array
    *************************************************************************/
    public static void allocrealarray(serializer s,
        double[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.alloc_entry();
        for (i = 0; i <= n - 1; i++)
        {
            s.alloc_entry();
        }
    }


    /*************************************************************************
    Allocation of serializer: boolean array
    *************************************************************************/
    public static void allocbooleanarray(serializer s,
        bool[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.alloc_entry();
        for (i = 0; i <= n - 1; i++)
        {
            s.alloc_entry();
        }
    }


    /*************************************************************************
    Serialization: complex value
    *************************************************************************/
    public static void serializerealarray(serializer s,
        double[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.serialize_int(n);
        for (i = 0; i <= n - 1; i++)
        {
            s.serialize_double(v[i]);
        }
    }


    /*************************************************************************
    Serialization: boolean array
    *************************************************************************/
    public static void serializebooleanarray(serializer s,
        bool[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.serialize_int(n);
        for (i = 0; i <= n - 1; i++)
        {
            s.serialize_bool(v[i]);
        }
    }


    /*************************************************************************
    Unserialization: complex value
    *************************************************************************/
    public static void unserializerealarray(serializer s,
        ref double[] v,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        double t = 0;

        v = new double[0];

        n = s.unserialize_int();
        if (n == 0)
        {
            return;
        }
        v = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            t = s.unserialize_double();
            v[i] = t;
        }
    }


    /*************************************************************************
    Unserialization: boolean value
    *************************************************************************/
    public static void unserializebooleanarray(serializer s,
        ref bool[] v,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        bool t = new bool();

        v = new bool[0];

        n = s.unserialize_int();
        if (n == 0)
        {
            return;
        }
        v = new bool[n];
        for (i = 0; i <= n - 1; i++)
        {
            t = s.unserialize_bool();
            v[i] = t;
        }
    }


    /*************************************************************************
    Allocation of serializer: Integer array
    *************************************************************************/
    public static void allocintegerarray(serializer s,
        int[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.alloc_entry();
        for (i = 0; i <= n - 1; i++)
        {
            s.alloc_entry();
        }
    }


    /*************************************************************************
    Serialization: Integer array
    *************************************************************************/
    public static void serializeintegerarray(serializer s,
        int[] v,
        int n,
        xparams _params)
    {
        int i = 0;

        if (n < 0)
        {
            n = ap.len(v);
        }
        s.serialize_int(n);
        for (i = 0; i <= n - 1; i++)
        {
            s.serialize_int(v[i]);
        }
    }


    /*************************************************************************
    Unserialization: complex value
    *************************************************************************/
    public static void unserializeintegerarray(serializer s,
        ref int[] v,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        int t = 0;

        v = new int[0];

        n = s.unserialize_int();
        if (n == 0)
        {
            return;
        }
        v = new int[n];
        for (i = 0; i <= n - 1; i++)
        {
            t = s.unserialize_int();
            v[i] = t;
        }
    }


    /*************************************************************************
    Allocation of serializer: real matrix
    *************************************************************************/
    public static void allocrealmatrix(serializer s,
        double[,] v,
        int n0,
        int n1,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        if (n0 < 0)
        {
            n0 = ap.rows(v);
        }
        if (n1 < 0)
        {
            n1 = ap.cols(v);
        }
        s.alloc_entry();
        s.alloc_entry();
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                s.alloc_entry();
            }
        }
    }


    /*************************************************************************
    Serialization: complex value
    *************************************************************************/
    public static void serializerealmatrix(serializer s,
        double[,] v,
        int n0,
        int n1,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        if (n0 < 0)
        {
            n0 = ap.rows(v);
        }
        if (n1 < 0)
        {
            n1 = ap.cols(v);
        }
        s.serialize_int(n0);
        s.serialize_int(n1);
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                s.serialize_double(v[i, j]);
            }
        }
    }


    /*************************************************************************
    Unserialization: complex value
    *************************************************************************/
    public static void unserializerealmatrix(serializer s,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n0 = 0;
        int n1 = 0;
        double t = 0;

        v = new double[0, 0];

        n0 = s.unserialize_int();
        n1 = s.unserialize_int();
        if (n0 == 0 || n1 == 0)
        {
            return;
        }
        v = new double[n0, n1];
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                t = s.unserialize_double();
                v[i, j] = t;
            }
        }
    }


    /*************************************************************************
    Copy boolean array
    *************************************************************************/
    public static void copybooleanarray(bool[] src,
        ref bool[] dst,
        xparams _params)
    {
        int i = 0;

        dst = new bool[0];

        if (ap.len(src) > 0)
        {
            dst = new bool[ap.len(src)];
            for (i = 0; i <= ap.len(src) - 1; i++)
            {
                dst[i] = src[i];
            }
        }
    }


    /*************************************************************************
    Copy integer array
    *************************************************************************/
    public static void copyintegerarray(int[] src,
        ref int[] dst,
        xparams _params)
    {
        int i = 0;

        dst = new int[0];

        if (ap.len(src) > 0)
        {
            dst = new int[ap.len(src)];
            for (i = 0; i <= ap.len(src) - 1; i++)
            {
                dst[i] = src[i];
            }
        }
    }


    /*************************************************************************
    Copy real array
    *************************************************************************/
    public static void copyrealarray(double[] src,
        ref double[] dst,
        xparams _params)
    {
        int i = 0;

        dst = new double[0];

        if (ap.len(src) > 0)
        {
            dst = new double[ap.len(src)];
            for (i = 0; i <= ap.len(src) - 1; i++)
            {
                dst[i] = src[i];
            }
        }
    }


    /*************************************************************************
    Copy real matrix
    *************************************************************************/
    public static void copyrealmatrix(double[,] src,
        ref double[,] dst,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        dst = new double[0, 0];

        if (ap.rows(src) > 0 && ap.cols(src) > 0)
        {
            dst = new double[ap.rows(src), ap.cols(src)];
            for (i = 0; i <= ap.rows(src) - 1; i++)
            {
                for (j = 0; j <= ap.cols(src) - 1; j++)
                {
                    dst[i, j] = src[i, j];
                }
            }
        }
    }


    /*************************************************************************
    Clears integer array
    *************************************************************************/
    public static void unsetintegerarray(ref int[] a,
        xparams _params)
    {
        a = new int[0];

        a = new int[0];
    }


    /*************************************************************************
    Clears real array
    *************************************************************************/
    public static void unsetrealarray(ref double[] a,
        xparams _params)
    {
        a = new double[0];

        a = new double[0];
    }


    /*************************************************************************
    Clears real matrix
    *************************************************************************/
    public static void unsetrealmatrix(ref double[,] a,
        xparams _params)
    {
        a = new double[0, 0];

        a = new double[0, 0];
    }


    /*************************************************************************
    Initialize nbPool - prepare it to store N-length arrays, N>=0.
    Tries to reuse previously allocated memory as much as possible.
    *************************************************************************/
    public static void nbpoolinit(nbpool pool,
        int n,
        xparams _params)
    {
        ap.assert(n >= 0, "niPoolInit: N<0");
        pool.n = n;
        pool.temporariescount = 0;
        if (n == 0)
        {
            return;
        }
        if (ap.len(pool.seed0.val) != 0)
        {
            pool.seed0.val = new bool[0];
        }
        if (ap.len(pool.seedn.val) != n)
        {
            pool.seedn.val = new bool[n];
        }
        smp.ae_shared_pool_set_seed(pool.sourcepool, pool.seedn);
        smp.ae_shared_pool_set_seed(pool.temporarypool, pool.seed0);
    }


    /*************************************************************************
    Thread-safe retrieval of array from the nbPool. If there are enough arrays
    in the pool, it is performed without additional dynamic allocations.

    INPUT PARAMETERS:
        Pool        -   nbPool properly initialized with nbPoolInit
        A           -   array[0], must have exactly zero length (exception will
                        be generated if length is different from zero)

    OUTPUT PARAMETERS:
        A           -   array[N], contents undefined
    *************************************************************************/
    public static void nbpoolretrieve(nbpool pool,
        ref bool[] a,
        xparams _params)
    {
        sbooleanarray tmp = null;

        ap.assert(ap.len(a) == 0, "nbPoolRetrieve: A has non-zero length on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.sourcepool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.temporarypool, ref tmp);
        threadunsafeinc(ref pool.temporariescount, _params);
        if (pool.temporariescount > maxtemporariesinnpool)
        {
            pool.temporariescount = 0;
            smp.ae_shared_pool_clear_recycled(pool.temporarypool);
        }
    }


    /*************************************************************************
    Thread-safe recycling of N-length array to the nbPool.

    INPUT PARAMETERS:
        Pool        -   nbPool properly initialized with nbPoolInit
        A           -   array[N], length must be N exactly (exception will
                        be generated if length is different from N)

    OUTPUT PARAMETERS:
        A           -   array[0], length is exactly zero on exit
    *************************************************************************/
    public static void nbpoolrecycle(nbpool pool,
        ref bool[] a,
        xparams _params)
    {
        sbooleanarray tmp = null;

        ap.assert(ap.len(a) == pool.n, "nbPoolRecycle: A has length<>N on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.temporarypool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.sourcepool, ref tmp);
        threadunsafeincby(ref pool.temporariescount, -1, _params);
        if (pool.temporariescount < 0)
        {
            pool.temporariescount = 0;
        }
    }


    /*************************************************************************
    Initialize niPool - prepare it to store N-length arrays, N>=0.
    Tries to reuse previously allocated memory as much as possible.
    *************************************************************************/
    public static void nipoolinit(nipool pool,
        int n,
        xparams _params)
    {
        ap.assert(n >= 0, "niPoolInit: N<0");
        pool.n = n;
        pool.temporariescount = 0;
        if (n == 0)
        {
            return;
        }
        if (ap.len(pool.seed0.val) != 0)
        {
            pool.seed0.val = new int[0];
        }
        if (ap.len(pool.seedn.val) != n)
        {
            pool.seedn.val = new int[n];
        }
        smp.ae_shared_pool_set_seed(pool.sourcepool, pool.seedn);
        smp.ae_shared_pool_set_seed(pool.temporarypool, pool.seed0);
    }


    /*************************************************************************
    Thread-safe retrieval of array from the nrPool. If there are enough arrays
    in the pool, it is performed without additional dynamic allocations.

    INPUT PARAMETERS:
        Pool        -   niPool properly initialized with niPoolInit
        A           -   array[0], must have exactly zero length (exception will
                        be generated if length is different from zero)

    OUTPUT PARAMETERS:
        A           -   array[N], contents undefined
    *************************************************************************/
    public static void nipoolretrieve(nipool pool,
        ref int[] a,
        xparams _params)
    {
        sintegerarray tmp = null;

        ap.assert(ap.len(a) == 0, "niPoolRetrieve: A has non-zero length on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.sourcepool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.temporarypool, ref tmp);
        threadunsafeinc(ref pool.temporariescount, _params);
        if (pool.temporariescount > maxtemporariesinnpool)
        {
            pool.temporariescount = 0;
            smp.ae_shared_pool_clear_recycled(pool.temporarypool);
        }
    }


    /*************************************************************************
    Thread-safe recycling of N-length array to the niPool.

    INPUT PARAMETERS:
        Pool        -   niPool properly initialized with niPoolInit
        A           -   array[N], length must be N exactly (exception will
                        be generated if length is different from N)

    OUTPUT PARAMETERS:
        A           -   array[0], length is exactly zero on exit
    *************************************************************************/
    public static void nipoolrecycle(nipool pool,
        ref int[] a,
        xparams _params)
    {
        sintegerarray tmp = null;

        ap.assert(ap.len(a) == pool.n, "niPoolRecycle: A has length<>N on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.temporarypool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.sourcepool, ref tmp);
        threadunsafeincby(ref pool.temporariescount, -1, _params);
        if (pool.temporariescount < 0)
        {
            pool.temporariescount = 0;
        }
    }


    /*************************************************************************
    Initialize nrPool - prepare it to store N-length arrays, N>=0.
    Tries to reuse previously allocated memory as much as possible.
    *************************************************************************/
    public static void nrpoolinit(nrpool pool,
        int n,
        xparams _params)
    {
        ap.assert(n >= 0, "nrPoolInit: N<0");
        pool.n = n;
        pool.temporariescount = 0;
        if (n == 0)
        {
            return;
        }
        if (ap.len(pool.seed0.val) != 0)
        {
            pool.seed0.val = new double[0];
        }
        if (ap.len(pool.seedn.val) != n)
        {
            pool.seedn.val = new double[n];
        }
        smp.ae_shared_pool_set_seed(pool.sourcepool, pool.seedn);
        smp.ae_shared_pool_set_seed(pool.temporarypool, pool.seed0);
    }


    /*************************************************************************
    Thread-safe retrieval of array from the nrPool. If there are enough arrays
    in the pool, it is performed without additional dynamic allocations.

    INPUT PARAMETERS:
        Pool        -   nrPool properly initialized with nrPoolInit
        A           -   array[0], must have exactly zero length (exception will
                        be generated if length is different from zero)

    OUTPUT PARAMETERS:
        A           -   array[N], contents undefined
    *************************************************************************/
    public static void nrpoolretrieve(nrpool pool,
        ref double[] a,
        xparams _params)
    {
        srealarray tmp = null;

        ap.assert(ap.len(a) == 0, "nrPoolRetrieve: A has non-zero length on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.sourcepool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.temporarypool, ref tmp);
        threadunsafeinc(ref pool.temporariescount, _params);
        if (pool.temporariescount > maxtemporariesinnpool)
        {
            pool.temporariescount = 0;
            smp.ae_shared_pool_clear_recycled(pool.temporarypool);
        }
    }


    /*************************************************************************
    Thread-safe recycling of N-length array to the nrPool.

    INPUT PARAMETERS:
        Pool        -   nrPool properly initialized with nrPoolInit
        A           -   array[N], length must be N exactly (exception will
                        be generated if length is different from N)

    OUTPUT PARAMETERS:
        A           -   array[0], length is exactly zero on exit
    *************************************************************************/
    public static void nrpoolrecycle(nrpool pool,
        ref double[] a,
        xparams _params)
    {
        srealarray tmp = null;

        ap.assert(ap.len(a) == pool.n, "nrPoolRecycle: A has length<>N on entry");
        if (pool.n == 0)
        {
            return;
        }
        smp.ae_shared_pool_retrieve(pool.temporarypool, ref tmp);
        ap.swap(ref tmp.val, ref a);
        smp.ae_shared_pool_recycle(pool.sourcepool, ref tmp);
        threadunsafeincby(ref pool.temporariescount, -1, _params);
        if (pool.temporariescount < 0)
        {
            pool.temporariescount = 0;
        }
    }


    /*************************************************************************
    This function is used in parallel functions for recurrent division of large
    task into two smaller tasks.

    It has following properties:
    * it works only for TaskSize>=2 and TaskSize>TileSize (assertion is thrown otherwise)
    * Task0+Task1=TaskSize, Task0>0, Task1>0
    * Task0 and Task1 are close to each other
    * Task0>=Task1
    * Task0 is always divisible by TileSize

      -- ALGLIB --
         Copyright 07.04.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void tiledsplit(int tasksize,
        int tilesize,
        ref int task0,
        ref int task1,
        xparams _params)
    {
        int cc = 0;

        task0 = 0;
        task1 = 0;

        ap.assert(tasksize >= 2, "TiledSplit: TaskSize<2");
        ap.assert(tasksize > tilesize, "TiledSplit: TaskSize<=TileSize");
        cc = chunkscount(tasksize, tilesize, _params);
        ap.assert(cc >= 2, "TiledSplit: integrity check failed");
        task0 = idivup(cc, 2, _params) * tilesize;
        task1 = tasksize - task0;
        ap.assert(task0 >= 1, "TiledSplit: internal error");
        ap.assert(task1 >= 1, "TiledSplit: internal error");
        ap.assert(task0 % tilesize == 0, "TiledSplit: internal error");
        ap.assert(task0 >= task1, "TiledSplit: internal error");
    }


    /*************************************************************************
    This function searches integer array. Elements in this array are actually
    records, each NRec elements wide. Each record has unique header - NHeader
    integer values, which identify it. Records are lexicographically sorted by
    header.

    Records are identified by their index, not offset (offset = NRec*index).

    This function searches A (records with indices [I0,I1)) for a record with
    header B. It returns index of this record (not offset!), or -1 on failure.

      -- ALGLIB --
         Copyright 28.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int recsearch(int[] a,
        int nrec,
        int nheader,
        int i0,
        int i1,
        int[] b,
        xparams _params)
    {
        int result = 0;
        int mididx = 0;
        int cflag = 0;
        int k = 0;
        int offs = 0;

        result = -1;
        while (true)
        {
            if (i0 >= i1)
            {
                break;
            }
            mididx = (i0 + i1) / 2;
            offs = nrec * mididx;
            cflag = 0;
            for (k = 0; k <= nheader - 1; k++)
            {
                if (a[offs + k] < b[k])
                {
                    cflag = -1;
                    break;
                }
                if (a[offs + k] > b[k])
                {
                    cflag = 1;
                    break;
                }
            }
            if (cflag == 0)
            {
                result = mididx;
                return result;
            }
            if (cflag < 0)
            {
                i0 = mididx + 1;
            }
            else
            {
                i1 = mididx;
            }
        }
        return result;
    }


    /*************************************************************************
    This function is used in parallel functions for recurrent division of large
    task into two smaller tasks.

    It has following properties:
    * it works only for TaskSize>=2 (assertion is thrown otherwise)
    * for TaskSize=2, it returns Task0=1, Task1=1
    * in case TaskSize is odd,  Task0=TaskSize-1, Task1=1
    * in case TaskSize is even, Task0 and Task1 are approximately TaskSize/2
      and both Task0 and Task1 are even, Task0>=Task1

      -- ALGLIB --
         Copyright 07.04.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void splitlengtheven(int tasksize,
        ref int task0,
        ref int task1,
        xparams _params)
    {
        task0 = 0;
        task1 = 0;

        ap.assert(tasksize >= 2, "SplitLengthEven: TaskSize<2");
        if (tasksize == 2)
        {
            task0 = 1;
            task1 = 1;
            return;
        }
        if (tasksize % 2 == 0)
        {

            //
            // Even division
            //
            task0 = tasksize / 2;
            task1 = tasksize / 2;
            if (task0 % 2 != 0)
            {
                task0 = task0 + 1;
                task1 = task1 - 1;
            }
        }
        else
        {

            //
            // Odd task size, split trailing odd part from it.
            //
            task0 = tasksize - 1;
            task1 = 1;
        }
        ap.assert(task0 >= 1, "SplitLengthEven: internal error");
        ap.assert(task1 >= 1, "SplitLengthEven: internal error");
    }


    /*************************************************************************
    This function is used to calculate number of chunks (including partial,
    non-complete chunks) in some set. It expects that ChunkSize>=1, TaskSize>=0.
    Assertion is thrown otherwise.

    Function result is equivalent to Ceil(TaskSize/ChunkSize), but with guarantees
    that rounding errors won't ruin results.

      -- ALGLIB --
         Copyright 21.01.2015 by Bochkanov Sergey
    *************************************************************************/
    public static int chunkscount(int tasksize,
        int chunksize,
        xparams _params)
    {
        int result = 0;

        ap.assert(tasksize >= 0, "ChunksCount: TaskSize<0");
        ap.assert(chunksize >= 1, "ChunksCount: ChunkSize<1");
        result = tasksize / chunksize;
        if (tasksize % chunksize != 0)
        {
            result = result + 1;
        }
        return result;
    }


    /*************************************************************************
    Returns maximum density for level 2 sparse/dense functions. Density values
    below one returned by this function are better to handle via sparse Level 2
    functionality.

      -- ALGLIB routine --
         10.01.2019
         Bochkanov Sergey
    *************************************************************************/
    public static double sparselevel2density(xparams _params)
    {
        double result = 0;

        result = 0.1;
        return result;
    }


    /*************************************************************************
    Returns A-tile size for a matrix.

    A-tiles are smallest tiles (32x32), suitable for processing by ALGLIB  own
    implementation of Level 3 linear algebra.

      -- ALGLIB routine --
         10.01.2019
         Bochkanov Sergey
    *************************************************************************/
    public static int matrixtilesizea(xparams _params)
    {
        int result = 0;

        result = 32;
        return result;
    }


    /*************************************************************************
    Returns B-tile size for a matrix.

    B-tiles are larger  tiles (64x64), suitable for parallel execution or for
    processing by vendor's implementation of Level 3 linear algebra.

      -- ALGLIB routine --
         10.01.2019
         Bochkanov Sergey
    *************************************************************************/
    public static int matrixtilesizeb(xparams _params)
    {
        int result = 0;

        result = 64;
        return result;
    }


    /*************************************************************************
    This function returns minimum cost of task which is feasible for
    multithreaded processing. It returns real number in order to avoid overflow
    problems.

      -- ALGLIB --
         Copyright 10.01.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double smpactivationlevel(xparams _params)
    {
        double result = 0;
        double nn = 0;

        nn = 2 * matrixtilesizeb(_params);
        result = Math.Max(0.95 * 2 * nn * nn * nn, 1.0E7);
        return result;
    }


    /*************************************************************************
    This function returns minimum cost of task which is feasible for
    spawn (given that multithreading is active).

    It returns real number in order to avoid overflow problems.

      -- ALGLIB --
         Copyright 10.01.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double spawnlevel(xparams _params)
    {
        double result = 0;
        double nn = 0;

        nn = 2 * matrixtilesizea(_params);
        result = 0.95 * 2 * nn * nn * nn;
        return result;
    }


    /*************************************************************************
    --- OBSOLETE FUNCTION, USE TILED SPLIT INSTEAD --- 

    This function is used in parallel functions for recurrent division of large
    task into two smaller tasks.

    It has following properties:
    * it works only for TaskSize>=2 and ChunkSize>=2
      (assertion is thrown otherwise)
    * Task0+Task1=TaskSize, Task0>0, Task1>0
    * Task0 and Task1 are close to each other
    * in case TaskSize>ChunkSize, Task0 is always divisible by ChunkSize

      -- ALGLIB --
         Copyright 07.04.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void splitlength(int tasksize,
        int chunksize,
        ref int task0,
        ref int task1,
        xparams _params)
    {
        task0 = 0;
        task1 = 0;

        ap.assert(chunksize >= 2, "SplitLength: ChunkSize<2");
        ap.assert(tasksize >= 2, "SplitLength: TaskSize<2");
        task0 = tasksize / 2;
        if (task0 > chunksize && task0 % chunksize != 0)
        {
            task0 = task0 - task0 % chunksize;
        }
        task1 = tasksize - task0;
        ap.assert(task0 >= 1, "SplitLength: internal error");
        ap.assert(task1 >= 1, "SplitLength: internal error");
    }


    /*************************************************************************
    Outputs vector A[I0,I1-1] to trace log using either:
    a)  6-digit exponential format (no trace flags is set)
    b) 15-ditit exponential format ('PREC.E15' trace flag is set)
    b)  6-ditit fixed-point format ('PREC.F6' trace flag is set)

    This function checks trace flags every time it is called.
    *************************************************************************/
    public static void tracevectorautoprec(double[] a,
        int i0,
        int i1,
        xparams _params)
    {
        int i = 0;
        int prectouse = 0;


        //
        // Determine precision to use
        //
        prectouse = 0;
        if (ap.istraceenabled("PREC.E15", _params))
        {
            prectouse = 1;
        }
        if (ap.istraceenabled("PREC.F6", _params))
        {
            prectouse = 2;
        }

        //
        // Output
        //
        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            if (prectouse == 0)
            {
                ap.trace(System.String.Format("{0,14:E6}", a[i]));
            }
            if (prectouse == 1)
            {
                ap.trace(System.String.Format("{0,23:E15}", a[i]));
            }
            if (prectouse == 2)
            {
                ap.trace(System.String.Format("{0,13:F6}", a[i]));
            }
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs row A[I,J0..J1-1] to trace log using either:
    a)  6-digit exponential format (no trace flags is set)
    b) 15-ditit exponential format ('PREC.E15' trace flag is set)
    b)  6-ditit fixed-point format ('PREC.F6' trace flag is set)

    This function checks trace flags every time it is called.
    *************************************************************************/
    public static void tracerowautoprec(double[,] a,
        int i,
        int j0,
        int j1,
        xparams _params)
    {
        int j = 0;
        int prectouse = 0;


        //
        // Determine precision to use
        //
        prectouse = 0;
        if (ap.istraceenabled("PREC.E15", _params))
        {
            prectouse = 1;
        }
        if (ap.istraceenabled("PREC.F6", _params))
        {
            prectouse = 2;
        }

        //
        // Output
        //
        ap.trace("[ ");
        for (j = j0; j <= j1 - 1; j++)
        {
            if (prectouse == 0)
            {
                ap.trace(System.String.Format("{0,14:E6}", a[i, j]));
            }
            if (prectouse == 1)
            {
                ap.trace(System.String.Format("{0,23:E15}", a[i, j]));
            }
            if (prectouse == 2)
            {
                ap.trace(System.String.Format("{0,13:F6}", a[i, j]));
            }
            if (j < j1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Unscales/unshifts vector A[N] by computing A*Scl+Sft and outputs result to
    trace log using either:
    a)  6-digit exponential format (no trace flags is set)
    b) 15-ditit exponential format ('PREC.E15' trace flag is set)
    b)  6-ditit fixed-point format ('PREC.F6' trace flag is set)

    This function checks trace flags every time it is called.
    Both Scl and Sft can be omitted.
    *************************************************************************/
    public static void tracevectorunscaledunshiftedautoprec(double[] x,
        int n,
        double[] scl,
        bool applyscl,
        double[] sft,
        bool applysft,
        xparams _params)
    {
        int i = 0;
        int prectouse = 0;
        double v = 0;


        //
        // Determine precision to use
        //
        prectouse = 0;
        if (ap.istraceenabled("PREC.E15", _params))
        {
            prectouse = 1;
        }
        if (ap.istraceenabled("PREC.F6", _params))
        {
            prectouse = 2;
        }

        //
        // Output
        //
        ap.trace("[ ");
        for (i = 0; i <= n - 1; i++)
        {
            v = x[i];
            if (applyscl)
            {
                v = v * scl[i];
            }
            if (applysft)
            {
                v = v + sft[i];
            }
            if (prectouse == 0)
            {
                ap.trace(System.String.Format("{0,14:E6}", v));
            }
            if (prectouse == 1)
            {
                ap.trace(System.String.Format("{0,23:E15}", v));
            }
            if (prectouse == 2)
            {
                ap.trace(System.String.Format("{0,13:F6}", v));
            }
            if (i < n - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs vector of 1-norms of rows [I0,I1-1] of A[I0...I1-1,J0...J1-1]   to
    trace log using either:
    a)  6-digit exponential format (no trace flags is set)
    b) 15-ditit exponential format ('PREC.E15' trace flag is set)
    b)  6-ditit fixed-point format ('PREC.F6' trace flag is set)

    This function checks trace flags every time it is called.
    *************************************************************************/
    public static void tracerownrm1autoprec(double[,] a,
        int i0,
        int i1,
        int j0,
        int j1,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        int prectouse = 0;


        //
        // Determine precision to use
        //
        prectouse = 0;
        if (ap.istraceenabled("PREC.E15", _params))
        {
            prectouse = 1;
        }
        if (ap.istraceenabled("PREC.F6", _params))
        {
            prectouse = 2;
        }

        //
        // Output
        //
        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            v = 0;
            for (j = j0; j <= j1 - 1; j++)
            {
                v = Math.Max(v, Math.Abs(a[i, j]));
            }
            if (prectouse == 0)
            {
                ap.trace(System.String.Format("{0,14:E6}", v));
            }
            if (prectouse == 1)
            {
                ap.trace(System.String.Format("{0,23:E15}", v));
            }
            if (prectouse == 2)
            {
                ap.trace(System.String.Format("{0,13:F6}", v));
            }
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs vector A[I0,I1-1] to trace log using E3 precision
    *************************************************************************/
    public static void tracevectore3(double[] a,
        int i0,
        int i1,
        xparams _params)
    {
        int i = 0;

        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            ap.trace(System.String.Format("{0,11:E3}", a[i]));
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs vector A[I0,I1-1] to trace log using E6 precision
    *************************************************************************/
    public static void tracevectore6(double[] a,
        int i0,
        int i1,
        xparams _params)
    {
        int i = 0;

        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            ap.trace(System.String.Format("{0,14:E6}", a[i]));
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs vector A[I0,I1-1] to trace log using E8 or E15 precision
    *************************************************************************/
    public static void tracevectore615(double[] a,
        int i0,
        int i1,
        bool usee15,
        xparams _params)
    {
        int i = 0;

        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            if (usee15)
            {
                ap.trace(System.String.Format("{0,23:E15}", a[i]));
            }
            else
            {
                ap.trace(System.String.Format("{0,14:E6}", a[i]));
            }
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs vector of 1-norms of rows [I0,I1-1] of A[I0...I1-1,J0...J1-1]   to
    trace log using E8 precision
    *************************************************************************/
    public static void tracerownrm1e6(double[,] a,
        int i0,
        int i1,
        int j0,
        int j1,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;

        ap.trace("[ ");
        for (i = i0; i <= i1 - 1; i++)
        {
            v = 0;
            for (j = j0; j <= j1 - 1; j++)
            {
                v = Math.Max(v, Math.Abs(a[i, j]));
            }
            ap.trace(System.String.Format("{0,14:E6}", v));
            if (i < i1 - 1)
            {
                ap.trace(" ");
            }
        }
        ap.trace(" ]");
    }


    /*************************************************************************
    Outputs specified number of spaces
    *************************************************************************/
    public static void tracespaces(int cnt,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= cnt - 1; i++)
        {
            ap.trace(" ");
        }
    }


    /*************************************************************************
    Minimum speedup feasible for multithreading
    *************************************************************************/
    public static double minspeedup(xparams _params)
    {
        double result = 0;

        result = 1.5;
        return result;
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Maximum concurrency on given system, with given compilation settings
        *************************************************************************/
        public static int maxconcurrency(xparams _params)
        {
            int result = 0;

            result = 1;
            return result;
        }
#endif


    /*************************************************************************
    Initialize SAvgCounter

    Prior value is a value that is returned when no values are in the buffer
    *************************************************************************/
    public static void savgcounterinit(savgcounter c,
        double priorvalue,
        xparams _params)
    {
        c.rsum = 0;
        c.rcnt = 0;
        c.prior = priorvalue;
    }


    /*************************************************************************
    Enqueue value into SAvgCounter
    *************************************************************************/
    public static void savgcounterenqueue(savgcounter c,
        double v,
        xparams _params)
    {
        c.rsum = c.rsum + v;
        c.rcnt = c.rcnt + 1;
    }


    /*************************************************************************
    Enqueue value into SAvgCounter
    *************************************************************************/
    public static double savgcounterget(savgcounter c,
        xparams _params)
    {
        double result = 0;

        if ((double)(c.rcnt) == (double)(0))
        {
            result = c.prior;
        }
        else
        {
            result = c.rsum / c.rcnt;
        }
        return result;
    }


    /*************************************************************************
    Initialize SQuantileCounter

    Prior value is a value that is returned when no values are in the buffer
    *************************************************************************/
    public static void squantilecounterinit(squantilecounter c,
        double priorvalue,
        xparams _params)
    {
        c.cnt = 0;
        c.prior = priorvalue;
    }


    /*************************************************************************
    Enqueue value into SQuantileCounter
    *************************************************************************/
    public static void squantilecounterenqueue(squantilecounter c,
        double v,
        xparams _params)
    {
        if (ap.len(c.elems) == c.cnt)
        {
            rvectorresize(ref c.elems, 2 * c.cnt + 1, _params);
        }
        c.elems[c.cnt] = v;
        c.cnt = c.cnt + 1;
    }


    /*************************************************************************
    Get k-th quantile. Thread-unsafe, modifies internal structures.

    0<=Q<=1.
    *************************************************************************/
    public static double squantilecounterget(squantilecounter c,
        double q,
        xparams _params)
    {
        double result = 0;
        int left = 0;
        int right = 0;
        int k = 0;
        int pivotindex = 0;
        double pivotvalue = 0;
        int storeindex = 0;
        int i = 0;

        ap.assert((double)(q) >= (double)(0) && (double)(q) <= (double)(1), "SQuantileCounterGet: incorrect Q");
        if (c.cnt == 0)
        {
            result = c.prior;
            return result;
        }
        if (c.cnt == 1)
        {
            result = c.elems[0];
            return result;
        }
        k = (int)Math.Round(q * (c.cnt - 1));
        left = 0;
        right = c.cnt - 1;
        while (true)
        {
            if (left == right)
            {
                result = c.elems[left];
                return result;
            }
            pivotindex = left + (right - left) / 2;
            pivotvalue = c.elems[pivotindex];
            swapelements(c.elems, pivotindex, right, _params);
            storeindex = left;
            for (i = left; i <= right - 1; i++)
            {
                if ((double)(c.elems[i]) < (double)(pivotvalue))
                {
                    swapelements(c.elems, storeindex, i, _params);
                    storeindex = storeindex + 1;
                }
            }
            swapelements(c.elems, storeindex, right, _params);
            pivotindex = storeindex;
            if (pivotindex == k)
            {
                result = c.elems[k];
                return result;
            }
            if (k < pivotindex)
            {
                right = pivotindex - 1;
            }
            else
            {
                left = pivotindex + 1;
            }
        }
        return result;
    }
}
