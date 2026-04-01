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

using System;

namespace Simargl.Algorithms.Raw;

public partial class ablasf
{
#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Computes dot product (X,Y) for elements [0,N) of X[] and Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            Y       -   array[N], vector to process

        RESULT:
            (X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotv(int n,
            double[] x,
            double[] y,
            xparams _params)
        {
            double result = 0;
            int i = 0;

            result = 0;
            for(i=0; i<=n-1; i++)
            {
                result = result+x[i]*y[i];
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Computes dot product (X,A[i]) for elements [0,N) of vector X[] and row A[i,*]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            A       -   array[?,N], matrix to process
            I       -   row index

        RESULT:
            (X,Ai)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotvr(int n,
            double[] x,
            double[,] a,
            int i,
            xparams _params)
        {
            double result = 0;
            int j = 0;

            result = 0;
            for(j=0; j<=n-1; j++)
            {
                result = result+x[j]*a[i,j];
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Computes dot product (X,A[i]) for rows A[ia,*] and B[ib,*]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            A       -   array[?,N], matrix to process
            I       -   row index

        RESULT:
            (X,Ai)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotrr(int n,
            double[,] a,
            int ia,
            double[,] b,
            int ib,
            xparams _params)
        {
            double result = 0;
            int j = 0;

            result = 0;
            for(j=0; j<=n-1; j++)
            {
                result = result+a[ia,j]*b[ib,j];
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Computes dot product (X,X) for elements [0,N) of X[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        RESULT:
            (X,X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotv2(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            result = 0;
            for(i=0; i<=n-1; i++)
            {
                v = x[i];
                result = result+v*v;
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of Y[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddv(int n,
            double alpha,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]+alpha*y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of Y[]*Z[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X + Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]+y[i]*z[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace subtraction of Y[]*Z[] from X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X - Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rnegmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]-y[i]*z[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs addition of Y[]*Z[] to X[], with result being stored to R[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process
            R       -   array[N], vector to process

        RESULT:
            R := X + Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            double[] r,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                r[i] = x[i]+y[i]*z[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs subtraction of Y[]*Z[] from X[], with result being stored to R[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process
            R       -   array[N], vector to process

        RESULT:
            R := X - Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopynegmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            double[] r,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                r[i] = x[i]-y[i]*z[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of Y[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   source vector
            OffsY   -   source offset
            X       -   destination vector
            OffsX   -   destination offset

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddvx(int n,
            double alpha,
            double[] y,
            int offsy,
            double[] x,
            int offsx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[offsx+i] = x[offsx+i]+alpha*y[offsy+i];
            }
        }
#endif


    /*************************************************************************
    Performs inplace addition of vector Y[] to column X[]

    INPUT PARAMETERS:
        N       -   vector length
        Alpha   -   multiplier
        Y       -   vector to add
        X       -   target column ColIdx

    RESULT:
        X := X + alpha*Y

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void raddvc(int n,
        double alpha,
        double[] y,
        double[,] x,
        int colidx,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= n - 1; i++)
        {
            x[i, colidx] = x[i, colidx] + alpha * y[i];
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of vector Y[] to row X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   vector to add
            X       -   target row RowIdx

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddvr(int n,
            double alpha,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = x[rowidx,i]+alpha*y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise multiplication of vector X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]*y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise multiplication of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = x[rowidx,i]*y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise multiplication of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulrv(int n,
            double[,] y,
            int rowidx,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]*y[rowidx,i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise division of vector X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target vector

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]/y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise division of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = x[rowidx,i]/y[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise division of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivrv(int n,
            double[,] y,
            int rowidx,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]/y[rowidx,i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of vector X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = Math.Max(x[i], y[i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of row X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = Math.Max(x[rowidx,i], y[i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of row X[I] and vector Y[] 

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix, I-th row is source
            X       -   target row RowIdx

        RESULT:
            Y := componentwise_max(Y,X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxrv(int n,
            double[,] x,
            int rowidx,
            double[] y,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                y[i] = Math.Max(y[i], x[rowidx,i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of vector X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = Math.Min(x[i], y[i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of row X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = Math.Min(x[rowidx,i], y[i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs componentwise max of row X[I] and vector Y[] 

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix, I-th row is source
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminrv(int n,
            double[,] x,
            int rowidx,
            double[] y,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                y[i] = Math.Min(y[i], x[rowidx,i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of Y[RIdx,...] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[?,N], matrix whose RIdx-th row is added
            RIdx    -   row index
            X       -   array[N], vector to process

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddrv(int n,
            double alpha,
            double[,] y,
            int ridx,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]+alpha*y[ridx,i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace addition of Y[RIdx,...] to X[RIdxDst]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[?,N], matrix whose RIdxSrc-th row is added
            RIdxSrc -   source row index
            X       -   array[?,N], matrix whose RIdxDst-th row is target
            RIdxDst -   destination row index

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddrr(int n,
            double alpha,
            double[,] y,
            int ridxsrc,
            double[,] x,
            int ridxdst,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[ridxdst,i] = x[ridxdst,i]+alpha*y[ridxsrc,i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace multiplication of X[] by V

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulv(int n,
            double v,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = x[i]*v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace multiplication of X[] by V

        INPUT PARAMETERS:
            N       -   row length
            X       -   array[?,N], row to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 of row RowIdx are multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulr(int n,
            double v,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = x[rowidx,i]*v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace computation of Sqrt(X)

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 replaced by Sqrt(X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsqrtv(int n,
            double[] x,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[i] = Math.Sqrt(x[i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace computation of Sqrt(X[RowIdx,*])

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[?,N], matrix to process

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 replaced by Sqrt(X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsqrtr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[rowidx,i] = Math.Sqrt(x[rowidx,i]);
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs inplace multiplication of X[OffsX:OffsX+N-1] by V

        INPUT PARAMETERS:
            N       -   subvector length
            X       -   vector to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements OffsX:OffsX+N-1 multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulvx(int n,
            double v,
            double[] x,
            int offsx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                x[offsx+i] = x[offsx+i]*v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Returns maximum X

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            max(X[i])
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxv(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            if( n<=0 )
            {
                result = 0;
                return result;
            }
            result = x[0];
            for(i=1; i<=n-1; i++)
            {
                v = x[i];
                if( v>result )
                {
                    result = v;
                }
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Returns maximum |X|

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            max(|X[i]|)
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxabsv(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            result = 0;
            for(i=0; i<=n-1; i++)
            {
                v = Math.Abs(x[i]);
                if( v>result )
                {
                    result = v;
                }
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Returns maximum X

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix to process, RowIdx-th row is processed

        OUTPUT PARAMETERS:
            max(X[RowIdx,i])
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            if( n<=0 )
            {
                result = 0;
                return result;
            }
            result = x[rowidx,0];
            for(i=1; i<=n-1; i++)
            {
                v = x[rowidx,i];
                if( v>result )
                {
                    result = v;
                }
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Returns maximum |X|

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix to process, RowIdx-th row is processed

        OUTPUT PARAMETERS:
            max(|X[RowIdx,i]|)
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxabsr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            result = 0;
            for(i=0; i<=n-1; i++)
            {
                v = Math.Abs(x[rowidx,i]);
                if( v>result )
                {
                    result = v;
                }
            }
            return result;
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetv(int n,
            double v,
            double[] x,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                x[j] = v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets X[OffsX:OffsX+N-1] to V

        INPUT PARAMETERS:
            N       -   subvector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   X[OffsX:OffsX+N-1] is replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetvx(int n,
            double v,
            double[] x,
            int offsx,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                x[offsx+j] = v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void isetv(int n,
            int v,
            int[] x,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                x[j] = v;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void bsetv(int n,
            bool v,
            bool[] x,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                x[j] = v;
            }
        }
#endif


    /*************************************************************************
    Sets vector X[] to V

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        X       -   array[N]

    OUTPUT PARAMETERS:
        X       -   leading N elements are replaced by V

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void csetv(int n,
        complex v,
        complex[] x,
        xparams _params)
    {
        int j = 0;

        for (j = 0; j <= n - 1; j++)
        {
            x[j].x = v.x;
            x[j].y = v.y;
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets matrix A[] to V

        INPUT PARAMETERS:
            M, N    -   rows/cols count
            V       -   value to set
            A       -   array[M,N]

        OUTPUT PARAMETERS:
            A       -   leading M rows, N cols are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetm(int m,
            int n,
            double v,
            double[,] a,
            xparams _params)
        {
            int i = 0;
            int j = 0;

            for(i=0; i<=m-1; i++)
            {
                for(j=0; j<=n-1; j++)
                {
                    a[i,j] = v;
                }
            }
        }
#endif


    /*************************************************************************
    Sets vector X[] to V, reallocating X[] if too small

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   leading N elements are replaced by V; array is reallocated
                    if its length is less than N.

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rsetallocv(int n,
        double v,
        ref double[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new double[n];
        }
        rsetv(n, v, x, _params);
    }


    /*************************************************************************
    Sets vector A[] to V, reallocating A[] if too small.

    INPUT PARAMETERS:
        M       -   rows count
        N       -   cols count
        V       -   value to set
        A       -   possibly preallocated matrix

    OUTPUT PARAMETERS:
        A       -   leading M rows, N cols are replaced by V; the matrix is
                    reallocated if its rows/cols count is less than M/N.

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rsetallocm(int m,
        int n,
        double v,
        ref double[,] a,
        xparams _params)
    {
        if (ap.rows(a) < m || ap.cols(a) < n)
        {
            a = new double[m, n];
        }
        rsetm(m, n, v, a, _params);
    }


    /*************************************************************************
    Reallocates X[] if its length is less than required value. Does not change
    its length and contents if it is large enough.

    INPUT PARAMETERS:
        N       -   desired vector length
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   length(X)>=N

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rallocv(int n,
        ref double[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new double[n];
        }
    }


    /*************************************************************************
    Reallocates X[] if its length is less than required value. Does not change
    its length and contents if it is large enough.

    INPUT PARAMETERS:
        N       -   desired vector length
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   length(X)>=N

      -- ALGLIB --
         Copyright 20.07.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void callocv(int n,
        ref complex[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new complex[n];
        }
    }


    /*************************************************************************
    Reallocates X[] if its length is less than required value. Does not change
    its length and contents if it is large enough.

    INPUT PARAMETERS:
        N       -   desired vector length
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   length(X)>=N

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void iallocv(int n,
        ref int[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new int[n];
        }
    }


    /*************************************************************************
    Reallocates X[] if its length is less than required value. Does not change
    its length and contents if it is large enough.

    INPUT PARAMETERS:
        N       -   desired vector length
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   length(X)>=N

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void ballocv(int n,
        ref bool[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new bool[n];
        }
    }


    /*************************************************************************
    Reallocates matrix if its rows or cols count is less than  required.  Does
    not change its size if it is exactly that size or larger.

    INPUT PARAMETERS:
        M       -   rows count
        N       -   cols count
        A       -   possibly preallocated matrix

    OUTPUT PARAMETERS:
        A       -   size is at least M*N

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rallocm(int m,
        int n,
        ref double[,] a,
        xparams _params)
    {
        if (ap.rows(a) < m || ap.cols(a) < n)
        {
            a = new double[m, n];
        }
    }


    /*************************************************************************
    Sets vector X[] to V, reallocating X[] if too small

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   leading N elements are replaced by V; array is reallocated
                    if its length is less than N.

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void isetallocv(int n,
        int v,
        ref int[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new int[n];
        }
        isetv(n, v, x, _params);
    }


    /*************************************************************************
    Sets vector X[] to V, reallocating X[] if too small

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   leading N elements are replaced by V; array is reallocated
                    if its length is less than N.

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void bsetallocv(int n,
        bool v,
        ref bool[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new bool[n];
        }
        bsetv(n, v, x, _params);
    }


    /*************************************************************************
    Sets vector X[] to V, reallocating X[] if too small

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        X       -   possibly preallocated array

    OUTPUT PARAMETERS:
        X       -   leading N elements are replaced by V; array is reallocated
                    if its length is less than N.

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void csetallocv(int n,
        complex v,
        ref complex[] x,
        xparams _params)
    {
        if (ap.len(x) < n)
        {
            x = new complex[n];
        }
        csetv(n, v, x, _params);
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Sets row I of A[,] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            A       -   array[N,N] or larger
            I       -   row index

        OUTPUT PARAMETERS:
            A       -   leading N elements of I-th row are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetr(int n,
            double v,
            double[,] a,
            int i,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                a[i,j] = v;
            }
        }
#endif


    /*************************************************************************
    Sets col J of A[,] to V

    INPUT PARAMETERS:
        N       -   vector length
        V       -   value to set
        A       -   array[N,N] or larger
        J       -   col index

    OUTPUT PARAMETERS:
        A       -   leading N elements of I-th col are replaced by V

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rsetc(int n,
        double v,
        double[,] a,
        int j,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= n - 1; i++)
        {
            a[i, j] = v;
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   leading N elements are replaced by X

            
        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyv(int n,
            double[] x,
            double[] y,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                y[j] = x[j];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   leading N elements are replaced by X

            
        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void bcopyv(int n,
            bool[] x,
            bool[] y,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                y[j] = x[j];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to Y[], extended version

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            OffsX   -   source offset
            Y       -   preallocated array[N]
            OffsY   -   destination offset

        OUTPUT PARAMETERS:
            Y       -   N elements starting from OffsY are replaced by X[OffsX:OffsX+N-1]
            
        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyvx(int n,
            double[] x,
            int offsx,
            double[] y,
            int offsy,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                y[offsy+j] = x[offsx+j];
            }
        }
#endif


    /*************************************************************************
    Copies vector X[] to Y[], resizing Y[] if needed.

    INPUT PARAMETERS:
        N       -   vector length
        X       -   array[N], source
        Y       -   possibly preallocated array[N] (resized if needed)

    OUTPUT PARAMETERS:
        Y       -   leading N elements are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopyallocv(int n,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        if (ap.len(y) < n)
        {
            y = new double[n];
        }
        rcopyv(n, x, y, _params);
    }


    /*************************************************************************
    Copies matrix X[] to Y[], resizing Y[] if needed. On resize, dimensions of
    Y[] are increased - but not decreased.

    INPUT PARAMETERS:
        M       -   rows count
        N       -   cols count
        X       -   array[M,N], source
        Y       -   possibly preallocated array[M,N] (resized if needed)

    OUTPUT PARAMETERS:
        Y       -   leading [M,N] elements are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopym(int m,
        int n,
        double[,] x,
        double[,] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                y[i, j] = x[i, j];
            }
        }
    }


    /*************************************************************************
    Copies matrix X[] to Y[], resizing Y[] if needed. On resize, dimensions of
    Y[] are increased - but not decreased.

    INPUT PARAMETERS:
        M       -   rows count
        N       -   cols count
        X       -   array[M,N], source
        Y       -   possibly preallocated array[M,N] (resized if needed)

    OUTPUT PARAMETERS:
        Y       -   leading [M,N] elements are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopyallocm(int m,
        int n,
        double[,] x,
        ref double[,] y,
        xparams _params)
    {
        if (m == 0 || n == 0)
        {
            return;
        }
        if (ap.rows(y) < m || ap.cols(y) < n)
        {
            y = new double[Math.Max(m, ap.rows(y)), Math.Max(n, ap.cols(y))];
        }
        rcopym(m, n, x, y, _params);
    }


    /*************************************************************************
    Copies vector X[] to Y[], resizing Y[] if needed.

    INPUT PARAMETERS:
        N       -   vector length
        X       -   array[N], source
        Y       -   possibly preallocated array[N] (resized if needed)

    OUTPUT PARAMETERS:
        Y       -   leading N elements are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void icopyallocv(int n,
        int[] x,
        ref int[] y,
        xparams _params)
    {
        if (ap.len(y) < n)
        {
            y = new int[n];
        }
        icopyv(n, x, y, _params);
    }


    /*************************************************************************
    Copies vector X[] to Y[], resizing Y[] if needed.

    INPUT PARAMETERS:
        N       -   vector length
        X       -   array[N], source
        Y       -   possibly preallocated array[N] (resized if needed)

    OUTPUT PARAMETERS:
        Y       -   leading N elements are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void bcopyallocv(int n,
        bool[] x,
        ref bool[] y,
        xparams _params)
    {
        if (ap.len(y) < n)
        {
            y = new bool[n];
        }
        bcopyv(n, x, y, _params);
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   X copied to Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void icopyv(int n,
            int[] x,
            int[] y,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                y[j] = x[j];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to Y[], extended version

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            OffsX   -   source offset
            Y       -   preallocated array[N]
            OffsY   -   destination offset

        OUTPUT PARAMETERS:
            Y       -   N elements starting from OffsY are replaced by X[OffsX:OffsX+N-1]
            
        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void icopyvx(int n,
            int[] x,
            int offsx,
            int[] y,
            int offsy,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                y[offsy+j] = x[offsx+j];
            }
        }
#endif


    /*************************************************************************
    Grows X, i.e. changes its size in such a way that:
    a) contents is preserved
    b) new size is at least N
    c) actual size can be larger than N, so subsequent grow() calls can return
       without reallocation

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void igrowv(int newn,
        ref int[] x,
        xparams _params)
    {
        int[] oldx = new int[0];
        int oldn = 0;

        if (ap.len(x) >= newn)
        {
            return;
        }
        oldn = ap.len(x);
        newn = Math.Max(newn, (int)Math.Round(1.8 * oldn + 1));
        ap.swap(ref x, ref oldx);
        x = new int[newn];
        icopyv(oldn, oldx, x, _params);
    }


    /*************************************************************************
    Grows X, i.e. changes its size in such a way that:
    a) contents is preserved
    b) new size is at least N
    c) actual size can be larger than N, so subsequent grow() calls can return
       without reallocation

      -- ALGLIB --
         Copyright 20.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void rgrowv(int newn,
        ref double[] x,
        xparams _params)
    {
        double[] oldx = new double[0];
        int oldn = 0;

        if (ap.len(x) >= newn)
        {
            return;
        }
        oldn = ap.len(x);
        newn = Math.Max(newn, (int)Math.Round(1.8 * oldn + 1));
        ap.swap(ref x, ref oldx);
        x = new double[newn];
        rcopyv(oldn, oldx, x, _params);
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs copying with multiplication of V*X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            V       -   multiplier
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   array[N], Y = V*X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymulv(int n,
            double v,
            double[] x,
            double[] y,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                y[i] = v*x[i];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Performs copying with multiplication of V*X[] to Y[I,*]

        INPUT PARAMETERS:
            N       -   vector length
            V       -   multiplier
            X       -   array[N], source
            Y       -   preallocated array[?,N]
            RIdx    -   destination row index

        OUTPUT PARAMETERS:
            Y       -   Y[RIdx,...] = V*X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymulvr(int n,
            double v,
            double[] x,
            double[,] y,
            int ridx,
            xparams _params)
        {
            int i = 0;

            for(i=0; i<=n-1; i++)
            {
                y[ridx,i] = v*x[i];
            }
        }
#endif


    /*************************************************************************
    Performs copying with multiplication of V*X[] to Y[*,J]

    INPUT PARAMETERS:
        N       -   vector length
        V       -   multiplier
        X       -   array[N], source
        Y       -   preallocated array[N,?]
        CIdx    -   destination rocol index

    OUTPUT PARAMETERS:
        Y       -   Y[RIdx,...] = V*X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopymulvc(int n,
        double v,
        double[] x,
        double[,] y,
        int cidx,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= n - 1; i++)
        {
            y[i, cidx] = v * x[i];
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies vector X[] to row I of A[,]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            A       -   preallocated 2D array large enough to store result
            I       -   destination row index

        OUTPUT PARAMETERS:
            A       -   leading N elements of I-th row are replaced by X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyvr(int n,
            double[] x,
            double[,] a,
            int i,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                a[i,j] = x[j];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies row I of A[,] to vector X[]

        INPUT PARAMETERS:
            N       -   vector length
            A       -   2D array, source
            I       -   source row index
            X       -   preallocated destination

        OUTPUT PARAMETERS:
            X       -   array[N], destination

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyrv(int n,
            double[,] a,
            int i,
            double[] x,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                x[j] = a[i,j];
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Copies row I of A[,] to row K of B[,].

        A[i,...] and B[k,...] may overlap.

        INPUT PARAMETERS:
            N       -   vector length
            A       -   2D array, source
            I       -   source row index
            B       -   preallocated destination
            K       -   destination row index

        OUTPUT PARAMETERS:
            B       -   row K overwritten

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyrr(int n,
            double[,] a,
            int i,
            double[,] b,
            int k,
            xparams _params)
        {
            int j = 0;

            for(j=0; j<=n-1; j++)
            {
                b[k,j] = a[i,j];
            }
        }
#endif


    /*************************************************************************
    Copies vector X[] to column J of A[,]

    INPUT PARAMETERS:
        N       -   vector length
        X       -   array[N], source
        A       -   preallocated 2D array large enough to store result
        J       -   destination col index

    OUTPUT PARAMETERS:
        A       -   leading N elements of J-th column are replaced by X

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopyvc(int n,
        double[] x,
        double[,] a,
        int j,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= n - 1; i++)
        {
            a[i, j] = x[i];
        }
    }


    /*************************************************************************
    Copies column J of A[,] to vector X[]

    INPUT PARAMETERS:
        N       -   vector length
        A       -   source 2D array
        J       -   source col index

    OUTPUT PARAMETERS:
        X       -   preallocated array[N], destination

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rcopycv(int n,
        double[,] a,
        int j,
        double[] x,
        xparams _params)
    {
        int i = 0;

        for (i = 0; i <= n - 1; i++)
        {
            x[i] = a[i, j];
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Matrix-vector product: y := alpha*op(A)*x + beta*y

        NOTE: this  function  expects  Y  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, x, y.

        INPUT PARAMETERS:
            M   -   number of rows of op(A)
            N   -   number of columns of op(A)
            Alpha-  coefficient
            A   -   source matrix
            OpA -   operation type:
                    * OpA=0     =>  op(A) = A
                    * OpA=1     =>  op(A) = A^T
            X   -   input vector, has at least N elements
            Beta-   coefficient
            Y   -   preallocated output array, has at least M elements

        OUTPUT PARAMETERS:
            Y   -   vector which stores result

        HANDLING OF SPECIAL CASES:
            * if M=0, then subroutine does nothing. It does not even touch arrays.
            * if N=0 or Alpha=0.0, then:
              * if Beta=0, then Y is filled by zeros. A and X are  not  referenced
                at all. Initial values of Y are ignored (we do not  multiply  Y by
                zero, we just rewrite it by zeros)
              * if Beta<>0, then Y is replaced by Beta*Y
            * if M>0, N>0, Alpha<>0, but  Beta=0,  then  Y  is  replaced  by  A*x;
               initial state of Y is ignored (rewritten by  A*x,  without  initial
               multiplication by zeros).


          -- ALGLIB routine --

             01.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rgemv(int m,
            int n,
            double alpha,
            double[,] a,
            int opa,
            double[] x,
            double beta,
            double[] y,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;

            
            //
            // Properly premultiply Y by Beta.
            //
            // Quick exit for M=0, N=0 or Alpha=0.
            // After this block we have M>0, N>0, Alpha<>0.
            //
            if( m<=0 )
            {
                return;
            }
            if( (double)(beta)!=(double)(0) )
            {
                rmulv(m, beta, y, _params);
            }
            else
            {
                rsetv(m, 0.0, y, _params);
            }
            if( n<=0 || (double)(alpha)==(double)(0.0) )
            {
                return;
            }
            
            //
            // Generic code
            //
            if( opa==0 )
            {
                
                //
                // y += A*x
                //
                for(i=0; i<=m-1; i++)
                {
                    v = 0;
                    for(j=0; j<=n-1; j++)
                    {
                        v = v+a[i,j]*x[j];
                    }
                    y[i] = alpha*v+y[i];
                }
                return;
            }
            if( opa==1 )
            {
                
                //
                // y += A^T*x
                //
                for(i=0; i<=n-1; i++)
                {
                    v = alpha*x[i];
                    for(j=0; j<=m-1; j++)
                    {
                        y[j] = y[j]+v*a[i,j];
                    }
                }
                return;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Matrix-vector product: y := alpha*op(A)*x + beta*y

        Here x, y, A are subvectors/submatrices of larger vectors/matrices.

        NOTE: this  function  expects  Y  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, x, y.

        INPUT PARAMETERS:
            M   -   number of rows of op(A)
            N   -   number of columns of op(A)
            Alpha-  coefficient
            A   -   source matrix
            IA  -   submatrix offset (row index)
            JA  -   submatrix offset (column index)
            OpA -   operation type:
                    * OpA=0     =>  op(A) = A
                    * OpA=1     =>  op(A) = A^T
            X   -   input vector, has at least N+IX elements
            IX  -   subvector offset
            Beta-   coefficient
            Y   -   preallocated output array, has at least M+IY elements
            IY  -   subvector offset

        OUTPUT PARAMETERS:
            Y   -   vector which stores result

        HANDLING OF SPECIAL CASES:
            * if M=0, then subroutine does nothing. It does not even touch arrays.
            * if N=0 or Alpha=0.0, then:
              * if Beta=0, then Y is filled by zeros. A and X are  not  referenced
                at all. Initial values of Y are ignored (we do not  multiply  Y by
                zero, we just rewrite it by zeros)
              * if Beta<>0, then Y is replaced by Beta*Y
            * if M>0, N>0, Alpha<>0, but  Beta=0,  then  Y  is  replaced  by  A*x;
               initial state of Y is ignored (rewritten by  A*x,  without  initial
               multiplication by zeros).


          -- ALGLIB routine --

             01.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rgemvx(int m,
            int n,
            double alpha,
            double[,] a,
            int ia,
            int ja,
            int opa,
            double[] x,
            int ix,
            double beta,
            double[] y,
            int iy,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;

            
            //
            // Properly premultiply Y by Beta.
            //
            // Quick exit for M=0, N=0 or Alpha=0.
            // After this block we have M>0, N>0, Alpha<>0.
            //
            if( m<=0 )
            {
                return;
            }
            if( (double)(beta)!=(double)(0) )
            {
                rmulvx(m, beta, y, iy, _params);
            }
            else
            {
                rsetvx(m, 0.0, y, iy, _params);
            }
            if( n<=0 || (double)(alpha)==(double)(0.0) )
            {
                return;
            }
            
            //
            // Generic code
            //
            if( opa==0 )
            {
                
                //
                // y += A*x
                //
                for(i=0; i<=m-1; i++)
                {
                    v = 0;
                    for(j=0; j<=n-1; j++)
                    {
                        v = v+a[ia+i,ja+j]*x[ix+j];
                    }
                    y[iy+i] = alpha*v+y[iy+i];
                }
                return;
            }
            if( opa==1 )
            {
                
                //
                // y += A^T*x
                //
                for(i=0; i<=n-1; i++)
                {
                    v = alpha*x[ix+i];
                    for(j=0; j<=m-1; j++)
                    {
                        y[iy+j] = y[iy+j]+v*a[ia+i,ja+j];
                    }
                }
                return;
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Rank-1 correction: A := A + alpha*u*v'

        NOTE: this  function  expects  A  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, u, v.

        INPUT PARAMETERS:
            M   -   number of rows
            N   -   number of columns
            A   -   target MxN matrix
            Alpha-  coefficient
            U   -   vector #1
            V   -   vector #2


          -- ALGLIB routine --
             07.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rger(int m,
            int n,
            double alpha,
            double[] u,
            double[] v,
            double[,] a,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double s = 0;

            if( (m<=0 || n<=0) || (double)(alpha)==(double)(0) )
            {
                return;
            }
            for(i=0; i<=m-1; i++)
            {
                s = alpha*u[i];
                for(j=0; j<=n-1; j++)
                {
                    a[i,j] = a[i,j]+s*v[j];
                }
            }
        }
#endif


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        This subroutine solves linear system op(A)*x=b where:
        * A is NxN upper/lower triangular/unitriangular matrix
        * X and B are Nx1 vectors
        * "op" may be identity transformation or transposition

        Solution replaces X.

        IMPORTANT: * no overflow/underflow/denegeracy tests is performed.
                   * no integrity checks for operand sizes, out-of-bounds accesses
                     and so on is performed

        INPUT PARAMETERS
            N   -   matrix size, N>=0
            A       -   matrix, actial matrix is stored in A[IA:IA+N-1,JA:JA+N-1]
            IA      -   submatrix offset
            JA      -   submatrix offset
            IsUpper -   whether matrix is upper triangular
            IsUnit  -   whether matrix is unitriangular
            OpType  -   transformation type:
                        * 0 - no transformation
                        * 1 - transposition
            X       -   right part, actual vector is stored in X[IX:IX+N-1]
            IX      -   offset
            
        OUTPUT PARAMETERS
            X       -   solution replaces elements X[IX:IX+N-1]

          -- ALGLIB routine --
             (c) 07.09.2021 Bochkanov Sergey
        *************************************************************************/
        public static void rtrsvx(int n,
            double[,] a,
            int ia,
            int ja,
            bool isupper,
            bool isunit,
            int optype,
            double[] x,
            int ix,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;

            if( n<=0 )
            {
                return;
            }
            if( optype==0 && isupper )
            {
                for(i=n-1; i>=0; i--)
                {
                    v = x[ix+i];
                    for(j=i+1; j<=n-1; j++)
                    {
                        v = v-a[ia+i,ja+j]*x[ix+j];
                    }
                    if( !isunit )
                    {
                        v = v/a[ia+i,ja+i];
                    }
                    x[ix+i] = v;
                }
                return;
            }
            if( optype==0 && !isupper )
            {
                for(i=0; i<=n-1; i++)
                {
                    v = x[ix+i];
                    for(j=0; j<=i-1; j++)
                    {
                        v = v-a[ia+i,ja+j]*x[ix+j];
                    }
                    if( !isunit )
                    {
                        v = v/a[ia+i,ja+i];
                    }
                    x[ix+i] = v;
                }
                return;
            }
            if( optype==1 && isupper )
            {
                for(i=0; i<=n-1; i++)
                {
                    v = x[ix+i];
                    if( !isunit )
                    {
                        v = v/a[ia+i,ja+i];
                    }
                    x[ix+i] = v;
                    if( v==0 )
                    {
                        continue;
                    }
                    for(j=i+1; j<=n-1; j++)
                    {
                        x[ix+j] = x[ix+j]-v*a[ia+i,ja+j];
                    }
                }
                return;
            }
            if( optype==1 && !isupper )
            {
                for(i=n-1; i>=0; i--)
                {
                    v = x[ix+i];
                    if( !isunit )
                    {
                        v = v/a[ia+i,ja+i];
                    }
                    x[ix+i] = v;
                    if( v==0 )
                    {
                        continue;
                    }
                    for(j=0; j<=i-1; j++)
                    {
                        x[ix+j] = x[ix+j]-v*a[ia+i,ja+j];
                    }
                }
                return;
            }
            ap.assert(false, "rTRSVX: unexpected operation type");
        }
#endif


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixgerf(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double ralpha,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixrank1f(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        complex[] u,
        int iu,
        complex[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixrank1f(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixrighttrsmf(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixlefttrsmf(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixrighttrsmf(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixlefttrsmf(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixherkf(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixsyrkf(int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        double[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel

      -- ALGLIB routine --
         19.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixgemmf(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    CMatrixGEMM kernel, basecase code for CMatrixGEMM.

    This subroutine calculates C = alpha*op1(A)*op2(B) +beta*C where:
    * C is MxN general matrix
    * op1(A) is MxK matrix
    * op2(B) is KxN matrix
    * "op" may be identity transformation, transposition, conjugate transposition

    Additional info:
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    IMPORTANT:

    This function does NOT preallocate output matrix C, it MUST be preallocated
    by caller prior to calling this function. In case C does not have  enough
    space to store result, exception will be generated.

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        OpTypeA -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        OpTypeB -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixgemmk(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex v = 0;
        complex v00 = 0;
        complex v01 = 0;
        complex v10 = 0;
        complex v11 = 0;
        double v00x = 0;
        double v00y = 0;
        double v01x = 0;
        double v01y = 0;
        double v10x = 0;
        double v10y = 0;
        double v11x = 0;
        double v11y = 0;
        double a0x = 0;
        double a0y = 0;
        double a1x = 0;
        double a1y = 0;
        double b0x = 0;
        double b0y = 0;
        double b1x = 0;
        double b1y = 0;
        int idxa0 = 0;
        int idxa1 = 0;
        int idxb0 = 0;
        int idxb1 = 0;
        int i0 = 0;
        int i1 = 0;
        int ik = 0;
        int j0 = 0;
        int j1 = 0;
        int jk = 0;
        int t = 0;
        int offsa = 0;
        int offsb = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // Try optimized code
        //
        if (cmatrixgemmf(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
        {
            return;
        }

        //
        // if K=0 or Alpha=0, then C=Beta*C
        //
        if (k == 0 || alpha == 0)
        {
            if (beta != 1)
            {
                if (beta != 0)
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        for (j = 0; j <= n - 1; j++)
                        {
                            c[ic + i, jc + j] = beta * c[ic + i, jc + j];
                        }
                    }
                }
                else
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        for (j = 0; j <= n - 1; j++)
                        {
                            c[ic + i, jc + j] = 0;
                        }
                    }
                }
            }
            return;
        }

        //
        // This phase is not really necessary, but compiler complains
        // about "possibly uninitialized variables"
        //
        a0x = 0;
        a0y = 0;
        a1x = 0;
        a1y = 0;
        b0x = 0;
        b0y = 0;
        b1x = 0;
        b1y = 0;

        //
        // General case
        //
        i = 0;
        while (i < m)
        {
            j = 0;
            while (j < n)
            {

                //
                // Choose between specialized 4x4 code and general code
                //
                if (i + 2 <= m && j + 2 <= n)
                {

                    //
                    // Specialized 4x4 code for [I..I+3]x[J..J+3] submatrix of C.
                    //
                    // This submatrix is calculated as sum of K rank-1 products,
                    // with operands cached in local variables in order to speed
                    // up operations with arrays.
                    //
                    v00x = 0.0;
                    v00y = 0.0;
                    v01x = 0.0;
                    v01y = 0.0;
                    v10x = 0.0;
                    v10y = 0.0;
                    v11x = 0.0;
                    v11y = 0.0;
                    if (optypea == 0)
                    {
                        idxa0 = ia + i + 0;
                        idxa1 = ia + i + 1;
                        offsa = ja;
                    }
                    else
                    {
                        idxa0 = ja + i + 0;
                        idxa1 = ja + i + 1;
                        offsa = ia;
                    }
                    if (optypeb == 0)
                    {
                        idxb0 = jb + j + 0;
                        idxb1 = jb + j + 1;
                        offsb = ib;
                    }
                    else
                    {
                        idxb0 = ib + j + 0;
                        idxb1 = ib + j + 1;
                        offsb = jb;
                    }
                    for (t = 0; t <= k - 1; t++)
                    {
                        if (optypea == 0)
                        {
                            a0x = a[idxa0, offsa].x;
                            a0y = a[idxa0, offsa].y;
                            a1x = a[idxa1, offsa].x;
                            a1y = a[idxa1, offsa].y;
                        }
                        if (optypea == 1)
                        {
                            a0x = a[offsa, idxa0].x;
                            a0y = a[offsa, idxa0].y;
                            a1x = a[offsa, idxa1].x;
                            a1y = a[offsa, idxa1].y;
                        }
                        if (optypea == 2)
                        {
                            a0x = a[offsa, idxa0].x;
                            a0y = -a[offsa, idxa0].y;
                            a1x = a[offsa, idxa1].x;
                            a1y = -a[offsa, idxa1].y;
                        }
                        if (optypeb == 0)
                        {
                            b0x = b[offsb, idxb0].x;
                            b0y = b[offsb, idxb0].y;
                            b1x = b[offsb, idxb1].x;
                            b1y = b[offsb, idxb1].y;
                        }
                        if (optypeb == 1)
                        {
                            b0x = b[idxb0, offsb].x;
                            b0y = b[idxb0, offsb].y;
                            b1x = b[idxb1, offsb].x;
                            b1y = b[idxb1, offsb].y;
                        }
                        if (optypeb == 2)
                        {
                            b0x = b[idxb0, offsb].x;
                            b0y = -b[idxb0, offsb].y;
                            b1x = b[idxb1, offsb].x;
                            b1y = -b[idxb1, offsb].y;
                        }
                        v00x = v00x + a0x * b0x - a0y * b0y;
                        v00y = v00y + a0x * b0y + a0y * b0x;
                        v01x = v01x + a0x * b1x - a0y * b1y;
                        v01y = v01y + a0x * b1y + a0y * b1x;
                        v10x = v10x + a1x * b0x - a1y * b0y;
                        v10y = v10y + a1x * b0y + a1y * b0x;
                        v11x = v11x + a1x * b1x - a1y * b1y;
                        v11y = v11y + a1x * b1y + a1y * b1x;
                        offsa = offsa + 1;
                        offsb = offsb + 1;
                    }
                    v00.x = v00x;
                    v00.y = v00y;
                    v10.x = v10x;
                    v10.y = v10y;
                    v01.x = v01x;
                    v01.y = v01y;
                    v11.x = v11x;
                    v11.y = v11y;
                    if (beta == 0)
                    {
                        c[ic + i + 0, jc + j + 0] = alpha * v00;
                        c[ic + i + 0, jc + j + 1] = alpha * v01;
                        c[ic + i + 1, jc + j + 0] = alpha * v10;
                        c[ic + i + 1, jc + j + 1] = alpha * v11;
                    }
                    else
                    {
                        c[ic + i + 0, jc + j + 0] = beta * c[ic + i + 0, jc + j + 0] + alpha * v00;
                        c[ic + i + 0, jc + j + 1] = beta * c[ic + i + 0, jc + j + 1] + alpha * v01;
                        c[ic + i + 1, jc + j + 0] = beta * c[ic + i + 1, jc + j + 0] + alpha * v10;
                        c[ic + i + 1, jc + j + 1] = beta * c[ic + i + 1, jc + j + 1] + alpha * v11;
                    }
                }
                else
                {

                    //
                    // Determine submatrix [I0..I1]x[J0..J1] to process
                    //
                    i0 = i;
                    i1 = Math.Min(i + 1, m - 1);
                    j0 = j;
                    j1 = Math.Min(j + 1, n - 1);

                    //
                    // Process submatrix
                    //
                    for (ik = i0; ik <= i1; ik++)
                    {
                        for (jk = j0; jk <= j1; jk++)
                        {
                            if (k == 0 || alpha == 0)
                            {
                                v = 0;
                            }
                            else
                            {
                                v = 0.0;
                                if (optypea == 0 && optypeb == 0)
                                {
                                    i1_ = (ib) - (ja);
                                    v = 0.0;
                                    for (i_ = ja; i_ <= ja + k - 1; i_++)
                                    {
                                        v += a[ia + ik, i_] * b[i_ + i1_, jb + jk];
                                    }
                                }
                                if (optypea == 0 && optypeb == 1)
                                {
                                    i1_ = (jb) - (ja);
                                    v = 0.0;
                                    for (i_ = ja; i_ <= ja + k - 1; i_++)
                                    {
                                        v += a[ia + ik, i_] * b[ib + jk, i_ + i1_];
                                    }
                                }
                                if (optypea == 0 && optypeb == 2)
                                {
                                    i1_ = (jb) - (ja);
                                    v = 0.0;
                                    for (i_ = ja; i_ <= ja + k - 1; i_++)
                                    {
                                        v += a[ia + ik, i_] * math.conj(b[ib + jk, i_ + i1_]);
                                    }
                                }
                                if (optypea == 1 && optypeb == 0)
                                {
                                    i1_ = (ib) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += a[i_, ja + ik] * b[i_ + i1_, jb + jk];
                                    }
                                }
                                if (optypea == 1 && optypeb == 1)
                                {
                                    i1_ = (jb) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += a[i_, ja + ik] * b[ib + jk, i_ + i1_];
                                    }
                                }
                                if (optypea == 1 && optypeb == 2)
                                {
                                    i1_ = (jb) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += a[i_, ja + ik] * math.conj(b[ib + jk, i_ + i1_]);
                                    }
                                }
                                if (optypea == 2 && optypeb == 0)
                                {
                                    i1_ = (ib) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += math.conj(a[i_, ja + ik]) * b[i_ + i1_, jb + jk];
                                    }
                                }
                                if (optypea == 2 && optypeb == 1)
                                {
                                    i1_ = (jb) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += math.conj(a[i_, ja + ik]) * b[ib + jk, i_ + i1_];
                                    }
                                }
                                if (optypea == 2 && optypeb == 2)
                                {
                                    i1_ = (jb) - (ia);
                                    v = 0.0;
                                    for (i_ = ia; i_ <= ia + k - 1; i_++)
                                    {
                                        v += math.conj(a[i_, ja + ik]) * math.conj(b[ib + jk, i_ + i1_]);
                                    }
                                }
                            }
                            if (beta == 0)
                            {
                                c[ic + ik, jc + jk] = alpha * v;
                            }
                            else
                            {
                                c[ic + ik, jc + jk] = beta * c[ic + ik, jc + jk] + alpha * v;
                            }
                        }
                    }
                }
                j = j + 2;
            }
            i = i + 2;
        }
    }


    /*************************************************************************
    RMatrixGEMM kernel, basecase code for RMatrixGEMM.

    This subroutine calculates C = alpha*op1(A)*op2(B) +beta*C where:
    * C is MxN general matrix
    * op1(A) is MxK matrix
    * op2(B) is KxN matrix
    * "op" may be identity transformation, transposition

    Additional info:
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    IMPORTANT:

    This function does NOT preallocate output matrix C, it MUST be preallocated
    by caller prior to calling this function. In case C does not have  enough
    space to store result, exception will be generated.

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        OpTypeA -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        OpTypeB -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemmk(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;


        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // Try optimized code
        //
        if (rgemm32basecase(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
        {
            return;
        }

        //
        // if K=0 or Alpha=0, then C=Beta*C
        //
        if (k == 0 || (double)(alpha) == (double)(0))
        {
            if ((double)(beta) != (double)(1))
            {
                if ((double)(beta) != (double)(0))
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        for (j = 0; j <= n - 1; j++)
                        {
                            c[ic + i, jc + j] = beta * c[ic + i, jc + j];
                        }
                    }
                }
                else
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        for (j = 0; j <= n - 1; j++)
                        {
                            c[ic + i, jc + j] = 0;
                        }
                    }
                }
            }
            return;
        }

        //
        // Call specialized code.
        //
        // NOTE: specialized code was moved to separate function because of strange
        //       issues with instructions cache on some systems; Having too long
        //       functions significantly slows down internal loop of the algorithm.
        //
        if (optypea == 0 && optypeb == 0)
        {
            rmatrixgemmk44v00(m, n, k, alpha, a, ia, ja, b, ib, jb, beta, c, ic, jc, _params);
        }
        if (optypea == 0 && optypeb != 0)
        {
            rmatrixgemmk44v01(m, n, k, alpha, a, ia, ja, b, ib, jb, beta, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb == 0)
        {
            rmatrixgemmk44v10(m, n, k, alpha, a, ia, ja, b, ib, jb, beta, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb != 0)
        {
            rmatrixgemmk44v11(m, n, k, alpha, a, ia, ja, b, ib, jb, beta, c, ic, jc, _params);
        }
    }


    /*************************************************************************
    RMatrixGEMM kernel, basecase code for RMatrixGEMM, specialized for sitation
    with OpTypeA=0 and OpTypeB=0.

    Additional info:
    * this function requires that Alpha<>0 (assertion is thrown otherwise)

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemmk44v00(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double v00 = 0;
        double v01 = 0;
        double v02 = 0;
        double v03 = 0;
        double v10 = 0;
        double v11 = 0;
        double v12 = 0;
        double v13 = 0;
        double v20 = 0;
        double v21 = 0;
        double v22 = 0;
        double v23 = 0;
        double v30 = 0;
        double v31 = 0;
        double v32 = 0;
        double v33 = 0;
        double a0 = 0;
        double a1 = 0;
        double a2 = 0;
        double a3 = 0;
        double b0 = 0;
        double b1 = 0;
        double b2 = 0;
        double b3 = 0;
        int idxa0 = 0;
        int idxa1 = 0;
        int idxa2 = 0;
        int idxa3 = 0;
        int idxb0 = 0;
        int idxb1 = 0;
        int idxb2 = 0;
        int idxb3 = 0;
        int i0 = 0;
        int i1 = 0;
        int ik = 0;
        int j0 = 0;
        int j1 = 0;
        int jk = 0;
        int t = 0;
        int offsa = 0;
        int offsb = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert((double)(alpha) != (double)(0), "RMatrixGEMMK44V00: internal error (Alpha=0)");

        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // A*B
        //
        i = 0;
        while (i < m)
        {
            j = 0;
            while (j < n)
            {

                //
                // Choose between specialized 4x4 code and general code
                //
                if (i + 4 <= m && j + 4 <= n)
                {

                    //
                    // Specialized 4x4 code for [I..I+3]x[J..J+3] submatrix of C.
                    //
                    // This submatrix is calculated as sum of K rank-1 products,
                    // with operands cached in local variables in order to speed
                    // up operations with arrays.
                    //
                    idxa0 = ia + i + 0;
                    idxa1 = ia + i + 1;
                    idxa2 = ia + i + 2;
                    idxa3 = ia + i + 3;
                    offsa = ja;
                    idxb0 = jb + j + 0;
                    idxb1 = jb + j + 1;
                    idxb2 = jb + j + 2;
                    idxb3 = jb + j + 3;
                    offsb = ib;
                    v00 = 0.0;
                    v01 = 0.0;
                    v02 = 0.0;
                    v03 = 0.0;
                    v10 = 0.0;
                    v11 = 0.0;
                    v12 = 0.0;
                    v13 = 0.0;
                    v20 = 0.0;
                    v21 = 0.0;
                    v22 = 0.0;
                    v23 = 0.0;
                    v30 = 0.0;
                    v31 = 0.0;
                    v32 = 0.0;
                    v33 = 0.0;

                    //
                    // Different variants of internal loop
                    //
                    for (t = 0; t <= k - 1; t++)
                    {
                        a0 = a[idxa0, offsa];
                        a1 = a[idxa1, offsa];
                        b0 = b[offsb, idxb0];
                        b1 = b[offsb, idxb1];
                        v00 = v00 + a0 * b0;
                        v01 = v01 + a0 * b1;
                        v10 = v10 + a1 * b0;
                        v11 = v11 + a1 * b1;
                        a2 = a[idxa2, offsa];
                        a3 = a[idxa3, offsa];
                        v20 = v20 + a2 * b0;
                        v21 = v21 + a2 * b1;
                        v30 = v30 + a3 * b0;
                        v31 = v31 + a3 * b1;
                        b2 = b[offsb, idxb2];
                        b3 = b[offsb, idxb3];
                        v22 = v22 + a2 * b2;
                        v23 = v23 + a2 * b3;
                        v32 = v32 + a3 * b2;
                        v33 = v33 + a3 * b3;
                        v02 = v02 + a0 * b2;
                        v03 = v03 + a0 * b3;
                        v12 = v12 + a1 * b2;
                        v13 = v13 + a1 * b3;
                        offsa = offsa + 1;
                        offsb = offsb + 1;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i + 0, jc + j + 0] = alpha * v00;
                        c[ic + i + 0, jc + j + 1] = alpha * v01;
                        c[ic + i + 0, jc + j + 2] = alpha * v02;
                        c[ic + i + 0, jc + j + 3] = alpha * v03;
                        c[ic + i + 1, jc + j + 0] = alpha * v10;
                        c[ic + i + 1, jc + j + 1] = alpha * v11;
                        c[ic + i + 1, jc + j + 2] = alpha * v12;
                        c[ic + i + 1, jc + j + 3] = alpha * v13;
                        c[ic + i + 2, jc + j + 0] = alpha * v20;
                        c[ic + i + 2, jc + j + 1] = alpha * v21;
                        c[ic + i + 2, jc + j + 2] = alpha * v22;
                        c[ic + i + 2, jc + j + 3] = alpha * v23;
                        c[ic + i + 3, jc + j + 0] = alpha * v30;
                        c[ic + i + 3, jc + j + 1] = alpha * v31;
                        c[ic + i + 3, jc + j + 2] = alpha * v32;
                        c[ic + i + 3, jc + j + 3] = alpha * v33;
                    }
                    else
                    {
                        c[ic + i + 0, jc + j + 0] = beta * c[ic + i + 0, jc + j + 0] + alpha * v00;
                        c[ic + i + 0, jc + j + 1] = beta * c[ic + i + 0, jc + j + 1] + alpha * v01;
                        c[ic + i + 0, jc + j + 2] = beta * c[ic + i + 0, jc + j + 2] + alpha * v02;
                        c[ic + i + 0, jc + j + 3] = beta * c[ic + i + 0, jc + j + 3] + alpha * v03;
                        c[ic + i + 1, jc + j + 0] = beta * c[ic + i + 1, jc + j + 0] + alpha * v10;
                        c[ic + i + 1, jc + j + 1] = beta * c[ic + i + 1, jc + j + 1] + alpha * v11;
                        c[ic + i + 1, jc + j + 2] = beta * c[ic + i + 1, jc + j + 2] + alpha * v12;
                        c[ic + i + 1, jc + j + 3] = beta * c[ic + i + 1, jc + j + 3] + alpha * v13;
                        c[ic + i + 2, jc + j + 0] = beta * c[ic + i + 2, jc + j + 0] + alpha * v20;
                        c[ic + i + 2, jc + j + 1] = beta * c[ic + i + 2, jc + j + 1] + alpha * v21;
                        c[ic + i + 2, jc + j + 2] = beta * c[ic + i + 2, jc + j + 2] + alpha * v22;
                        c[ic + i + 2, jc + j + 3] = beta * c[ic + i + 2, jc + j + 3] + alpha * v23;
                        c[ic + i + 3, jc + j + 0] = beta * c[ic + i + 3, jc + j + 0] + alpha * v30;
                        c[ic + i + 3, jc + j + 1] = beta * c[ic + i + 3, jc + j + 1] + alpha * v31;
                        c[ic + i + 3, jc + j + 2] = beta * c[ic + i + 3, jc + j + 2] + alpha * v32;
                        c[ic + i + 3, jc + j + 3] = beta * c[ic + i + 3, jc + j + 3] + alpha * v33;
                    }
                }
                else
                {

                    //
                    // Determine submatrix [I0..I1]x[J0..J1] to process
                    //
                    i0 = i;
                    i1 = Math.Min(i + 3, m - 1);
                    j0 = j;
                    j1 = Math.Min(j + 3, n - 1);

                    //
                    // Process submatrix
                    //
                    for (ik = i0; ik <= i1; ik++)
                    {
                        for (jk = j0; jk <= j1; jk++)
                        {
                            if (k == 0 || (double)(alpha) == (double)(0))
                            {
                                v = 0;
                            }
                            else
                            {
                                i1_ = (ib) - (ja);
                                v = 0.0;
                                for (i_ = ja; i_ <= ja + k - 1; i_++)
                                {
                                    v += a[ia + ik, i_] * b[i_ + i1_, jb + jk];
                                }
                            }
                            if ((double)(beta) == (double)(0))
                            {
                                c[ic + ik, jc + jk] = alpha * v;
                            }
                            else
                            {
                                c[ic + ik, jc + jk] = beta * c[ic + ik, jc + jk] + alpha * v;
                            }
                        }
                    }
                }
                j = j + 4;
            }
            i = i + 4;
        }
    }


    /*************************************************************************
    RMatrixGEMM kernel, basecase code for RMatrixGEMM, specialized for sitation
    with OpTypeA=0 and OpTypeB=1.

    Additional info:
    * this function requires that Alpha<>0 (assertion is thrown otherwise)

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemmk44v01(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double v00 = 0;
        double v01 = 0;
        double v02 = 0;
        double v03 = 0;
        double v10 = 0;
        double v11 = 0;
        double v12 = 0;
        double v13 = 0;
        double v20 = 0;
        double v21 = 0;
        double v22 = 0;
        double v23 = 0;
        double v30 = 0;
        double v31 = 0;
        double v32 = 0;
        double v33 = 0;
        double a0 = 0;
        double a1 = 0;
        double a2 = 0;
        double a3 = 0;
        double b0 = 0;
        double b1 = 0;
        double b2 = 0;
        double b3 = 0;
        int idxa0 = 0;
        int idxa1 = 0;
        int idxa2 = 0;
        int idxa3 = 0;
        int idxb0 = 0;
        int idxb1 = 0;
        int idxb2 = 0;
        int idxb3 = 0;
        int i0 = 0;
        int i1 = 0;
        int ik = 0;
        int j0 = 0;
        int j1 = 0;
        int jk = 0;
        int t = 0;
        int offsa = 0;
        int offsb = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert((double)(alpha) != (double)(0), "RMatrixGEMMK44V00: internal error (Alpha=0)");

        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // A*B'
        //
        i = 0;
        while (i < m)
        {
            j = 0;
            while (j < n)
            {

                //
                // Choose between specialized 4x4 code and general code
                //
                if (i + 4 <= m && j + 4 <= n)
                {

                    //
                    // Specialized 4x4 code for [I..I+3]x[J..J+3] submatrix of C.
                    //
                    // This submatrix is calculated as sum of K rank-1 products,
                    // with operands cached in local variables in order to speed
                    // up operations with arrays.
                    //
                    idxa0 = ia + i + 0;
                    idxa1 = ia + i + 1;
                    idxa2 = ia + i + 2;
                    idxa3 = ia + i + 3;
                    offsa = ja;
                    idxb0 = ib + j + 0;
                    idxb1 = ib + j + 1;
                    idxb2 = ib + j + 2;
                    idxb3 = ib + j + 3;
                    offsb = jb;
                    v00 = 0.0;
                    v01 = 0.0;
                    v02 = 0.0;
                    v03 = 0.0;
                    v10 = 0.0;
                    v11 = 0.0;
                    v12 = 0.0;
                    v13 = 0.0;
                    v20 = 0.0;
                    v21 = 0.0;
                    v22 = 0.0;
                    v23 = 0.0;
                    v30 = 0.0;
                    v31 = 0.0;
                    v32 = 0.0;
                    v33 = 0.0;
                    for (t = 0; t <= k - 1; t++)
                    {
                        a0 = a[idxa0, offsa];
                        a1 = a[idxa1, offsa];
                        b0 = b[idxb0, offsb];
                        b1 = b[idxb1, offsb];
                        v00 = v00 + a0 * b0;
                        v01 = v01 + a0 * b1;
                        v10 = v10 + a1 * b0;
                        v11 = v11 + a1 * b1;
                        a2 = a[idxa2, offsa];
                        a3 = a[idxa3, offsa];
                        v20 = v20 + a2 * b0;
                        v21 = v21 + a2 * b1;
                        v30 = v30 + a3 * b0;
                        v31 = v31 + a3 * b1;
                        b2 = b[idxb2, offsb];
                        b3 = b[idxb3, offsb];
                        v22 = v22 + a2 * b2;
                        v23 = v23 + a2 * b3;
                        v32 = v32 + a3 * b2;
                        v33 = v33 + a3 * b3;
                        v02 = v02 + a0 * b2;
                        v03 = v03 + a0 * b3;
                        v12 = v12 + a1 * b2;
                        v13 = v13 + a1 * b3;
                        offsa = offsa + 1;
                        offsb = offsb + 1;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i + 0, jc + j + 0] = alpha * v00;
                        c[ic + i + 0, jc + j + 1] = alpha * v01;
                        c[ic + i + 0, jc + j + 2] = alpha * v02;
                        c[ic + i + 0, jc + j + 3] = alpha * v03;
                        c[ic + i + 1, jc + j + 0] = alpha * v10;
                        c[ic + i + 1, jc + j + 1] = alpha * v11;
                        c[ic + i + 1, jc + j + 2] = alpha * v12;
                        c[ic + i + 1, jc + j + 3] = alpha * v13;
                        c[ic + i + 2, jc + j + 0] = alpha * v20;
                        c[ic + i + 2, jc + j + 1] = alpha * v21;
                        c[ic + i + 2, jc + j + 2] = alpha * v22;
                        c[ic + i + 2, jc + j + 3] = alpha * v23;
                        c[ic + i + 3, jc + j + 0] = alpha * v30;
                        c[ic + i + 3, jc + j + 1] = alpha * v31;
                        c[ic + i + 3, jc + j + 2] = alpha * v32;
                        c[ic + i + 3, jc + j + 3] = alpha * v33;
                    }
                    else
                    {
                        c[ic + i + 0, jc + j + 0] = beta * c[ic + i + 0, jc + j + 0] + alpha * v00;
                        c[ic + i + 0, jc + j + 1] = beta * c[ic + i + 0, jc + j + 1] + alpha * v01;
                        c[ic + i + 0, jc + j + 2] = beta * c[ic + i + 0, jc + j + 2] + alpha * v02;
                        c[ic + i + 0, jc + j + 3] = beta * c[ic + i + 0, jc + j + 3] + alpha * v03;
                        c[ic + i + 1, jc + j + 0] = beta * c[ic + i + 1, jc + j + 0] + alpha * v10;
                        c[ic + i + 1, jc + j + 1] = beta * c[ic + i + 1, jc + j + 1] + alpha * v11;
                        c[ic + i + 1, jc + j + 2] = beta * c[ic + i + 1, jc + j + 2] + alpha * v12;
                        c[ic + i + 1, jc + j + 3] = beta * c[ic + i + 1, jc + j + 3] + alpha * v13;
                        c[ic + i + 2, jc + j + 0] = beta * c[ic + i + 2, jc + j + 0] + alpha * v20;
                        c[ic + i + 2, jc + j + 1] = beta * c[ic + i + 2, jc + j + 1] + alpha * v21;
                        c[ic + i + 2, jc + j + 2] = beta * c[ic + i + 2, jc + j + 2] + alpha * v22;
                        c[ic + i + 2, jc + j + 3] = beta * c[ic + i + 2, jc + j + 3] + alpha * v23;
                        c[ic + i + 3, jc + j + 0] = beta * c[ic + i + 3, jc + j + 0] + alpha * v30;
                        c[ic + i + 3, jc + j + 1] = beta * c[ic + i + 3, jc + j + 1] + alpha * v31;
                        c[ic + i + 3, jc + j + 2] = beta * c[ic + i + 3, jc + j + 2] + alpha * v32;
                        c[ic + i + 3, jc + j + 3] = beta * c[ic + i + 3, jc + j + 3] + alpha * v33;
                    }
                }
                else
                {

                    //
                    // Determine submatrix [I0..I1]x[J0..J1] to process
                    //
                    i0 = i;
                    i1 = Math.Min(i + 3, m - 1);
                    j0 = j;
                    j1 = Math.Min(j + 3, n - 1);

                    //
                    // Process submatrix
                    //
                    for (ik = i0; ik <= i1; ik++)
                    {
                        for (jk = j0; jk <= j1; jk++)
                        {
                            if (k == 0 || (double)(alpha) == (double)(0))
                            {
                                v = 0;
                            }
                            else
                            {
                                i1_ = (jb) - (ja);
                                v = 0.0;
                                for (i_ = ja; i_ <= ja + k - 1; i_++)
                                {
                                    v += a[ia + ik, i_] * b[ib + jk, i_ + i1_];
                                }
                            }
                            if ((double)(beta) == (double)(0))
                            {
                                c[ic + ik, jc + jk] = alpha * v;
                            }
                            else
                            {
                                c[ic + ik, jc + jk] = beta * c[ic + ik, jc + jk] + alpha * v;
                            }
                        }
                    }
                }
                j = j + 4;
            }
            i = i + 4;
        }
    }


    /*************************************************************************
    RMatrixGEMM kernel, basecase code for RMatrixGEMM, specialized for sitation
    with OpTypeA=1 and OpTypeB=0.

    Additional info:
    * this function requires that Alpha<>0 (assertion is thrown otherwise)

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemmk44v10(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double v00 = 0;
        double v01 = 0;
        double v02 = 0;
        double v03 = 0;
        double v10 = 0;
        double v11 = 0;
        double v12 = 0;
        double v13 = 0;
        double v20 = 0;
        double v21 = 0;
        double v22 = 0;
        double v23 = 0;
        double v30 = 0;
        double v31 = 0;
        double v32 = 0;
        double v33 = 0;
        double a0 = 0;
        double a1 = 0;
        double a2 = 0;
        double a3 = 0;
        double b0 = 0;
        double b1 = 0;
        double b2 = 0;
        double b3 = 0;
        int idxa0 = 0;
        int idxa1 = 0;
        int idxa2 = 0;
        int idxa3 = 0;
        int idxb0 = 0;
        int idxb1 = 0;
        int idxb2 = 0;
        int idxb3 = 0;
        int i0 = 0;
        int i1 = 0;
        int ik = 0;
        int j0 = 0;
        int j1 = 0;
        int jk = 0;
        int t = 0;
        int offsa = 0;
        int offsb = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert((double)(alpha) != (double)(0), "RMatrixGEMMK44V00: internal error (Alpha=0)");

        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // A'*B
        //
        i = 0;
        while (i < m)
        {
            j = 0;
            while (j < n)
            {

                //
                // Choose between specialized 4x4 code and general code
                //
                if (i + 4 <= m && j + 4 <= n)
                {

                    //
                    // Specialized 4x4 code for [I..I+3]x[J..J+3] submatrix of C.
                    //
                    // This submatrix is calculated as sum of K rank-1 products,
                    // with operands cached in local variables in order to speed
                    // up operations with arrays.
                    //
                    idxa0 = ja + i + 0;
                    idxa1 = ja + i + 1;
                    idxa2 = ja + i + 2;
                    idxa3 = ja + i + 3;
                    offsa = ia;
                    idxb0 = jb + j + 0;
                    idxb1 = jb + j + 1;
                    idxb2 = jb + j + 2;
                    idxb3 = jb + j + 3;
                    offsb = ib;
                    v00 = 0.0;
                    v01 = 0.0;
                    v02 = 0.0;
                    v03 = 0.0;
                    v10 = 0.0;
                    v11 = 0.0;
                    v12 = 0.0;
                    v13 = 0.0;
                    v20 = 0.0;
                    v21 = 0.0;
                    v22 = 0.0;
                    v23 = 0.0;
                    v30 = 0.0;
                    v31 = 0.0;
                    v32 = 0.0;
                    v33 = 0.0;
                    for (t = 0; t <= k - 1; t++)
                    {
                        a0 = a[offsa, idxa0];
                        a1 = a[offsa, idxa1];
                        b0 = b[offsb, idxb0];
                        b1 = b[offsb, idxb1];
                        v00 = v00 + a0 * b0;
                        v01 = v01 + a0 * b1;
                        v10 = v10 + a1 * b0;
                        v11 = v11 + a1 * b1;
                        a2 = a[offsa, idxa2];
                        a3 = a[offsa, idxa3];
                        v20 = v20 + a2 * b0;
                        v21 = v21 + a2 * b1;
                        v30 = v30 + a3 * b0;
                        v31 = v31 + a3 * b1;
                        b2 = b[offsb, idxb2];
                        b3 = b[offsb, idxb3];
                        v22 = v22 + a2 * b2;
                        v23 = v23 + a2 * b3;
                        v32 = v32 + a3 * b2;
                        v33 = v33 + a3 * b3;
                        v02 = v02 + a0 * b2;
                        v03 = v03 + a0 * b3;
                        v12 = v12 + a1 * b2;
                        v13 = v13 + a1 * b3;
                        offsa = offsa + 1;
                        offsb = offsb + 1;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i + 0, jc + j + 0] = alpha * v00;
                        c[ic + i + 0, jc + j + 1] = alpha * v01;
                        c[ic + i + 0, jc + j + 2] = alpha * v02;
                        c[ic + i + 0, jc + j + 3] = alpha * v03;
                        c[ic + i + 1, jc + j + 0] = alpha * v10;
                        c[ic + i + 1, jc + j + 1] = alpha * v11;
                        c[ic + i + 1, jc + j + 2] = alpha * v12;
                        c[ic + i + 1, jc + j + 3] = alpha * v13;
                        c[ic + i + 2, jc + j + 0] = alpha * v20;
                        c[ic + i + 2, jc + j + 1] = alpha * v21;
                        c[ic + i + 2, jc + j + 2] = alpha * v22;
                        c[ic + i + 2, jc + j + 3] = alpha * v23;
                        c[ic + i + 3, jc + j + 0] = alpha * v30;
                        c[ic + i + 3, jc + j + 1] = alpha * v31;
                        c[ic + i + 3, jc + j + 2] = alpha * v32;
                        c[ic + i + 3, jc + j + 3] = alpha * v33;
                    }
                    else
                    {
                        c[ic + i + 0, jc + j + 0] = beta * c[ic + i + 0, jc + j + 0] + alpha * v00;
                        c[ic + i + 0, jc + j + 1] = beta * c[ic + i + 0, jc + j + 1] + alpha * v01;
                        c[ic + i + 0, jc + j + 2] = beta * c[ic + i + 0, jc + j + 2] + alpha * v02;
                        c[ic + i + 0, jc + j + 3] = beta * c[ic + i + 0, jc + j + 3] + alpha * v03;
                        c[ic + i + 1, jc + j + 0] = beta * c[ic + i + 1, jc + j + 0] + alpha * v10;
                        c[ic + i + 1, jc + j + 1] = beta * c[ic + i + 1, jc + j + 1] + alpha * v11;
                        c[ic + i + 1, jc + j + 2] = beta * c[ic + i + 1, jc + j + 2] + alpha * v12;
                        c[ic + i + 1, jc + j + 3] = beta * c[ic + i + 1, jc + j + 3] + alpha * v13;
                        c[ic + i + 2, jc + j + 0] = beta * c[ic + i + 2, jc + j + 0] + alpha * v20;
                        c[ic + i + 2, jc + j + 1] = beta * c[ic + i + 2, jc + j + 1] + alpha * v21;
                        c[ic + i + 2, jc + j + 2] = beta * c[ic + i + 2, jc + j + 2] + alpha * v22;
                        c[ic + i + 2, jc + j + 3] = beta * c[ic + i + 2, jc + j + 3] + alpha * v23;
                        c[ic + i + 3, jc + j + 0] = beta * c[ic + i + 3, jc + j + 0] + alpha * v30;
                        c[ic + i + 3, jc + j + 1] = beta * c[ic + i + 3, jc + j + 1] + alpha * v31;
                        c[ic + i + 3, jc + j + 2] = beta * c[ic + i + 3, jc + j + 2] + alpha * v32;
                        c[ic + i + 3, jc + j + 3] = beta * c[ic + i + 3, jc + j + 3] + alpha * v33;
                    }
                }
                else
                {

                    //
                    // Determine submatrix [I0..I1]x[J0..J1] to process
                    //
                    i0 = i;
                    i1 = Math.Min(i + 3, m - 1);
                    j0 = j;
                    j1 = Math.Min(j + 3, n - 1);

                    //
                    // Process submatrix
                    //
                    for (ik = i0; ik <= i1; ik++)
                    {
                        for (jk = j0; jk <= j1; jk++)
                        {
                            if (k == 0 || (double)(alpha) == (double)(0))
                            {
                                v = 0;
                            }
                            else
                            {
                                v = 0.0;
                                i1_ = (ib) - (ia);
                                v = 0.0;
                                for (i_ = ia; i_ <= ia + k - 1; i_++)
                                {
                                    v += a[i_, ja + ik] * b[i_ + i1_, jb + jk];
                                }
                            }
                            if ((double)(beta) == (double)(0))
                            {
                                c[ic + ik, jc + jk] = alpha * v;
                            }
                            else
                            {
                                c[ic + ik, jc + jk] = beta * c[ic + ik, jc + jk] + alpha * v;
                            }
                        }
                    }
                }
                j = j + 4;
            }
            i = i + 4;
        }
    }


    /*************************************************************************
    RMatrixGEMM kernel, basecase code for RMatrixGEMM, specialized for sitation
    with OpTypeA=1 and OpTypeB=1.

    Additional info:
    * this function requires that Alpha<>0 (assertion is thrown otherwise)

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix
        IC      -   submatrix offset
        JC      -   submatrix offset

      -- ALGLIB routine --
         27.03.2013
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemmk44v11(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double v00 = 0;
        double v01 = 0;
        double v02 = 0;
        double v03 = 0;
        double v10 = 0;
        double v11 = 0;
        double v12 = 0;
        double v13 = 0;
        double v20 = 0;
        double v21 = 0;
        double v22 = 0;
        double v23 = 0;
        double v30 = 0;
        double v31 = 0;
        double v32 = 0;
        double v33 = 0;
        double a0 = 0;
        double a1 = 0;
        double a2 = 0;
        double a3 = 0;
        double b0 = 0;
        double b1 = 0;
        double b2 = 0;
        double b3 = 0;
        int idxa0 = 0;
        int idxa1 = 0;
        int idxa2 = 0;
        int idxa3 = 0;
        int idxb0 = 0;
        int idxb1 = 0;
        int idxb2 = 0;
        int idxb3 = 0;
        int i0 = 0;
        int i1 = 0;
        int ik = 0;
        int j0 = 0;
        int j1 = 0;
        int jk = 0;
        int t = 0;
        int offsa = 0;
        int offsb = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert((double)(alpha) != (double)(0), "RMatrixGEMMK44V00: internal error (Alpha=0)");

        //
        // if matrix size is zero
        //
        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // A'*B'
        //
        i = 0;
        while (i < m)
        {
            j = 0;
            while (j < n)
            {

                //
                // Choose between specialized 4x4 code and general code
                //
                if (i + 4 <= m && j + 4 <= n)
                {

                    //
                    // Specialized 4x4 code for [I..I+3]x[J..J+3] submatrix of C.
                    //
                    // This submatrix is calculated as sum of K rank-1 products,
                    // with operands cached in local variables in order to speed
                    // up operations with arrays.
                    //
                    idxa0 = ja + i + 0;
                    idxa1 = ja + i + 1;
                    idxa2 = ja + i + 2;
                    idxa3 = ja + i + 3;
                    offsa = ia;
                    idxb0 = ib + j + 0;
                    idxb1 = ib + j + 1;
                    idxb2 = ib + j + 2;
                    idxb3 = ib + j + 3;
                    offsb = jb;
                    v00 = 0.0;
                    v01 = 0.0;
                    v02 = 0.0;
                    v03 = 0.0;
                    v10 = 0.0;
                    v11 = 0.0;
                    v12 = 0.0;
                    v13 = 0.0;
                    v20 = 0.0;
                    v21 = 0.0;
                    v22 = 0.0;
                    v23 = 0.0;
                    v30 = 0.0;
                    v31 = 0.0;
                    v32 = 0.0;
                    v33 = 0.0;
                    for (t = 0; t <= k - 1; t++)
                    {
                        a0 = a[offsa, idxa0];
                        a1 = a[offsa, idxa1];
                        b0 = b[idxb0, offsb];
                        b1 = b[idxb1, offsb];
                        v00 = v00 + a0 * b0;
                        v01 = v01 + a0 * b1;
                        v10 = v10 + a1 * b0;
                        v11 = v11 + a1 * b1;
                        a2 = a[offsa, idxa2];
                        a3 = a[offsa, idxa3];
                        v20 = v20 + a2 * b0;
                        v21 = v21 + a2 * b1;
                        v30 = v30 + a3 * b0;
                        v31 = v31 + a3 * b1;
                        b2 = b[idxb2, offsb];
                        b3 = b[idxb3, offsb];
                        v22 = v22 + a2 * b2;
                        v23 = v23 + a2 * b3;
                        v32 = v32 + a3 * b2;
                        v33 = v33 + a3 * b3;
                        v02 = v02 + a0 * b2;
                        v03 = v03 + a0 * b3;
                        v12 = v12 + a1 * b2;
                        v13 = v13 + a1 * b3;
                        offsa = offsa + 1;
                        offsb = offsb + 1;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i + 0, jc + j + 0] = alpha * v00;
                        c[ic + i + 0, jc + j + 1] = alpha * v01;
                        c[ic + i + 0, jc + j + 2] = alpha * v02;
                        c[ic + i + 0, jc + j + 3] = alpha * v03;
                        c[ic + i + 1, jc + j + 0] = alpha * v10;
                        c[ic + i + 1, jc + j + 1] = alpha * v11;
                        c[ic + i + 1, jc + j + 2] = alpha * v12;
                        c[ic + i + 1, jc + j + 3] = alpha * v13;
                        c[ic + i + 2, jc + j + 0] = alpha * v20;
                        c[ic + i + 2, jc + j + 1] = alpha * v21;
                        c[ic + i + 2, jc + j + 2] = alpha * v22;
                        c[ic + i + 2, jc + j + 3] = alpha * v23;
                        c[ic + i + 3, jc + j + 0] = alpha * v30;
                        c[ic + i + 3, jc + j + 1] = alpha * v31;
                        c[ic + i + 3, jc + j + 2] = alpha * v32;
                        c[ic + i + 3, jc + j + 3] = alpha * v33;
                    }
                    else
                    {
                        c[ic + i + 0, jc + j + 0] = beta * c[ic + i + 0, jc + j + 0] + alpha * v00;
                        c[ic + i + 0, jc + j + 1] = beta * c[ic + i + 0, jc + j + 1] + alpha * v01;
                        c[ic + i + 0, jc + j + 2] = beta * c[ic + i + 0, jc + j + 2] + alpha * v02;
                        c[ic + i + 0, jc + j + 3] = beta * c[ic + i + 0, jc + j + 3] + alpha * v03;
                        c[ic + i + 1, jc + j + 0] = beta * c[ic + i + 1, jc + j + 0] + alpha * v10;
                        c[ic + i + 1, jc + j + 1] = beta * c[ic + i + 1, jc + j + 1] + alpha * v11;
                        c[ic + i + 1, jc + j + 2] = beta * c[ic + i + 1, jc + j + 2] + alpha * v12;
                        c[ic + i + 1, jc + j + 3] = beta * c[ic + i + 1, jc + j + 3] + alpha * v13;
                        c[ic + i + 2, jc + j + 0] = beta * c[ic + i + 2, jc + j + 0] + alpha * v20;
                        c[ic + i + 2, jc + j + 1] = beta * c[ic + i + 2, jc + j + 1] + alpha * v21;
                        c[ic + i + 2, jc + j + 2] = beta * c[ic + i + 2, jc + j + 2] + alpha * v22;
                        c[ic + i + 2, jc + j + 3] = beta * c[ic + i + 2, jc + j + 3] + alpha * v23;
                        c[ic + i + 3, jc + j + 0] = beta * c[ic + i + 3, jc + j + 0] + alpha * v30;
                        c[ic + i + 3, jc + j + 1] = beta * c[ic + i + 3, jc + j + 1] + alpha * v31;
                        c[ic + i + 3, jc + j + 2] = beta * c[ic + i + 3, jc + j + 2] + alpha * v32;
                        c[ic + i + 3, jc + j + 3] = beta * c[ic + i + 3, jc + j + 3] + alpha * v33;
                    }
                }
                else
                {

                    //
                    // Determine submatrix [I0..I1]x[J0..J1] to process
                    //
                    i0 = i;
                    i1 = Math.Min(i + 3, m - 1);
                    j0 = j;
                    j1 = Math.Min(j + 3, n - 1);

                    //
                    // Process submatrix
                    //
                    for (ik = i0; ik <= i1; ik++)
                    {
                        for (jk = j0; jk <= j1; jk++)
                        {
                            if (k == 0 || (double)(alpha) == (double)(0))
                            {
                                v = 0;
                            }
                            else
                            {
                                v = 0.0;
                                i1_ = (jb) - (ia);
                                v = 0.0;
                                for (i_ = ia; i_ <= ia + k - 1; i_++)
                                {
                                    v += a[i_, ja + ik] * b[ib + jk, i_ + i1_];
                                }
                            }
                            if ((double)(beta) == (double)(0))
                            {
                                c[ic + ik, jc + jk] = alpha * v;
                            }
                            else
                            {
                                c[ic + ik, jc + jk] = beta * c[ic + ik, jc + jk] + alpha * v;
                            }
                        }
                    }
                }
                j = j + 4;
            }
            i = i + 4;
        }
    }


#if ALGLIB_NO_FAST_KERNELS
        /*************************************************************************
        Fast kernel (new version with AVX2/SSE2)

          -- ALGLIB routine --
             19.01.2010
             Bochkanov Sergey
        *************************************************************************/
        private static bool rgemm32basecase(int m,
            int n,
            int k,
            double alpha,
            double[,] a,
            int ia,
            int ja,
            int optypea,
            double[,] b,
            int ib,
            int jb,
            int optypeb,
            double beta,
            double[,] c,
            int ic,
            int jc,
            xparams _params)
        {
            bool result = new bool();

            result = false;
            return result;
        }
#endif



    /*************************************************************************
    ABLASF kernels
    *************************************************************************/
#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rdot() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rdot(
        int n,
        double *A,
        double *B,
        out double R)
    {
        R = 0;
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_dot = Intrinsics.Vector256<double>.Zero;
            for(i=0; i<head; i+=4)
            {
                avx_dot = Fma.MultiplyAdd(
                            Avx2.LoadVector256(A+i),
                            Avx2.LoadVector256(B+i),
                            avx_dot
                            );
            }
            double *vdot = stackalloc double[4];
            Avx2.Store(vdot, avx_dot);
            for(i=head; i<n; i++)
                vdot[0] += A[i]*B[i];
            R = vdot[0]+vdot[1]+vdot[2]+vdot[3];
            return true;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_dot = Intrinsics.Vector256<double>.Zero;
            for(i=0; i<head; i+=4)
            {
                avx_dot = Avx2.Add(
                            Avx2.Multiply(
                                Avx2.LoadVector256(A+i),
                                Avx2.LoadVector256(B+i)
                                ),
                            avx_dot
                            );
            }
            double *vdot = stackalloc double[4];
            Avx2.Store(vdot, avx_dot);
            for(i=head; i<n; i++)
                vdot[0] += A[i]*B[i];
            R = vdot[0]+vdot[1]+vdot[2]+vdot[3];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif
#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rdotv2()

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rdotv2(
        int n,
        double *A,
        out double R)
    {
        R = 0;
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_dot = Intrinsics.Vector256<double>.Zero;
            for(i=0; i<head; i+=4)
            {
                Intrinsics.Vector256<double> Ai = Avx2.LoadVector256(A+i);
                avx_dot = Fma.MultiplyAdd(Ai, Ai, avx_dot);
            }
            double *vdot = stackalloc double[4];
            Avx2.Store(vdot, avx_dot);
            for(i=head; i<n; i++)
                vdot[0] += A[i]*A[i];
            R = vdot[0]+vdot[1]+vdot[2]+vdot[3];
            return true;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_dot = Intrinsics.Vector256<double>.Zero;
            for(i=0; i<head; i+=4)
            {
                Intrinsics.Vector256<double> Ai = Avx2.LoadVector256(A+i);
                avx_dot = Avx2.Add(Avx2.Multiply(Ai, Ai), avx_dot);
            }
            double *vdot = stackalloc double[4];
            Avx2.Store(vdot, avx_dot);
            for(i=head; i<n; i++)
                vdot[0] += A[i]*A[i];
            R = vdot[0]+vdot[1]+vdot[2]+vdot[3];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

        /*************************************************************************
        Computes dot product (X,Y) for elements [0,N) of X[] and Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            Y       -   array[N], vector to process

        RESULT:
            (X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotv(int n,
            double[] x,
            double[] y,
            xparams _params)
        {
            double result = 0;
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    double r;
                    if( try_rdot(n, py, px, out r) )
                        return r;
                }
            }
#endif

            result = 0;
            for (i = 0; i <= n - 1; i++)
            {
                result = result + x[i] * y[i];
            }
            return result;
        }

        /*************************************************************************
        Computes dot product (X,A[i]) for elements [0,N) of vector X[] and row A[i,*]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            A       -   array[?,N], matrix to process
            I       -   row index

        RESULT:
            (X,Ai)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotvr(int n,
            double[] x,
            double[,] a,
            int i,
            xparams _params)
        {
            double result = 0;
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, pa=a)
                {
                    double r;
                    if( try_rdot(n, px, pa+i*a.GetLength(1), out r) )
                        return r;
                }
            }
#endif

            result = 0;
            for (j = 0; j <= n - 1; j++)
            {
                result = result + x[j] * a[i, j];
            }
            return result;
        }

        /*************************************************************************
        Computes dot product (X,A[i]) for rows A[ia,*] and B[ib,*]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            A       -   array[?,N], matrix to process
            I       -   row index

        RESULT:
            (X,Ai)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotrr(int n,
            double[,] a,
            int ia,
            double[,] b,
            int ib,
            xparams _params)
        {
            double result = 0;
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* pa=a, pb=b)
                {
                    double r;
                    if( try_rdot(n, pa+ia*a.GetLength(1), pb+ib*b.GetLength(1), out r) )
                        return r;
                }
            }
#endif

            result = 0;
            for (j = 0; j <= n - 1; j++)
            {
                result = result + a[ia, j] * b[ib, j];
            }
            return result;
        }

        /*************************************************************************
        Computes dot product (X,X) for elements [0,N) of X[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        RESULT:
            (X,X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rdotv2(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    double r;
                    if( try_rdotv2(n, px, out r) )
                        return r;
                }
            }
#endif

            result = 0;
            for (i = 0; i <= n - 1; i++)
            {
                v = x[i];
                result = result + v * v;
            }
            return result;
        }

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for raddv() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_raddv(
        int n,
        double vSrc,
        double *Src,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_vsrc = Avx2.BroadcastScalarToVector256(&vSrc);
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Fma.MultiplyAdd(
                        Avx2.LoadVector256(Src+i),
                        avx_vsrc,
                        Avx2.LoadVector256(Dst+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] += vSrc*Src[i];
            return true;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_vsrc = Avx2.BroadcastScalarToVector256(&vSrc);
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Avx2.Add(
                        Avx2.Multiply(
                            Avx2.LoadVector256(Src+i),
                            avx_vsrc
                            ),
                        Avx2.LoadVector256(Dst+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] += vSrc*Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rmuladdv() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmuladdv(
        int n,
        double *Src0,
        double *Src1,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Fma.MultiplyAdd(
                        Avx2.LoadVector256(Src0+i),
                        Avx2.LoadVector256(Src1+i),
                        Avx2.LoadVector256(Dst+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] += Src0[i]*Src1[i];
            return true;
        }
#endif // no-fma
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rmuladdv() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rnegmuladdv(
        int n,
        double *Src0,
        double *Src1,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Fma.MultiplyAddNegated(
                        Avx2.LoadVector256(Src0+i),
                        Avx2.LoadVector256(Src1+i),
                        Avx2.LoadVector256(Dst+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] -= Src0[i]*Src1[i];
            return true;
        }
#endif // no-fma
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rcopymuladdv() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rcopymuladdv(
        int n,
        double *Src0,
        double *Src1,
        double *Src2,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Fma.MultiplyAdd(
                        Avx2.LoadVector256(Src0+i),
                        Avx2.LoadVector256(Src1+i),
                        Avx2.LoadVector256(Src2+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] = Src2[i]+Src0[i]*Src1[i];
            return true;
        }
#endif // no-fma
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rcopymuladdv() and similar funcs

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rcopynegmuladdv(
        int n,
        double *Src0,
        double *Src1,
        double *Src2,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Fma.MultiplyAddNegated(
                        Avx2.LoadVector256(Src0+i),
                        Avx2.LoadVector256(Src1+i),
                        Avx2.LoadVector256(Src2+i)
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] = Src2[i]-Src0[i]*Src1[i];
            return true;
        }
#endif // no-fma
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rmul()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmulv(
        int n,
        double vDst,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_vdst = Avx2.BroadcastScalarToVector256(&vDst);
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Avx2.Multiply(
                        Avx2.LoadVector256(Dst+i),
                        avx_vdst
                        )
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] *= vDst;
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rsqrt()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rsqrtv(
        int n,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
                Avx2.Store(Dst+i, Avx2.Sqrt(Avx2.LoadVector256(Dst+i)));
            for(i=head; i<n; i++)
                Dst[i] = System.Math.Sqrt(Dst[i]);
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rcopy()

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rcopy(
        int n,
        double *Src,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Avx2.LoadVector256(Src+i)
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] = Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for icopy()

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_icopy(
        int n,
        int *Src,
        int *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n8 = n>>3;
            int head = n8<<3;
            for(i=0; i<head; i+=8)
            {
                Avx2.Store(
                    Dst+i,
                    Avx2.LoadVector256(Src+i)
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] = Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rcopymul()

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rcopymul(
        int n,
        double vSrc,
        double *Src,
        double *Dst)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_vsrc = Avx2.BroadcastScalarToVector256(&vSrc);
            for(i=0; i<head; i+=4)
            {
                Avx2.Store(
                    Dst+i,
                    Avx2.Multiply(
                        Avx2.LoadVector256(Src+i),
                        avx_vsrc)
                    );
            }
            for(i=head; i<n; i++)
                Dst[i] = vSrc*Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rset()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rset(
        int n,
        double vDst,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            Intrinsics.Vector256<double> avx_vdst = Avx2.BroadcastScalarToVector256(&vDst);
            for(i=0; i<head; i+=4)
                Avx2.Store(Dst+i, avx_vdst);
            for(i=head; i<n; i++)
                Dst[i] = vDst;
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for mergemul()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmergemul(
        int n,
        double *Src,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
                Avx2.Store(
                    Dst+i,
                    Avx2.Multiply(
                        Avx2.LoadVector256(Dst+i),
                        Avx2.LoadVector256(Src+i)
                        )
                    );
            for(i=head; i<n; i++)
                Dst[i] *= Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for mergediv()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmergediv(
        int n,
        double *Src,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
                Avx2.Store(
                    Dst+i,
                    Avx2.Divide(
                        Avx2.LoadVector256(Dst+i),
                        Avx2.LoadVector256(Src+i)
                        )
                    );
            for(i=head; i<n; i++)
                Dst[i] /= Src[i];
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for mergemax()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmergemax(
        int n,
        double *Src,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
                Avx2.Store(
                    Dst+i,
                    Avx2.Max(
                        Avx2.LoadVector256(Dst+i),
                        Avx2.LoadVector256(Src+i)
                        )
                    );
            for(i=head; i<n; i++)
                Dst[i] = System.Math.Max(Dst[i],Src[i]);
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for mergemin()
    
      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rmergemin(
        int n,
        double *Src,
        double *Dst)
    {   
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        if( Avx2.IsSupported )
        {
            int i;
            int n4 = n>>2;
            int head = n4<<2;
            for(i=0; i<head; i+=4)
                Avx2.Store(
                    Dst+i,
                    Avx2.Min(
                        Avx2.LoadVector256(Dst+i),
                        Avx2.LoadVector256(Src+i)
                        )
                    );
            for(i=head; i<n; i++)
                Dst[i] = System.Math.Min(Dst[i],Src[i]);
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

        /*************************************************************************
        Performs inplace addition of Y[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddv(int n,
            double alpha,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_raddv(n, alpha, py, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] + alpha * y[i];
            }
        }

        /*************************************************************************
        Performs inplace addition of Y[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   source vector
            OffsY   -   source offset
            X       -   destination vector
            OffsX   -   destination offset

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddvx(int n,
            double alpha,
            double[] y,
            int offsy,
            double[] x,
            int offsx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_raddv(n, alpha, py+offsy, px+offsx) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[offsx + i] = x[offsx + i] + alpha * y[offsy + i];
            }
        }


        /*************************************************************************
        Performs inplace addition of vector Y[] to row X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   vector to add
            X       -   target row RowIdx

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddvr(int n,
            double alpha,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_raddv(n, alpha, py, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = x[rowidx, i] + alpha * y[i];
            }
        }

        /*************************************************************************
        Performs inplace addition of Y[]*Z[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X + Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            xparams _params)
        {
            int i = 0;
#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y, pz=z)
                {
                    if( try_rmuladdv(n, py, pz, px) )
                        return;
                }
            }
#endif
            for (i = 0; i <= n - 1; i++)
                x[i] += y[i] * z[i];
        }

        /*************************************************************************
        Performs inplace subtraction of Y[]*Z[] from X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process

        RESULT:
            X := X - Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rnegmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            xparams _params)
        {
            int i = 0;
#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y, pz=z)
                {
                    if( try_rnegmuladdv(n, py, pz, px) )
                        return;
                }
            }
#endif
            for (i = 0; i <= n - 1; i++)
                x[i] -= y[i] * z[i];
        }

        /*************************************************************************
        Performs addition of Y[]*Z[] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process
            R       -   array[N], vector to process

        RESULT:
            R := X + Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            double[] r,
            xparams _params)
        {
            int i = 0;
#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y, pz=z, pr=r)
                {
                    if( try_rcopymuladdv(n, py, pz, px, pr) )
                        return;
                }
            }
#endif
            for (i = 0; i <= n - 1; i++)
                r[i] = x[i] + y[i] * z[i];
        }

        /*************************************************************************
        Performs subtraction of Y[]*Z[] from X[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   array[N], vector to process
            Z       -   array[N], vector to process
            X       -   array[N], vector to process
            R       -   array[N], vector to process

        RESULT:
            R := X - Y*Z

          -- ALGLIB --
             Copyright 29.10.2021 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopynegmuladdv(int n,
            double[] y,
            double[] z,
            double[] x,
            double[] r,
            xparams _params)
        {
            int i = 0;
#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y, pz=z, pr=r)
                {
                    if( try_rcopynegmuladdv(n, py, pz, px, pr) )
                        return;
                }
            }
#endif
            for (i = 0; i <= n - 1; i++)
                r[i] = x[i] - y[i] * z[i];
        }

        /*************************************************************************
        Performs componentwise multiplication of vector X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemul(n, py, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] * y[i];
            }
        }

        /*************************************************************************
        Performs componentwise multiplication of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemul(n, py, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = x[rowidx, i] * y[i];
            }
        }

        /*************************************************************************
        Performs componentwise multiplication of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X*Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemulrv(int n,
            double[,] y,
            int rowidx,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemul(n, py+rowidx*y.GetLength(1), px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] * y[rowidx, i];
            }
        }



        /*************************************************************************
        Performs componentwise division of vector X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target vector

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergediv(n, py, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] / y[i];
            }
        }

        /*************************************************************************
        Performs componentwise division of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergediv(n, py, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = x[rowidx, i] / y[i];
            }
        }

        /*************************************************************************
        Performs componentwise division of row X[] by vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to divide by
            X       -   target row RowIdx

        RESULT:
            X := componentwise(X/Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergedivrv(int n,
            double[,] y,
            int rowidx,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergediv(n, py+rowidx*y.GetLength(1), px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] / y[rowidx, i];
            }
        }

        /*************************************************************************
        Performs componentwise max of vector X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemax(n, py, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = System.Math.Max(x[i], y[i]);
            }
        }

        /*************************************************************************
        Performs componentwise max of row X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemax(n, py, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = System.Math.Max(x[rowidx, i], y[i]);
            }
        }

        /*************************************************************************
        Performs componentwise max of row X[I] and vector Y[] 

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix, I-th row is source
            X       -   target row RowIdx

        RESULT:
            Y := componentwise_max(Y,X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergemaxrv(int n,
            double[,] x,
            int rowidx,
            double[] y,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemax(n, px+rowidx*x.GetLength(1), py) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                y[i] = System.Math.Max(y[i], x[rowidx, i]);
            }
        }

        /*************************************************************************
        Performs componentwise max of vector X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target vector

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminv(int n,
            double[] y,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemin(n, py, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = System.Math.Min(x[i], y[i]);
            }
        }

        /*************************************************************************
        Performs componentwise max of row X[] and vector Y[]

        INPUT PARAMETERS:
            N       -   vector length
            Y       -   vector to multiply by
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminvr(int n,
            double[] y,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemin(n, py, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = System.Math.Min(x[rowidx, i], y[i]);
            }
        }

        /*************************************************************************
        Performs componentwise max of row X[I] and vector Y[] 

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix, I-th row is source
            X       -   target row RowIdx

        RESULT:
            X := componentwise_max(X,Y)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmergeminrv(int n,
            double[,] x,
            int rowidx,
            double[] y,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rmergemin(n, px+rowidx*x.GetLength(1), py) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                y[i] = System.Math.Min(y[i], x[rowidx, i]);
            }
        }

        /*************************************************************************
        Performs inplace addition of Y[RIdx,...] to X[]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[?,N], matrix whose RIdx-th row is added
            RIdx    -   row index
            X       -   array[N], vector to process

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddrv(int n,
            double alpha,
            double[,] y,
            int ridx,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_raddv(n, alpha, py+ridx*y.GetLength(1), px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] + alpha * y[ridx, i];
            }
        }

        /*************************************************************************
        Performs inplace addition of Y[RIdx,...] to X[RIdxDst]

        INPUT PARAMETERS:
            N       -   vector length
            Alpha   -   multiplier
            Y       -   array[?,N], matrix whose RIdxSrc-th row is added
            RIdxSrc -   source row index
            X       -   array[?,N], matrix whose RIdxDst-th row is target
            RIdxDst -   destination row index

        RESULT:
            X := X + alpha*Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void raddrr(int n,
            double alpha,
            double[,] y,
            int ridxsrc,
            double[,] x,
            int ridxdst,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_raddv(n, alpha, py+ridxsrc*y.GetLength(1), px+ridxdst*x.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[ridxdst, i] = x[ridxdst, i] + alpha * y[ridxsrc, i];
            }
        }

        /*************************************************************************
        Performs inplace multiplication of X[] by V

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulv(int n,
            double v,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rmulv(n, v, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = x[i] * v;
            }
        }

        /*************************************************************************
        Performs inplace multiplication of X[] by V

        INPUT PARAMETERS:
            N       -   row length
            X       -   array[?,N], row to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 of row RowIdx are multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulr(int n,
            double v,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rmulv(n, v, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif


            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = x[rowidx, i] * v;
            }
        }

        /*************************************************************************
        Performs inplace computation of Sqrt(X)

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 replaced by Sqrt(X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsqrtv(int n,
            double[] x,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rsqrtv(n, px) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[i] = System.Math.Sqrt(x[i]);
            }
        }

        /*************************************************************************
        Performs inplace computation of Sqrt(X[RowIdx,*])

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[?,N], matrix to process

        OUTPUT PARAMETERS:
            X       -   elements 0...N-1 replaced by Sqrt(X)

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsqrtr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rsqrtv(n, px+rowidx*x.GetLength(1)) )
                        return;
                }
            }
#endif


            for (i = 0; i <= n - 1; i++)
            {
                x[rowidx, i] = System.Math.Sqrt(x[rowidx, i]);
            }
        }

        /*************************************************************************
        Performs inplace multiplication of X[OffsX:OffsX+N-1] by V

        INPUT PARAMETERS:
            N       -   subvector length
            X       -   vector to process
            V       -   multiplier

        OUTPUT PARAMETERS:
            X       -   elements OffsX:OffsX+N-1 multiplied by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rmulvx(int n,
            double v,
            double[] x,
            int offsx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rmulv(n, v, px+offsx) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                x[offsx + i] = x[offsx + i] * v;
            }
        }

        /*************************************************************************
        Returns maximum X

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            max(X[i])
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxv(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            if (n <= 0)
            {
                result = 0;
                return result;
            }
            result = x[0];
            for (i = 1; i <= n - 1; i++)
            {
                v = x[i];
                if (v > result)
                {
                    result = v;
                }
            }
            return result;
        }

        /*************************************************************************
        Returns maximum |X|

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], vector to process

        OUTPUT PARAMETERS:
            max(|X[i]|)
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxabsv(int n,
            double[] x,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            result = 0;
            for (i = 0; i <= n - 1; i++)
            {
                v = System.Math.Abs(x[i]);
                if (v > result)
                {
                    result = v;
                }
            }
            return result;
        }

        /*************************************************************************
        Returns maximum X

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix to process, RowIdx-th row is processed

        OUTPUT PARAMETERS:
            max(X[RowIdx,i])
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            if (n <= 0)
            {
                result = 0;
                return result;
            }
            result = x[rowidx, 0];
            for (i = 1; i <= n - 1; i++)
            {
                v = x[rowidx, i];
                if (v > result)
                {
                    result = v;
                }
            }
            return result;
        }

        /*************************************************************************
        Returns maximum |X|

        INPUT PARAMETERS:
            N       -   vector length
            X       -   matrix to process, RowIdx-th row is processed

        OUTPUT PARAMETERS:
            max(|X[RowIdx,i]|)
            zero for N=0

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static double rmaxabsr(int n,
            double[,] x,
            int rowidx,
            xparams _params)
        {
            double result = 0;
            int i = 0;
            double v = 0;

            result = 0;
            for (i = 0; i <= n - 1; i++)
            {
                v = System.Math.Abs(x[rowidx, i]);
                if (v > result)
                {
                    result = v;
                }
            }
            return result;
        }

        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetv(int n,
            double v,
            double[] x,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rset(n, v, px) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                x[j] = v;
            }
        }

        /*************************************************************************
        Sets X[OffsX:OffsX+N-1] to V

        INPUT PARAMETERS:
            N       -   subvector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   X[OffsX:OffsX+N-1] is replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetvx(int n,
            double v,
            double[] x,
            int offsx,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x)
                {
                    if( try_rset(n, v, px+offsx) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                x[offsx + j] = v;
            }
        }

        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void isetv(int n,
            int v,
            int[] x,
            xparams _params)
        {
            int j = 0;

            for (j = 0; j <= n - 1; j++)
            {
                x[j] = v;
            }
        }

        /*************************************************************************
        Sets vector X[] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            X       -   array[N]

        OUTPUT PARAMETERS:
            X       -   leading N elements are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void bsetv(int n,
            bool v,
            bool[] x,
            xparams _params)
        {
            int j = 0;

            for (j = 0; j <= n - 1; j++)
            {
                x[j] = v;
            }
        }

        /*************************************************************************
        Sets matrix A[] to V

        INPUT PARAMETERS:
            M, N    -   rows/cols count
            V       -   value to set
            A       -   array[M,N]

        OUTPUT PARAMETERS:
            A       -   leading M rows, N cols are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetm(int m,
            int n,
            double v,
            double[,] a,
            xparams _params)
        {
            int i = 0;
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* pa=a)
                {
                    for(i=0; i<m; i++)
                    {
                        double *prow = pa+i*a.GetLength(1);
                        if( !try_rset(n, v, prow) )
                        {
                            for(j=0; j<n; j++)
                                prow[j] = v;
                        }
                    }
                }
                return;
            }
#endif

            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = v;
                }
            }
        }

        /*************************************************************************
        Sets row I of A[,] to V

        INPUT PARAMETERS:
            N       -   vector length
            V       -   value to set
            A       -   array[N,N] or larger
            I       -   row index

        OUTPUT PARAMETERS:
            A       -   leading N elements of I-th row are replaced by V

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rsetr(int n,
            double v,
            double[,] a,
            int i,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* pa=a)
                {
                    if( try_rset(n, v, pa+i*a.GetLength(1)) )
                        return;
                }
            }
#endif


            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = v;
            }
        }

        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   leading N elements are replaced by X


        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyv(int n,
            double[] x,
            double[] y,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rcopy(n, px, py) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                y[j] = x[j];
            }
        }

        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   leading N elements are replaced by X


        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void bcopyv(int n,
            bool[] x,
            bool[] y,
            xparams _params)
        {
            int j = 0;

            for (j = 0; j <= n - 1; j++)
            {
                y[j] = x[j];
            }
        }

        /*************************************************************************
        Copies vector X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   X copied to Y

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void icopyv(int n,
            int[] x,
            int[] y,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(int* px=x, py=y)
                {
                    if( try_icopy(n, px, py) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                y[j] = x[j];
            }
        }

        /*************************************************************************
        Performs copying with multiplication of V*X[] to Y[]

        INPUT PARAMETERS:
            N       -   vector length
            V       -   multiplier
            X       -   array[N], source
            Y       -   preallocated array[N]

        OUTPUT PARAMETERS:
            Y       -   array[N], Y = V*X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymulv(int n,
            double v,
            double[] x,
            double[] y,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rcopymul(n, v, px, py) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                y[i] = v * x[i];
            }
        }

        /*************************************************************************
        Performs copying with multiplication of V*X[] to Y[I,*]

        INPUT PARAMETERS:
            N       -   vector length
            V       -   multiplier
            X       -   array[N], source
            Y       -   preallocated array[?,N]
            RIdx    -   destination row index

        OUTPUT PARAMETERS:
            Y       -   Y[RIdx,...] = V*X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopymulvr(int n,
            double v,
            double[] x,
            double[,] y,
            int ridx,
            xparams _params)
        {
            int i = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rcopymul(n, v, px, py+ridx*y.GetLength(1)) )
                        return;
                }
            }
#endif

            for (i = 0; i <= n - 1; i++)
            {
                y[ridx, i] = v * x[i];
            }
        }

        /*************************************************************************
        Copies vector X[] to row I of A[,]

        INPUT PARAMETERS:
            N       -   vector length
            X       -   array[N], source
            A       -   preallocated 2D array large enough to store result
            I       -   destination row index

        OUTPUT PARAMETERS:
            A       -   leading N elements of I-th row are replaced by X

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyvr(int n,
            double[] x,
            double[,] a,
            int i,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, pa=a)
                {
                    if( try_rcopy(n, px, pa+i*a.GetLength(1)) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = x[j];
            }
        }

        /*************************************************************************
        Copies row I of A[,] to vector X[]

        INPUT PARAMETERS:
            N       -   vector length
            A       -   2D array, source
            I       -   source row index
            X       -   preallocated destination

        OUTPUT PARAMETERS:
            X       -   array[N], destination

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyrv(int n,
            double[,] a,
            int i,
            double[] x,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, pa=a)
                {
                    if( try_rcopy(n, pa+i*a.GetLength(1), px) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                x[j] = a[i, j];
            }
        }

        /*************************************************************************
        Copies row I of A[,] to row K of B[,].

        A[i,...] and B[k,...] may overlap.

        INPUT PARAMETERS:
            N       -   vector length
            A       -   2D array, source
            I       -   source row index
            B       -   preallocated destination
            K       -   destination row index

        OUTPUT PARAMETERS:
            B       -   row K overwritten

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyrr(int n,
            double[,] a,
            int i,
            double[,] b,
            int k,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* pa=a, pb=b)
                {
                    if( try_rcopy(n, pa+i*a.GetLength(1), pb+k*b.GetLength(1)) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                b[k, j] = a[i, j];
            }
        }

        /*************************************************************************
        Copies vector X[] to Y[], extended version

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            OffsX   -   source offset
            Y       -   preallocated array[N]
            OffsY   -   destination offset

        OUTPUT PARAMETERS:
            Y       -   N elements starting from OffsY are replaced by X[OffsX:OffsX+N-1]

        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void rcopyvx(int n,
            double[] x,
            int offsx,
            double[] y,
            int offsy,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(double* px=x, py=y)
                {
                    if( try_rcopy(n, px+offsx, py+offsy) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                y[offsy + j] = x[offsx + j];
            }
        }

        /*************************************************************************
        Copies vector X[] to Y[], extended version

        INPUT PARAMETERS:
            N       -   vector length
            X       -   source array
            OffsX   -   source offset
            Y       -   preallocated array[N]
            OffsY   -   destination offset

        OUTPUT PARAMETERS:
            Y       -   N elements starting from OffsY are replaced by X[OffsX:OffsX+N-1]

        NOTE: destination and source should NOT overlap

          -- ALGLIB --
             Copyright 20.01.2020 by Bochkanov Sergey
        *************************************************************************/
        public static void icopyvx(int n,
            int[] x,
            int offsx,
            int[] y,
            int offsy,
            xparams _params)
        {
            int j = 0;

#if ALGLIB_USE_SIMD
        if( n>=_ABLASF_KERNEL_SIZE1 )
            unsafe
            {
                fixed(int* px=x, py=y)
                {
                    if( try_icopy(n, px+offsx, py+offsy) )
                        return;
                }
            }
#endif

            for (j = 0; j <= n - 1; j++)
            {
                y[offsy + j] = x[offsx + j];
            }
        }


#if ALGLIB_USE_SIMD
    /*************************************************************************
    SIMD kernel for rgemv() and rgemvx()

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    private static unsafe bool try_rgemv(
        int m,
        int n,
        double  alpha,
        double* a,
        int stride_a,
        int opa,
        double* x,
        double* y)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            if( opa==0 )
            {
                
                //
                // y += A*x
                //
                int n4 = n>>2;
                int head = n4<<2;
                double *a_row = a;
                int i, j;
                double *vdot = stackalloc double[4];
                for(i=0; i<m; i++)
                {
                    Intrinsics.Vector256<double> simd_dot = Intrinsics.Vector256<double>.Zero;
                    for(j=0; j<head; j+=4)
                        simd_dot = Fma.MultiplyAdd(Fma.LoadVector256(a_row+j), Fma.LoadVector256(x+j), simd_dot);
                    Fma.Store(vdot, simd_dot);
                    for(j=head; j<n; j++)
                        vdot[0] += a_row[j]*x[j];
                    double v = vdot[0]+vdot[1]+vdot[2]+vdot[3];
                    y[i] = alpha*v+y[i];
                    a_row += stride_a;
                }
                return true;
            }
            if( opa==1 )
            {
                
                //
                // y += A^T*x
                //
                double *a_row = a;
                int i, j;
                int m4 = m>>2;
                int head = m4<<2;
                for(i=0; i<n; i++)
                {
                    double v = alpha*x[i];
                    Intrinsics.Vector256<double> simd_v = Fma.BroadcastScalarToVector256(&v);
                    for(j=0; j<head; j+=4)
                        Fma.Store(y+j, Fma.MultiplyAdd(Fma.LoadVector256(a_row+j), simd_v, Fma.LoadVector256(y+j)));
                    for(j=head; j<m; j++)
                        y[j] += v*a_row[j];
                    a_row += stride_a;
                }
                return true;
            }
            return false;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            if( opa==0 )
            {
                
                //
                // y += A*x
                //
                int n4 = n>>2;
                int head = n4<<2;
                double *a_row = a;
                int i, j;
                double *vdot = stackalloc double[4];
                for(i=0; i<m; i++)
                {
                    Intrinsics.Vector256<double> simd_dot = Intrinsics.Vector256<double>.Zero;
                    for(j=0; j<head; j+=4)
                        simd_dot = Avx2.Add(Avx2.Multiply(Avx2.LoadVector256(a_row+j), Avx2.LoadVector256(x+j)), simd_dot);
                    Avx2.Store(vdot, simd_dot);
                    for(j=head; j<n; j++)
                        vdot[0] += a_row[j]*x[j];
                    double v = vdot[0]+vdot[1]+vdot[2]+vdot[3];
                    y[i] = alpha*v+y[i];
                    a_row += stride_a;
                }
                return true;
            }
            if( opa==1 )
            {
                
                //
                // y += A^T*x
                //
                double *a_row = a;
                int i, j;
                int m4 = m>>2;
                int head = m4<<2;
                for(i=0; i<n; i++)
                {
                    double v = alpha*x[i];
                    Intrinsics.Vector256<double> simd_v = Avx2.BroadcastScalarToVector256(&v);
                    for(j=0; j<head; j+=4)
                        Avx2.Store(y+j, Avx2.Add(Avx2.Multiply(Avx2.LoadVector256(a_row+j), simd_v), Avx2.LoadVector256(y+j)));
                    for(j=head; j<m; j++)
                        y[j] += v*a_row[j];
                    a_row += stride_a;
                }
                return true;
            }
            return false;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif

        /*************************************************************************
        Matrix-vector product: y := alpha*op(A)*x + beta*y

        NOTE: this  function  expects  Y  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, x, y.

        INPUT PARAMETERS:
            M   -   number of rows of op(A)
            N   -   number of columns of op(A)
            Alpha-  coefficient
            A   -   source matrix
            OpA -   operation type:
                    * OpA=0     =>  op(A) = A
                    * OpA=1     =>  op(A) = A^T
            X   -   input vector, has at least N elements
            Beta-   coefficient
            Y   -   preallocated output array, has at least M elements

        OUTPUT PARAMETERS:
            Y   -   vector which stores result

        HANDLING OF SPECIAL CASES:
            * if M=0, then subroutine does nothing. It does not even touch arrays.
            * if N=0 or Alpha=0.0, then:
              * if Beta=0, then Y is filled by zeros. A and X are  not  referenced
                at all. Initial values of Y are ignored (we do not  multiply  Y by
                zero, we just rewrite it by zeros)
              * if Beta<>0, then Y is replaced by Beta*Y
            * if M>0, N>0, Alpha<>0, but  Beta=0,  then  Y  is  replaced  by  A*x;
               initial state of Y is ignored (rewritten by  A*x,  without  initial
               multiplication by zeros).


          -- ALGLIB routine --

             01.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rgemv(int m,
            int n,
            double alpha,
            double[,] a,
            int opa,
            double[] x,
            double beta,
            double[] y,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;


            //
            // Properly premultiply Y by Beta.
            //
            // Quick exit for M=0, N=0 or Alpha=0.
            // After this block we have M>0, N>0, Alpha<>0.
            //
            if (m <= 0)
            {
                return;
            }
            if ((double)(beta) != (double)(0))
            {
                rmulv(m, beta, y, _params);
            }
            else
            {
                rsetv(m, 0.0, y, _params);
            }
            if (n <= 0 || (double)(alpha) == (double)(0.0))
            {
                return;
            }

            //
            // Try fast kernel
            //
#if ALGLIB_USE_SIMD
        if( (opa==0 && n>=_ABLASF_KERNEL_SIZE2) || (opa==1 && m>=_ABLASF_KERNEL_SIZE2) )
            unsafe
            {
                fixed(double* pa=a, px=x, py=y)
                {
                    if( try_rgemv(m, n, alpha, pa, a.GetLength(1), opa, px, py) )
                        return;
                }
            }
#endif

            //
            // Generic code
            //
            if (opa == 0)
            {

                //
                // y += A*x
                //
                for (i = 0; i <= m - 1; i++)
                {
                    v = 0;
                    for (j = 0; j <= n - 1; j++)
                    {
                        v = v + a[i, j] * x[j];
                    }
                    y[i] = alpha * v + y[i];
                }
                return;
            }
            if (opa == 1)
            {

                //
                // y += A^T*x
                //
                for (i = 0; i <= n - 1; i++)
                {
                    v = alpha * x[i];
                    for (j = 0; j <= m - 1; j++)
                    {
                        y[j] = y[j] + v * a[i, j];
                    }
                }
                return;
            }
        }

        /*************************************************************************
        Matrix-vector product: y := alpha*op(A)*x + beta*y

        Here x, y, A are subvectors/submatrices of larger vectors/matrices.

        NOTE: this  function  expects  Y  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, x, y.

        INPUT PARAMETERS:
            M   -   number of rows of op(A)
            N   -   number of columns of op(A)
            Alpha-  coefficient
            A   -   source matrix
            IA  -   submatrix offset (row index)
            JA  -   submatrix offset (column index)
            OpA -   operation type:
                    * OpA=0     =>  op(A) = A
                    * OpA=1     =>  op(A) = A^T
            X   -   input vector, has at least N+IX elements
            IX  -   subvector offset
            Beta-   coefficient
            Y   -   preallocated output array, has at least M+IY elements
            IY  -   subvector offset

        OUTPUT PARAMETERS:
            Y   -   vector which stores result

        HANDLING OF SPECIAL CASES:
            * if M=0, then subroutine does nothing. It does not even touch arrays.
            * if N=0 or Alpha=0.0, then:
              * if Beta=0, then Y is filled by zeros. A and X are  not  referenced
                at all. Initial values of Y are ignored (we do not  multiply  Y by
                zero, we just rewrite it by zeros)
              * if Beta<>0, then Y is replaced by Beta*Y
            * if M>0, N>0, Alpha<>0, but  Beta=0,  then  Y  is  replaced  by  A*x;
               initial state of Y is ignored (rewritten by  A*x,  without  initial
               multiplication by zeros).


          -- ALGLIB routine --

             01.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rgemvx(int m,
            int n,
            double alpha,
            double[,] a,
            int ia,
            int ja,
            int opa,
            double[] x,
            int ix,
            double beta,
            double[] y,
            int iy,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;


            //
            // Properly premultiply Y by Beta.
            //
            // Quick exit for M=0, N=0 or Alpha=0.
            // After this block we have M>0, N>0, Alpha<>0.
            //
            if (m <= 0)
            {
                return;
            }
            if ((double)(beta) != (double)(0))
            {
                rmulvx(m, beta, y, iy, _params);
            }
            else
            {
                rsetvx(m, 0.0, y, iy, _params);
            }
            if (n <= 0 || (double)(alpha) == (double)(0.0))
            {
                return;
            }

            //
            // Try fast kernel
            //
#if ALGLIB_USE_SIMD
        if( (opa==0 && n>=_ABLASF_KERNEL_SIZE2) || (opa==1 && m>=_ABLASF_KERNEL_SIZE2) )
            unsafe
            {
                fixed(double* pa=a, px=x, py=y)
                {
                    if( try_rgemv(m, n, alpha, pa+ia*a.GetLength(1)+ja, a.GetLength(1), opa, px+ix, py+iy) )
                        return;
                }
            }
#endif

            //
            // Generic code
            //
            if (opa == 0)
            {

                //
                // y += A*x
                //
                for (i = 0; i <= m - 1; i++)
                {
                    v = 0;
                    for (j = 0; j <= n - 1; j++)
                    {
                        v = v + a[ia + i, ja + j] * x[ix + j];
                    }
                    y[iy + i] = alpha * v + y[iy + i];
                }
                return;
            }
            if (opa == 1)
            {

                //
                // y += A^T*x
                //
                for (i = 0; i <= n - 1; i++)
                {
                    v = alpha * x[ix + i];
                    for (j = 0; j <= m - 1; j++)
                    {
                        y[iy + j] = y[iy + j] + v * a[ia + i, ja + j];
                    }
                }
                return;
            }
        }


        /*************************************************************************
        Rank-1 correction: A := A + alpha*u*v'

        NOTE: this  function  expects  A  to  be  large enough to store result. No
              automatic preallocation happens for  smaller  arrays.  No  integrity
              checks is performed for sizes of A, u, v.

        INPUT PARAMETERS:
            M   -   number of rows
            N   -   number of columns
            A   -   target MxN matrix
            Alpha-  coefficient
            U   -   vector #1
            V   -   vector #2


          -- ALGLIB routine --
             07.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void rger(int m,
            int n,
            double alpha,
            double[] u,
            double[] v,
            double[,] a,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double s = 0;

            if ((m <= 0 || n <= 0) || (double)(alpha) == (double)(0))
            {
                return;
            }
            for (i = 0; i <= m - 1; i++)
            {
                s = alpha * u[i];
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = a[i, j] + s * v[j];
                }
            }
        }


        /*************************************************************************
        This subroutine solves linear system op(A)*x=b where:
        * A is NxN upper/lower triangular/unitriangular matrix
        * X and B are Nx1 vectors
        * "op" may be identity transformation or transposition

        Solution replaces X.

        IMPORTANT: * no overflow/underflow/denegeracy tests is performed.
                   * no integrity checks for operand sizes, out-of-bounds accesses
                     and so on is performed

        INPUT PARAMETERS
            N   -   matrix size, N>=0
            A       -   matrix, actial matrix is stored in A[IA:IA+N-1,JA:JA+N-1]
            IA      -   submatrix offset
            JA      -   submatrix offset
            IsUpper -   whether matrix is upper triangular
            IsUnit  -   whether matrix is unitriangular
            OpType  -   transformation type:
                        * 0 - no transformation
                        * 1 - transposition
            X       -   right part, actual vector is stored in X[IX:IX+N-1]
            IX      -   offset

        OUTPUT PARAMETERS
            X       -   solution replaces elements X[IX:IX+N-1]

          -- ALGLIB routine --
             (c) 07.09.2021 Bochkanov Sergey
        *************************************************************************/
        public static void rtrsvx(int n,
            double[,] a,
            int ia,
            int ja,
            bool isupper,
            bool isunit,
            int optype,
            double[] x,
            int ix,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            double v = 0;

            if (n <= 0)
            {
                return;
            }
            if (optype == 0 && isupper)
            {
                for (i = n - 1; i >= 0; i--)
                {
                    v = x[ix + i];
                    for (j = i + 1; j <= n - 1; j++)
                    {
                        v = v - a[ia + i, ja + j] * x[ix + j];
                    }
                    if (!isunit)
                    {
                        v = v / a[ia + i, ja + i];
                    }
                    x[ix + i] = v;
                }
                return;
            }
            if (optype == 0 && !isupper)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    v = x[ix + i];
                    for (j = 0; j <= i - 1; j++)
                    {
                        v = v - a[ia + i, ja + j] * x[ix + j];
                    }
                    if (!isunit)
                    {
                        v = v / a[ia + i, ja + i];
                    }
                    x[ix + i] = v;
                }
                return;
            }
            if (optype == 1 && isupper)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    v = x[ix + i];
                    if (!isunit)
                    {
                        v = v / a[ia + i, ja + i];
                    }
                    x[ix + i] = v;
                    if (v == 0)
                    {
                        continue;
                    }
                    for (j = i + 1; j <= n - 1; j++)
                    {
                        x[ix + j] = x[ix + j] - v * a[ia + i, ja + j];
                    }
                }
                return;
            }
            if (optype == 1 && !isupper)
            {
                for (i = n - 1; i >= 0; i--)
                {
                    v = x[ix + i];
                    if (!isunit)
                    {
                        v = v / a[ia + i, ja + i];
                    }
                    x[ix + i] = v;
                    if (v == 0)
                    {
                        continue;
                    }
                    for (j = 0; j <= i - 1; j++)
                    {
                        x[ix + j] = x[ix + j] - v * a[ia + i, ja + j];
                    }
                }
                return;
            }
            ap.assert(false, "rTRSVX: unexpected operation type");
        }


        /*************************************************************************
        Fast kernel (new version with AVX2/FMA)

          -- ALGLIB routine --
             19.09.2021
             Bochkanov Sergey
        *************************************************************************/
#if ALGLIB_USE_SIMD
    /*************************************************************************
    Block packing function for fast rGEMM. Loads long  WIDTH*LENGTH  submatrix
    with LENGTH<=BLOCK_SIZE and WIDTH<=MICRO_SIZE into contiguous  MICRO_SIZE*
    BLOCK_SIZE row-wise 'horizontal' storage (hence H in the function name).

    The matrix occupies first ROUND_LENGTH cols of the  storage  (with  LENGTH
    being rounded up to nearest SIMD granularity).  ROUND_LENGTH  is  returned
    as result. It is guaranteed that ROUND_LENGTH depends only on LENGTH,  and
    that it will be same for all function calls.

    Unused rows and columns in [LENGTH,ROUND_LENGTH) range are filled by zeros;
    unused cols in [ROUND_LENGTH,BLOCK_SIZE) range are ignored.

    * op=0 means that source is an WIDTH*LENGTH matrix stored with  src_stride
      stride. The matrix is NOT transposed on load.
    * op=1 means that source is an LENGTH*WIDTH matrix  stored with src_stride
      that is loaded with transposition
    * present version of the function supports only MICRO_SIZE=2, the behavior
      is undefined for other micro sizes.
    * the target is properly aligned; the source can be unaligned.

    Requires AVX2, does NOT check its presense.

    The function is present in two versions, one  with  variable  opsrc_length
    and another one with opsrc_length==block_size==32.

      -- ALGLIB routine --
         19.07.2021
         Bochkanov Sergey
    *************************************************************************/
    private static unsafe int ablasf_packblkh_avx2(
        double *src,
        int src_stride,
        int op,
        int opsrc_length,
        int opsrc_width,
        double   *dst,
        int block_size,
        int micro_size)
    {
        int i;
        
        /*
         * Write to the storage
         */
        if( op==0 )
        {
            /*
             * Copy without transposition
             */
            int len8=(opsrc_length>>3)<<3;
            double *src1 = src+src_stride;
            double *dst1 = dst+block_size;
            if( opsrc_width==2 )
            {
                /*
                 * Width=2
                 */
                for(i=0; i<len8; i+=8)
                {
                    Avx2.StoreAligned(dst+i,    Avx2.LoadVector256(src+i));
                    Avx2.StoreAligned(dst+i+4,  Avx2.LoadVector256(src+i+4));
                    Avx2.StoreAligned(dst1+i,   Avx2.LoadVector256(src1+i));
                    Avx2.StoreAligned(dst1+i+4, Avx2.LoadVector256(src1+i+4));
                }
                for(i=len8; i<opsrc_length; i++)
                {
                    dst[i]  = src[i];
                    dst1[i] = src1[i];
                }
            }
            else
            {
                /*
                 * Width=1, pad by zeros
                 */
                Intrinsics.Vector256<double> vz = Intrinsics.Vector256<double>.Zero;
                for(i=0; i<len8; i+=8)
                {
                    Avx2.StoreAligned(dst+i,    Avx2.LoadVector256(src+i));
                    Avx2.StoreAligned(dst+i+4,  Avx2.LoadVector256(src+i+4));
                    Avx2.StoreAligned(dst1+i,   vz);
                    Avx2.StoreAligned(dst1+i+4, vz);
                }
                for(i=len8; i<opsrc_length; i++)
                {
                    dst[i]  = src[i];
                    dst1[i] = 0.0;
                }
            }
        }
        else
        {
            /*
             * Copy with transposition
             */
            int stride2 = src_stride<<1;
            int stride3 = src_stride+stride2;
            int stride4 = src_stride<<2;
            int len4=(opsrc_length>>2)<<2;
            double *srci = src;
            double *dst1 = dst+block_size;
            if( opsrc_width==2 )
            {
                /*
                 * Width=2
                 */
                for(i=0; i<len4; i+=4)
                {
                    Intrinsics.Vector128<double> s0 = Sse2.LoadVector128(srci),         s1 = Sse2.LoadVector128(srci+src_stride);
                    Intrinsics.Vector128<double> s2 = Sse2.LoadVector128(srci+stride2), s3 = Sse2.LoadVector128(srci+stride3);
                    Sse2.Store(dst+i,    Sse2.UnpackLow( s0,s1));
                    Sse2.Store(dst1+i,   Sse2.UnpackHigh(s0,s1));
                    Sse2.Store(dst+i+2,  Sse2.UnpackLow( s2,s3));
                    Sse2.Store(dst1+i+2, Sse2.UnpackHigh(s2,s3));
                    srci += stride4;
                }
                for(i=len4; i<opsrc_length; i++)
                {
                    dst[i]  = srci[0];
                    dst1[i] = srci[1];
                    srci += src_stride;
                }
            }
            else
            {
                /*
                 * Width=1, pad by zeros
                 */
                Intrinsics.Vector128<double> vz = Intrinsics.Vector128<double>.Zero;
                for(i=0; i<len4; i+=4)
                {
                    Intrinsics.Vector128<double> s0 = Sse2.LoadVector128(srci), s1 = Sse2.LoadVector128(srci+src_stride);
                    Intrinsics.Vector128<double> s2 = Sse2.LoadVector128(srci+stride2), s3 = Sse2.LoadVector128(srci+stride3);
                    Sse2.Store(dst+i,    Sse2.UnpackLow(s0,s1));
                    Sse2.Store(dst+i+2,  Sse2.UnpackLow(s2,s3));
                    Sse2.Store(dst1+i,   vz);
                    Sse2.Store(dst1+i+2, vz);
                    srci += stride4;
                }
                for(i=len4; i<opsrc_length; i++)
                {
                    dst[i]  = srci[0];
                    dst1[i] = 0.0;
                    srci += src_stride;
                }
            }
        }
        
        /*
         * Pad by zeros, if needed
         */
        int round_length = ((opsrc_length+3)>>2)<<2;
        for(i=opsrc_length; i<round_length; i++)
        {
            dst[i] = 0;
            dst[i+block_size] = 0;
        }
        return round_length;
    }
    
    /*************************************************************************
    Computes  product   A*transpose(B)  of two MICRO_SIZE*ROUND_LENGTH rowwise 
    'horizontal' matrices, stored with stride=block_size, and writes it to the
    row-wise matrix C.

    ROUND_LENGTH is expected to be properly SIMD-rounded length,  as  returned
    by ablasf_packblkh_avx2().

    Present version of the function supports only MICRO_SIZE=2,  the  behavior
    is undefined for other micro sizes.

    Assumes that at least AVX2 is present; additionally checks for FMA and tries
    to use it.

      -- ALGLIB routine --
         19.07.2021
         Bochkanov Sergey
    *************************************************************************/
    private static unsafe void ablasf_dotblkh_avx2_fma(
        double *src_a,
        double *src_b,
        int round_length,
        int block_size,
        int micro_size,
        double *dst,
        int dst_stride)
    {
        int z;
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            /*
             * Try FMA version
             */
            Intrinsics.Vector256<double> r00 = Intrinsics.Vector256<double>.Zero,
                                         r01 = Intrinsics.Vector256<double>.Zero,
                                         r10 = Intrinsics.Vector256<double>.Zero,
                                         r11 = Intrinsics.Vector256<double>.Zero;
            if( (round_length&0x7)!=0 )
            {
                /*
                 * round_length is multiple of 4, but not multiple of 8
                 */
                for(z=0; z<round_length; z+=4, src_a+=4, src_b+=4)
                {
                    Intrinsics.Vector256<double> a0 = Fma.LoadAlignedVector256(src_a);
                    Intrinsics.Vector256<double> a1 = Fma.LoadAlignedVector256(src_a+block_size);
                    Intrinsics.Vector256<double> b0 = Fma.LoadAlignedVector256(src_b);
                    Intrinsics.Vector256<double> b1 = Fma.LoadAlignedVector256(src_b+block_size);
                    r00 = Fma.MultiplyAdd(a0, b0, r00);
                    r01 = Fma.MultiplyAdd(a0, b1, r01);
                    r10 = Fma.MultiplyAdd(a1, b0, r10);
                    r11 = Fma.MultiplyAdd(a1, b1, r11);
                }
            }
            else
            {
                /*
                 * round_length is multiple of 8
                 */
                for(z=0; z<round_length; z+=8, src_a+=8, src_b+=8)
                {
                    Intrinsics.Vector256<double> a0 = Fma.LoadAlignedVector256(src_a);
                    Intrinsics.Vector256<double> a1 = Fma.LoadAlignedVector256(src_a+block_size);
                    Intrinsics.Vector256<double> b0 = Fma.LoadAlignedVector256(src_b);
                    Intrinsics.Vector256<double> b1 = Fma.LoadAlignedVector256(src_b+block_size);
                    Intrinsics.Vector256<double> c0 = Fma.LoadAlignedVector256(src_a+4);
                    Intrinsics.Vector256<double> c1 = Fma.LoadAlignedVector256(src_a+block_size+4);
                    Intrinsics.Vector256<double> d0 = Fma.LoadAlignedVector256(src_b+4);
                    Intrinsics.Vector256<double> d1 = Fma.LoadAlignedVector256(src_b+block_size+4);
                    r00 = Fma.MultiplyAdd(c0, d0, Fma.MultiplyAdd(a0, b0, r00));
                    r01 = Fma.MultiplyAdd(c0, d1, Fma.MultiplyAdd(a0, b1, r01));
                    r10 = Fma.MultiplyAdd(c1, d0, Fma.MultiplyAdd(a1, b0, r10));
                    r11 = Fma.MultiplyAdd(c1, d1, Fma.MultiplyAdd(a1, b1, r11));
                }
            }
            Intrinsics.Vector256<double> sum0 = Fma.HorizontalAdd(r00,r01);
            Intrinsics.Vector256<double> sum1 = Fma.HorizontalAdd(r10,r11);
            Sse2.Store(dst,            Sse2.Add(Fma.ExtractVector128(sum0,0), Fma.ExtractVector128(sum0,1)));
            Sse2.Store(dst+dst_stride, Sse2.Add(Fma.ExtractVector128(sum1,0), Fma.ExtractVector128(sum1,1)));
        }
        else
#endif // no-fma
        {
            /*
             * Only AVX2 is present
             */
            Intrinsics.Vector256<double> r00 = Intrinsics.Vector256<double>.Zero,
                                         r01 = Intrinsics.Vector256<double>.Zero,
                                         r10 = Intrinsics.Vector256<double>.Zero,
                                         r11 = Intrinsics.Vector256<double>.Zero;
            if( (round_length&0x7)!=0 )
            {
                /*
                 * round_length is multiple of 4, but not multiple of 8
                 */
                for(z=0; z<round_length; z+=4, src_a+=4, src_b+=4)
                {
                    Intrinsics.Vector256<double> a0 = Avx2.LoadAlignedVector256(src_a);
                    Intrinsics.Vector256<double> a1 = Avx2.LoadAlignedVector256(src_a+block_size);
                    Intrinsics.Vector256<double> b0 = Avx2.LoadAlignedVector256(src_b);
                    Intrinsics.Vector256<double> b1 = Avx2.LoadAlignedVector256(src_b+block_size);
                    r00 = Avx2.Add(Avx2.Multiply(a0, b0), r00);
                    r01 = Avx2.Add(Avx2.Multiply(a0, b1), r01);
                    r10 = Avx2.Add(Avx2.Multiply(a1, b0), r10);
                    r11 = Avx2.Add(Avx2.Multiply(a1, b1), r11);
                }
            }
            else
            {
                /*
                 * round_length is multiple of 8
                 */
                for(z=0; z<round_length; z+=8, src_a+=8, src_b+=8)
                {
                    Intrinsics.Vector256<double> a0 = Avx2.LoadAlignedVector256(src_a);
                    Intrinsics.Vector256<double> a1 = Avx2.LoadAlignedVector256(src_a+block_size);
                    Intrinsics.Vector256<double> b0 = Avx2.LoadAlignedVector256(src_b);
                    Intrinsics.Vector256<double> b1 = Avx2.LoadAlignedVector256(src_b+block_size);
                    Intrinsics.Vector256<double> c0 = Avx2.LoadAlignedVector256(src_a+4);
                    Intrinsics.Vector256<double> c1 = Avx2.LoadAlignedVector256(src_a+block_size+4);
                    Intrinsics.Vector256<double> d0 = Avx2.LoadAlignedVector256(src_b+4);
                    Intrinsics.Vector256<double> d1 = Avx2.LoadAlignedVector256(src_b+block_size+4);
                    r00 = Avx2.Add(Avx2.Multiply(c0, d0), Avx2.Add(Avx2.Multiply(a0, b0), r00));
                    r01 = Avx2.Add(Avx2.Multiply(c0, d1), Avx2.Add(Avx2.Multiply(a0, b1), r01));
                    r10 = Avx2.Add(Avx2.Multiply(c1, d0), Avx2.Add(Avx2.Multiply(a1, b0), r10));
                    r11 = Avx2.Add(Avx2.Multiply(c1, d1), Avx2.Add(Avx2.Multiply(a1, b1), r11));
                }
            }
            Intrinsics.Vector256<double> sum0 = Avx2.HorizontalAdd(r00,r01);
            Intrinsics.Vector256<double> sum1 = Avx2.HorizontalAdd(r10,r11);
            Sse2.Store(dst,            Sse2.Add(Avx2.ExtractVector128(sum0,0), Avx2.ExtractVector128(sum0,1)));
            Sse2.Store(dst+dst_stride, Sse2.Add(Avx2.ExtractVector128(sum1,0), Avx2.ExtractVector128(sum1,1)));
        }
#endif // no-avx2
#endif // no-sse2
    }
    
    /*************************************************************************
    Y := alpha*X + beta*Y

    Requires AVX2, does NOT check its presense.

      -- ALGLIB routine --
         19.07.2021
         Bochkanov Sergey
    *************************************************************************/
    private static unsafe void ablasf_daxpby_avx2(
        int    n,
        double alpha,
        double *src,
        double beta,
        double *dst)
    {
        if( beta==1.0 )
        {
            /*
             * The most optimized case: DST := alpha*SRC + DST
             *
             * First, we process leading elements with generic C code until DST is aligned.
             * Then, we process central part, assuming that DST is properly aligned.
             * Finally, we process tail.
             */
            int i, n4;
            Intrinsics.Vector256<double> avx_alpha = Intrinsics.Vector256.Create(alpha);
            while( n>0 && ((((ulong)dst)&31)!=0) )
            {
                *dst += alpha*(*src);
                n--;
                dst++;
                src++;
            }
            n4=(n>>2)<<2;
            for(i=0; i<n4; i+=4)
                Avx2.StoreAligned(dst+i, Avx2.Add(Avx2.Multiply(avx_alpha, Avx2.LoadVector256(src+i)), Avx2.LoadAlignedVector256(dst+i)));
            for(i=n4; i<n; i++)
                dst[i] = alpha*src[i]+dst[i];
        }
        else if( beta!=0.0 )
        {
            /*
             * Well optimized: DST := alpha*SRC + beta*DST
             */
            int i, n4;
            Intrinsics.Vector256<double> avx_alpha = Intrinsics.Vector256.Create(alpha);
            Intrinsics.Vector256<double> avx_beta  = Intrinsics.Vector256.Create(beta);
            while( n>0 && ((((ulong)dst)&31)!=0) )
            {
                *dst = alpha*(*src) + beta*(*dst);
                n--;
                dst++;
                src++;
            }
            n4=(n>>2)<<2;
            for(i=0; i<n4; i+=4)
                Avx2.StoreAligned(dst+i, Avx2.Add(Avx2.Multiply(avx_alpha, Avx2.LoadVector256(src+i)), Avx2.Multiply(avx_beta,Avx2.LoadAlignedVector256(dst+i))));
            for(i=n4; i<n; i++)
                dst[i] = alpha*src[i]+beta*dst[i];
        }
        else
        {
            /*
             * Easy case: DST := alpha*SRC
             */
            int i;
            for(i=0; i<n; i++)
                dst[i] = alpha*src[i];
        }
    }
#endif

        private static bool rgemm32basecase(int m,
            int n,
            int k,
            double alpha,
            double[,] _a,
            int ia,
            int ja,
            int optypea,
            double[,] _b,
            int ib,
            int jb,
            int optypeb,
            double beta,
            double[,] _c,
            int ic,
            int jc,
            xparams _params)
        {
#if !ALGLIB_USE_SIMD
            return false;
#else
        //
        // Quick exit
        //
        int block_size = 32;
        if( m<=_ABLASF_KERNEL_SIZE3 || n<=_ABLASF_KERNEL_SIZE3 || k<=_ABLASF_KERNEL_SIZE3 )
            return false;
        if( m>block_size || n>block_size || k>block_size || m==0 || n==0 || !Avx2.IsSupported )
            return false;
        
        //
        // Pin arrays and multiply using SIMD
        //
        int micro_size = 2;
        int alignment_doubles = 4;
        ulong alignment_bytes = (ulong)(alignment_doubles*sizeof(double));
        unsafe
        {
            fixed(double *c = &_c[ic,jc])
            {
                int out0, out1;
                int stride_c = _c.GetLength(1);
                
                /*
                 * Do we have alpha*A*B ?
                 */
                if( alpha!=0 && k>0 )
                {
                    fixed(double* a=&_a[ia,ja], b=&_b[ib,jb])
                    {
                        /*
                         * Prepare structures
                         */
                        int base0, base1, offs0;
                        int stride_a = _a.GetLength(1);
                        int stride_b = _b.GetLength(1);
                        double*      _blka = stackalloc double[block_size*micro_size+alignment_doubles];
                        double* _blkb_long = stackalloc double[block_size*block_size+alignment_doubles];
                        double*      _blkc = stackalloc double[micro_size*block_size+alignment_doubles];
                        double* blka          = (double*)(((((ulong)_blka)+alignment_bytes-1)/alignment_bytes)*alignment_bytes);
                        double* storageb_long = (double*)(((((ulong)_blkb_long)+alignment_bytes-1)/alignment_bytes)*alignment_bytes);
                        double* blkc          = (double*)(((((ulong)_blkc)+alignment_bytes-1)/alignment_bytes)*alignment_bytes);
                        
                        /*
                         * Pack transform(B) into precomputed block form
                         */
                        for(base1=0; base1<n; base1+=micro_size)
                        {
                            int lim1 = n-base1<micro_size ? n-base1 : micro_size;
                            double *curb = storageb_long+base1*block_size;
                            ablasf_packblkh_avx2(
                                b + (optypeb==0 ? base1 : base1*stride_b), stride_b, optypeb==0 ? 1 : 0, k, lim1,
                                curb, block_size, micro_size);
                        }
                        
                        /*
                         * Output
                         */
                        for(base0=0; base0<m; base0+=micro_size)
                        {
                            /*
                             * Load block row of transform(A)
                             */
                            int lim0    = m-base0<micro_size ? m-base0 : micro_size;
                            int round_k = ablasf_packblkh_avx2(
                                a + (optypea==0 ? base0*stride_a : base0), stride_a, optypea, k, lim0,
                                blka, block_size, micro_size);
                                
                            /*
                             * Compute block(A)'*entire(B)
                             */
                            for(base1=0; base1<n; base1+=micro_size)
                                ablasf_dotblkh_avx2_fma(blka, storageb_long+base1*block_size, round_k, block_size, micro_size, blkc+base1, block_size);

                            /*
                             * Output block row of block(A)'*entire(B)
                             */
                            for(offs0=0; offs0<lim0; offs0++)
                                ablasf_daxpby_avx2(n, alpha, blkc+offs0*block_size, beta, c+(base0+offs0)*stride_c);
                        }
                    }
                }
                else
                {
                    /*
                     * No A*B, just beta*C (degenerate case, not optimized)
                     */
                    if( beta==0 )
                    {
                        for(out0=0; out0<m; out0++)
                            for(out1=0; out1<n; out1++)
                                c[out0*stride_c+out1] = 0.0;
                    }
                    else if( beta!=1 )
                    {
                        for(out0=0; out0<m; out0++)
                            for(out1=0; out1<n; out1++)
                                c[out0*stride_c+out1] *= beta;
                    }
                }
            }
        }
        return true;
#endif
        }

}
