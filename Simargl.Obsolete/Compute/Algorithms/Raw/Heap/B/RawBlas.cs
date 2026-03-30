using System;

namespace Simargl.Algorithms.Raw;

/// <summary>
/// 
/// </summary>
public class RawBlas
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static double vectornorm2(double[] x,
        int i1,
        int i2,
        xparams _params)
    {
        double result = 0;
        int n = 0;
        int ix = 0;
        double absxi = 0;
        double scl = 0;
        double ssq = 0;

        n = i2 - i1 + 1;
        if (n < 1)
        {
            result = 0;
            return result;
        }
        if (n == 1)
        {
            result = Math.Abs(x[i1]);
            return result;
        }
        scl = 0;
        ssq = 1;
        for (ix = i1; ix <= i2; ix++)
        {
            if ((double)(x[ix]) != (double)(0))
            {
                absxi = Math.Abs(x[ix]);
                if ((double)(scl) < (double)(absxi))
                {
                    ssq = 1 + ssq * math.sqr(scl / absxi);
                    scl = absxi;
                }
                else
                {
                    ssq = ssq + math.sqr(absxi / scl);
                }
            }
        }
        result = scl * Math.Sqrt(ssq);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static int vectoridxabsmax(double[] x,
        int i1,
        int i2,
        xparams _params)
    {
        int result = 0;
        int i = 0;

        result = i1;
        for (i = i1 + 1; i <= i2; i++)
        {
            if ((double)(Math.Abs(x[i])) > (double)(Math.Abs(x[result])))
            {
                result = i;
            }
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="j"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static int columnidxabsmax(double[,] x,
        int i1,
        int i2,
        int j,
        xparams _params)
    {
        int result = 0;
        int i = 0;

        result = i1;
        for (i = i1 + 1; i <= i2; i++)
        {
            if ((double)(Math.Abs(x[i, j])) > (double)(Math.Abs(x[result, j])))
            {
                result = i;
            }
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="i"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static int rowidxabsmax(double[,] x,
        int j1,
        int j2,
        int i,
        xparams _params)
    {
        int result = 0;
        int j = 0;

        result = j1;
        for (j = j1 + 1; j <= j2; j++)
        {
            if ((double)(Math.Abs(x[i, j])) > (double)(Math.Abs(x[i, result])))
            {
                result = j;
            }
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="work"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static double upperhessenberg1norm(double[,] a,
        int i1,
        int i2,
        int j1,
        int j2,
        ref double[] work,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;

        ap.assert(i2 - i1 == j2 - j1, "UpperHessenberg1Norm: I2-I1<>J2-J1!");
        for (j = j1; j <= j2; j++)
        {
            work[j] = 0;
        }
        for (i = i1; i <= i2; i++)
        {
            for (j = Math.Max(j1, j1 + i - i1 - 1); j <= j2; j++)
            {
                work[j] = work[j] + Math.Abs(a[i, j]);
            }
        }
        result = 0;
        for (j = j1; j <= j2; j++)
        {
            result = Math.Max(result, work[j]);
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="is1"></param>
    /// <param name="is2"></param>
    /// <param name="js1"></param>
    /// <param name="js2"></param>
    /// <param name="b"></param>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="jd1"></param>
    /// <param name="jd2"></param>
    /// <param name="_params"></param>
    public static void copymatrix(double[,] a,
        int is1,
        int is2,
        int js1,
        int js2,
        ref double[,] b,
        int id1,
        int id2,
        int jd1,
        int jd2,
        xparams _params)
    {
        int isrc = 0;
        int idst = 0;
        int i_ = 0;
        int i1_ = 0;

        if (is1 > is2 || js1 > js2)
        {
            return;
        }
        ap.assert(is2 - is1 == id2 - id1, "CopyMatrix: different sizes!");
        ap.assert(js2 - js1 == jd2 - jd1, "CopyMatrix: different sizes!");
        for (isrc = is1; isrc <= is2; isrc++)
        {
            idst = isrc - is1 + id1;
            i1_ = (js1) - (jd1);
            for (i_ = jd1; i_ <= jd2; i_++)
            {
                b[idst, i_] = a[isrc, i_ + i1_];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="work"></param>
    /// <param name="_params"></param>
    public static void inplacetranspose(ref double[,] a,
        int i1,
        int i2,
        int j1,
        int j2,
        ref double[] work,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ips = 0;
        int jps = 0;
        int l = 0;
        int i_ = 0;
        int i1_ = 0;

        if (i1 > i2 || j1 > j2)
        {
            return;
        }
        ap.assert(i1 - i2 == j1 - j2, "InplaceTranspose error: incorrect array size!");
        for (i = i1; i <= i2 - 1; i++)
        {
            j = j1 + i - i1;
            ips = i + 1;
            jps = j1 + ips - i1;
            l = i2 - i;
            i1_ = (ips) - (1);
            for (i_ = 1; i_ <= l; i_++)
            {
                work[i_] = a[i_ + i1_, j];
            }
            i1_ = (jps) - (ips);
            for (i_ = ips; i_ <= i2; i_++)
            {
                a[i_, j] = a[i, i_ + i1_];
            }
            i1_ = (1) - (jps);
            for (i_ = jps; i_ <= j2; i_++)
            {
                a[i, i_] = work[i_ + i1_];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="is1"></param>
    /// <param name="is2"></param>
    /// <param name="js1"></param>
    /// <param name="js2"></param>
    /// <param name="b"></param>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="jd1"></param>
    /// <param name="jd2"></param>
    /// <param name="_params"></param>
    public static void copyandtranspose(double[,] a,
        int is1,
        int is2,
        int js1,
        int js2,
        ref double[,] b,
        int id1,
        int id2,
        int jd1,
        int jd2,
        xparams _params)
    {
        int isrc = 0;
        int jdst = 0;
        int i_ = 0;
        int i1_ = 0;

        if (is1 > is2 || js1 > js2)
        {
            return;
        }
        ap.assert(is2 - is1 == jd2 - jd1, "CopyAndTranspose: different sizes!");
        ap.assert(js2 - js1 == id2 - id1, "CopyAndTranspose: different sizes!");
        for (isrc = is1; isrc <= is2; isrc++)
        {
            jdst = isrc - is1 + jd1;
            i1_ = (js1) - (id1);
            for (i_ = id1; i_ <= id2; i_++)
            {
                b[i_, jdst] = a[isrc, i_ + i1_];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="trans"></param>
    /// <param name="x"></param>
    /// <param name="ix1"></param>
    /// <param name="ix2"></param>
    /// <param name="alpha"></param>
    /// <param name="y"></param>
    /// <param name="iy1"></param>
    /// <param name="iy2"></param>
    /// <param name="beta"></param>
    /// <param name="_params"></param>
    public static void matrixvectormultiply(double[,] a,
        int i1,
        int i2,
        int j1,
        int j2,
        bool trans,
        double[] x,
        int ix1,
        int ix2,
        double alpha,
        ref double[] y,
        int iy1,
        int iy2,
        double beta,
        xparams _params)
    {
        int i = 0;
        double v = 0;
        int i_ = 0;
        int i1_ = 0;

        if (!trans)
        {

            //
            // y := alpha*A*x + beta*y;
            //
            if (i1 > i2 || j1 > j2)
            {
                return;
            }
            ap.assert(j2 - j1 == ix2 - ix1, "MatrixVectorMultiply: A and X dont match!");
            ap.assert(i2 - i1 == iy2 - iy1, "MatrixVectorMultiply: A and Y dont match!");

            //
            // beta*y
            //
            if ((double)(beta) == (double)(0))
            {
                for (i = iy1; i <= iy2; i++)
                {
                    y[i] = 0;
                }
            }
            else
            {
                for (i_ = iy1; i_ <= iy2; i_++)
                {
                    y[i_] = beta * y[i_];
                }
            }

            //
            // alpha*A*x
            //
            for (i = i1; i <= i2; i++)
            {
                i1_ = (ix1) - (j1);
                v = 0.0;
                for (i_ = j1; i_ <= j2; i_++)
                {
                    v += a[i, i_] * x[i_ + i1_];
                }
                y[iy1 + i - i1] = y[iy1 + i - i1] + alpha * v;
            }
        }
        else
        {

            //
            // y := alpha*A'*x + beta*y;
            //
            if (i1 > i2 || j1 > j2)
            {
                return;
            }
            ap.assert(i2 - i1 == ix2 - ix1, "MatrixVectorMultiply: A and X dont match!");
            ap.assert(j2 - j1 == iy2 - iy1, "MatrixVectorMultiply: A and Y dont match!");

            //
            // beta*y
            //
            if ((double)(beta) == (double)(0))
            {
                for (i = iy1; i <= iy2; i++)
                {
                    y[i] = 0;
                }
            }
            else
            {
                for (i_ = iy1; i_ <= iy2; i_++)
                {
                    y[i_] = beta * y[i_];
                }
            }

            //
            // alpha*A'*x
            //
            for (i = i1; i <= i2; i++)
            {
                v = alpha * x[ix1 + i - i1];
                i1_ = (j1) - (iy1);
                for (i_ = iy1; i_ <= iy2; i_++)
                {
                    y[i_] = y[i_] + v * a[i, i_ + i1_];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    public static double pythag2(double x,
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="ai1"></param>
    /// <param name="ai2"></param>
    /// <param name="aj1"></param>
    /// <param name="aj2"></param>
    /// <param name="transa"></param>
    /// <param name="b"></param>
    /// <param name="bi1"></param>
    /// <param name="bi2"></param>
    /// <param name="bj1"></param>
    /// <param name="bj2"></param>
    /// <param name="transb"></param>
    /// <param name="alpha"></param>
    /// <param name="c"></param>
    /// <param name="ci1"></param>
    /// <param name="ci2"></param>
    /// <param name="cj1"></param>
    /// <param name="cj2"></param>
    /// <param name="beta"></param>
    /// <param name="work"></param>
    /// <param name="_params"></param>
    public static void matrixmatrixmultiply(double[,] a,
        int ai1,
        int ai2,
        int aj1,
        int aj2,
        bool transa,
        double[,] b,
        int bi1,
        int bi2,
        int bj1,
        int bj2,
        bool transb,
        double alpha,
        ref double[,] c,
        int ci1,
        int ci2,
        int cj1,
        int cj2,
        double beta,
        ref double[] work,
        xparams _params)
    {
        int arows = 0;
        int acols = 0;
        int brows = 0;
        int bcols = 0;
        int crows = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        int r = 0;
        double v = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Setup
        //
        if (!transa)
        {
            arows = ai2 - ai1 + 1;
            acols = aj2 - aj1 + 1;
        }
        else
        {
            arows = aj2 - aj1 + 1;
            acols = ai2 - ai1 + 1;
        }
        if (!transb)
        {
            brows = bi2 - bi1 + 1;
            bcols = bj2 - bj1 + 1;
        }
        else
        {
            brows = bj2 - bj1 + 1;
            bcols = bi2 - bi1 + 1;
        }
        ap.assert(acols == brows, "MatrixMatrixMultiply: incorrect matrix sizes!");
        if (((arows <= 0 || acols <= 0) || brows <= 0) || bcols <= 0)
        {
            return;
        }
        crows = arows;

        //
        // Test WORK
        //
        i = Math.Max(arows, acols);
        i = Math.Max(brows, i);
        i = Math.Max(i, bcols);
        work[1] = 0;
        work[i] = 0;

        //
        // Prepare C
        //
        if ((double)(beta) == (double)(0))
        {
            for (i = ci1; i <= ci2; i++)
            {
                for (j = cj1; j <= cj2; j++)
                {
                    c[i, j] = 0;
                }
            }
        }
        else
        {
            for (i = ci1; i <= ci2; i++)
            {
                for (i_ = cj1; i_ <= cj2; i_++)
                {
                    c[i, i_] = beta * c[i, i_];
                }
            }
        }

        //
        // A*B
        //
        if (!transa && !transb)
        {
            for (l = ai1; l <= ai2; l++)
            {
                for (r = bi1; r <= bi2; r++)
                {
                    v = alpha * a[l, aj1 + r - bi1];
                    k = ci1 + l - ai1;
                    i1_ = (bj1) - (cj1);
                    for (i_ = cj1; i_ <= cj2; i_++)
                    {
                        c[k, i_] = c[k, i_] + v * b[r, i_ + i1_];
                    }
                }
            }
            return;
        }

        //
        // A*B'
        //
        if (!transa && transb)
        {
            if (arows * acols < brows * bcols)
            {
                for (r = bi1; r <= bi2; r++)
                {
                    for (l = ai1; l <= ai2; l++)
                    {
                        i1_ = (bj1) - (aj1);
                        v = 0.0;
                        for (i_ = aj1; i_ <= aj2; i_++)
                        {
                            v += a[l, i_] * b[r, i_ + i1_];
                        }
                        c[ci1 + l - ai1, cj1 + r - bi1] = c[ci1 + l - ai1, cj1 + r - bi1] + alpha * v;
                    }
                }
                return;
            }
            else
            {
                for (l = ai1; l <= ai2; l++)
                {
                    for (r = bi1; r <= bi2; r++)
                    {
                        i1_ = (bj1) - (aj1);
                        v = 0.0;
                        for (i_ = aj1; i_ <= aj2; i_++)
                        {
                            v += a[l, i_] * b[r, i_ + i1_];
                        }
                        c[ci1 + l - ai1, cj1 + r - bi1] = c[ci1 + l - ai1, cj1 + r - bi1] + alpha * v;
                    }
                }
                return;
            }
        }

        //
        // A'*B
        //
        if (transa && !transb)
        {
            for (l = aj1; l <= aj2; l++)
            {
                for (r = bi1; r <= bi2; r++)
                {
                    v = alpha * a[ai1 + r - bi1, l];
                    k = ci1 + l - aj1;
                    i1_ = (bj1) - (cj1);
                    for (i_ = cj1; i_ <= cj2; i_++)
                    {
                        c[k, i_] = c[k, i_] + v * b[r, i_ + i1_];
                    }
                }
            }
            return;
        }

        //
        // A'*B'
        //
        if (transa && transb)
        {
            if (arows * acols < brows * bcols)
            {
                for (r = bi1; r <= bi2; r++)
                {
                    k = cj1 + r - bi1;
                    for (i = 1; i <= crows; i++)
                    {
                        work[i] = 0.0;
                    }
                    for (l = ai1; l <= ai2; l++)
                    {
                        v = alpha * b[r, bj1 + l - ai1];
                        i1_ = (aj1) - (1);
                        for (i_ = 1; i_ <= crows; i_++)
                        {
                            work[i_] = work[i_] + v * a[l, i_ + i1_];
                        }
                    }
                    i1_ = (1) - (ci1);
                    for (i_ = ci1; i_ <= ci2; i_++)
                    {
                        c[i_, k] = c[i_, k] + work[i_ + i1_];
                    }
                }
                return;
            }
            else
            {
                for (l = aj1; l <= aj2; l++)
                {
                    k = ai2 - ai1 + 1;
                    i1_ = (ai1) - (1);
                    for (i_ = 1; i_ <= k; i_++)
                    {
                        work[i_] = a[i_ + i1_, l];
                    }
                    for (r = bi1; r <= bi2; r++)
                    {
                        i1_ = (bj1) - (1);
                        v = 0.0;
                        for (i_ = 1; i_ <= k; i_++)
                        {
                            v += work[i_] * b[r, i_ + i1_];
                        }
                        c[ci1 + l - aj1, cj1 + r - bi1] = c[ci1 + l - aj1, cj1 + r - bi1] + alpha * v;
                    }
                }
                return;
            }
        }
    }
}
