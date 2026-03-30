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

public class xdebug
{
    /*************************************************************************
    This is a debug class intended for testing ALGLIB interface generator.
    Never use it in any real life project.

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    public class xdebugrecord1 : apobject
    {
        public int i;
        public complex c;
        public double[] a;
        public xdebugrecord1()
        {
            init();
        }
        public override void init()
        {
            a = new double[0];
        }
        public override apobject make_copy()
        {
            xdebugrecord1 _result = new xdebugrecord1();
            _result.i = i;
            _result.c = c;
            _result.a = (double[])a.Clone();
            return _result;
        }
    };




    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Creates and returns XDebugRecord1 structure:
    * integer and complex fields of Rec1 are set to 1 and 1+i correspondingly
    * array field of Rec1 is set to [2,3]

      -- ALGLIB --
         Copyright 27.05.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebuginitrecord1(xdebugrecord1 rec1,
        xparams _params)
    {
        rec1.i = 1;
        rec1.c.x = 1;
        rec1.c.y = 1;
        rec1.a = new double[2];
        rec1.a[0] = 2;
        rec1.a[1] = 3;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Creates and returns XDebugRecord1 structure:
    * integer and complex fields of Rec1 are set to 1 and 1+i correspondingly
    * array field of Rec1 is set to [2,3]

      -- ALGLIB --
         Copyright 27.05.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugupdaterecord1(xdebugrecord1 rec1,
        xparams _params)
    {
        rec1.i = rec1.i + 1;
        rec1.c.x = rec1.c.x + 2;
        rec1.c.y = rec1.c.y + 3;
        apserv.rvectorresize(ref rec1.a, ap.len(rec1.a) + 1, _params);
        rec1.a[ap.len(rec1.a) - 1] = rec1.a[ap.len(rec1.a) - 2] + 3;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Counts number of True values in the boolean 1D array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static int xdebugb1count(bool[] a,
        xparams _params)
    {
        int result = 0;
        int i = 0;

        result = 0;
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            if (a[i])
            {
                result = result + 1;
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by NOT(a[i]).
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb1not(bool[] a,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = !a[i];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Appends copy of array to itself.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb1appendcopy(ref bool[] a,
        xparams _params)
    {
        int i = 0;
        bool[] b = new bool[0];

        b = new bool[ap.len(a)];
        for (i = 0; i <= ap.len(b) - 1; i++)
        {
            b[i] = a[i];
        }
        a = new bool[2 * ap.len(b)];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = b[i % ap.len(b)];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate N-element array with even-numbered elements set to True.
    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb1outeven(int n,
        ref bool[] a,
        xparams _params)
    {
        int i = 0;

        a = new bool[0];

        a = new bool[n];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = i % 2 == 0;
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static int xdebugi1sum(int[] a,
        xparams _params)
    {
        int result = 0;
        int i = 0;

        result = 0;
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            result = result + a[i];
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -A[I]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi1neg(int[] a,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = -a[i];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Appends copy of array to itself.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi1appendcopy(ref int[] a,
        xparams _params)
    {
        int i = 0;
        int[] b = new int[0];

        b = new int[ap.len(a)];
        for (i = 0; i <= ap.len(b) - 1; i++)
        {
            b[i] = a[i];
        }
        a = new int[2 * ap.len(b)];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = b[i % ap.len(b)];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate N-element array with even-numbered A[I] set to I, and odd-numbered
    ones set to 0.

    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi1outeven(int n,
        ref int[] a,
        xparams _params)
    {
        int i = 0;

        a = new int[0];

        a = new int[n];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            if (i % 2 == 0)
            {
                a[i] = i;
            }
            else
            {
                a[i] = 0;
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static double xdebugr1sum(double[] a,
        xparams _params)
    {
        double result = 0;
        int i = 0;

        result = 0;
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            result = result + a[i];
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

    Internally it creates a copy of the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static double xdebugr1internalcopyandsum(double[] a,
        xparams _params)
    {
        double result = 0;
        int i = 0;

        a = (double[])a.Clone();

        result = 0;
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            result = result + a[i];
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -A[I]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr1neg(double[] a,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = -a[i];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Appends copy of array to itself.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr1appendcopy(ref double[] a,
        xparams _params)
    {
        int i = 0;
        double[] b = new double[0];

        b = new double[ap.len(a)];
        for (i = 0; i <= ap.len(b) - 1; i++)
        {
            b[i] = a[i];
        }
        a = new double[2 * ap.len(b)];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = b[i % ap.len(b)];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate N-element array with even-numbered A[I] set to I*0.25,
    and odd-numbered ones are set to 0.

    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr1outeven(int n,
        ref double[] a,
        xparams _params)
    {
        int i = 0;

        a = new double[0];

        a = new double[n];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            if (i % 2 == 0)
            {
                a[i] = i * 0.25;
            }
            else
            {
                a[i] = 0;
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static complex xdebugc1sum(complex[] a,
        xparams _params)
    {
        complex result = 0;
        int i = 0;

        result = 0;
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            result = result + a[i];
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -A[I]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc1neg(complex[] a,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = -a[i];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Appends copy of array to itself.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc1appendcopy(ref complex[] a,
        xparams _params)
    {
        int i = 0;
        complex[] b = new complex[0];

        b = new complex[ap.len(a)];
        for (i = 0; i <= ap.len(b) - 1; i++)
        {
            b[i] = a[i];
        }
        a = new complex[2 * ap.len(b)];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            a[i] = b[i % ap.len(b)];
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate N-element array with even-numbered A[K] set to (x,y) = (K*0.25, K*0.125)
    and odd-numbered ones are set to 0.

    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc1outeven(int n,
        ref complex[] a,
        xparams _params)
    {
        int i = 0;

        a = new complex[0];

        a = new complex[n];
        for (i = 0; i <= ap.len(a) - 1; i++)
        {
            if (i % 2 == 0)
            {
                a[i].x = i * 0.250;
                a[i].y = i * 0.125;
            }
            else
            {
                a[i] = 0;
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Counts number of True values in the boolean 2D array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static int xdebugb2count(bool[,] a,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;

        result = 0;
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                if (a[i, j])
                {
                    result = result + 1;
                }
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by NOT(a[i]).
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb2not(bool[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = !a[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Transposes array.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb2transpose(ref bool[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        bool[,] b = new bool[0, 0];

        b = new bool[ap.rows(a), ap.cols(a)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                b[i, j] = a[i, j];
            }
        }
        a = new bool[ap.cols(b), ap.rows(b)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                a[j, i] = b[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate MxN matrix with elements set to "Sin(3*I+5*J)>0"
    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugb2outsin(int m,
        int n,
        ref bool[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new bool[0, 0];

        a = new bool[m, n];
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = (double)(Math.Sin(3 * i + 5 * j)) > (double)(0);
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static int xdebugi2sum(int[,] a,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;

        result = 0;
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                result = result + a[i, j];
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -a[i,j]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi2neg(int[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = -a[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Transposes array.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi2transpose(ref int[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int[,] b = new int[0, 0];

        b = new int[ap.rows(a), ap.cols(a)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                b[i, j] = a[i, j];
            }
        }
        a = new int[ap.cols(b), ap.rows(b)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                a[j, i] = b[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate MxN matrix with elements set to "Sign(Sin(3*I+5*J))"
    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugi2outsin(int m,
        int n,
        ref int[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new int[0, 0];

        a = new int[m, n];
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = Math.Sign(Math.Sin(3 * i + 5 * j));
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static double xdebugr2sum(double[,] a,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;

        result = 0;
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                result = result + a[i, j];
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

    Internally it creates a copy of a.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static double xdebugr2internalcopyandsum(double[,] a,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;

        a = (double[,])a.Clone();

        result = 0;
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                result = result + a[i, j];
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -a[i,j]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr2neg(double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = -a[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Transposes array.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr2transpose(ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[,] b = new double[0, 0];

        b = new double[ap.rows(a), ap.cols(a)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                b[i, j] = a[i, j];
            }
        }
        a = new double[ap.cols(b), ap.rows(b)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                a[j, i] = b[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate MxN matrix with elements set to "Sin(3*I+5*J)"
    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugr2outsin(int m,
        int n,
        ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new double[0, 0];

        a = new double[m, n];
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = Math.Sin(3 * i + 5 * j);
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of elements in the array.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static complex xdebugc2sum(complex[,] a,
        xparams _params)
    {
        complex result = 0;
        int i = 0;
        int j = 0;

        result = 0;
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                result = result + a[i, j];
            }
        }
        return result;
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Replace all values in array by -a[i,j]
    Array is passed using "shared" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc2neg(complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j] = -a[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Transposes array.
    Array is passed using "var" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc2transpose(ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex[,] b = new complex[0, 0];

        b = new complex[ap.rows(a), ap.cols(a)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                b[i, j] = a[i, j];
            }
        }
        a = new complex[ap.cols(b), ap.rows(b)];
        for (i = 0; i <= ap.rows(b) - 1; i++)
        {
            for (j = 0; j <= ap.cols(b) - 1; j++)
            {
                a[j, i] = b[i, j];
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Generate MxN matrix with elements set to "Sin(3*I+5*J),Cos(3*I+5*J)"
    Array is passed using "out" convention.

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void xdebugc2outsincos(int m,
        int n,
        ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new complex[0, 0];

        a = new complex[m, n];
        for (i = 0; i <= ap.rows(a) - 1; i++)
        {
            for (j = 0; j <= ap.cols(a) - 1; j++)
            {
                a[i, j].x = Math.Sin(3 * i + 5 * j);
                a[i, j].y = Math.Cos(3 * i + 5 * j);
            }
        }
    }


    /*************************************************************************
    This is debug function intended for testing ALGLIB interface generator.
    Never use it in any real life project.

    Returns sum of a[i,j]*(1+b[i,j]) such that c[i,j] is True

      -- ALGLIB --
         Copyright 11.10.2013 by Bochkanov Sergey
    *************************************************************************/
    public static double xdebugmaskedbiasedproductsum(int m,
        int n,
        double[,] a,
        double[,] b,
        bool[,] c,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;

        ap.assert(m >= ap.rows(a));
        ap.assert(m >= ap.rows(b));
        ap.assert(m >= ap.rows(c));
        ap.assert(n >= ap.cols(a));
        ap.assert(n >= ap.cols(b));
        ap.assert(n >= ap.cols(c));
        result = 0.0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (c[i, j])
                {
                    result = result + a[i, j] * (1 + b[i, j]);
                }
            }
        }
        return result;
    }


}
