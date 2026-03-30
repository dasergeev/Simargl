namespace Apeiron.Analysis.Polynomials;

/// <summary>
/// Представляет кубический сплайн.
/// </summary>
internal sealed class CubicSpline
{
    internal static int Spline(int n, int end1, int end2,
                double slope1, double slope2,
                double[] x, double[] y,
                double[] b, double[] c, double[] d,
                out int iflag)
    {
        int nm1, ib, i, ascend;
        double t;
        nm1 = n - 1;
        iflag = 0;

        if (n < 2)
        { /* no possible interpolation */
            iflag = 1;
            goto LeaveSpline;
        }

        ascend = 1;
        for (i = 1; i < n; ++i) if (x[i] <= x[i - 1]) ascend = 0;
        if (ascend == 0)
        {
            iflag = 2;
            goto LeaveSpline;
        }
        if (n >= 3)
        {
            d[0] = x[1] - x[0];
            c[1] = (y[1] - y[0]) / d[0];
            for (i = 1; i < nm1; ++i)
            {
                d[i] = x[i + 1] - x[i];
                b[i] = 2.0 * (d[i - 1] + d[i]);
                c[i + 1] = (y[i + 1] - y[i]) / d[i];
                c[i] = c[i + 1] - c[i];
            }

            //  Конечные условия по умолчанию
            b[0] = -d[0];
            b[nm1] = -d[n - 2];
            c[0] = 0.0;
            c[nm1] = 0.0;
            if (n != 3)
            {
                c[0] = c[2] / (x[3] - x[1]) - c[1] / (x[2] - x[0]);
                c[nm1] = c[n - 2] / (x[nm1] - x[n - 3]) - c[n - 3] / (x[n - 2] - x[n - 4]);
                c[0] = c[0] * d[0] * d[0] / (x[3] - x[0]);
                c[nm1] = -c[nm1] * d[n - 2] * d[n - 2] / (x[nm1] - x[n - 4]);
            }

            //  Альтернативные конечные условия - известные уклоны.
            if (end1 == 1)
            {
                b[0] = 2.0 * (x[1] - x[0]);
                c[0] = (y[1] - y[0]) / (x[1] - x[0]) - slope1;
            }
            if (end2 == 1)
            {
                b[nm1] = 2.0 * (x[nm1] - x[n - 2]);
                c[nm1] = slope2 - (y[nm1] - y[n - 2]) / (x[nm1] - x[n - 2]);
            }

            //  Прямое устранение.
            for (i = 1; i < n; ++i)
            {
                t = d[i - 1] / b[i - 1];
                b[i] = b[i] - t * d[i - 1];
                c[i] = c[i] - t * c[i - 1];
            }

            //  Обратная замена
            c[nm1] = c[nm1] / b[nm1];
            for (ib = 0; ib < nm1; ++ib)
            {
                i = n - ib - 2;
                c[i] = (c[i] - d[i] * c[i + 1]) / b[i];
            }
            b[nm1] = (y[nm1] - y[n - 2]) / d[n - 2] + d[n - 2] * (c[n - 2] + 2.0 * c[nm1]);
            for (i = 0; i < nm1; ++i)
            {
                b[i] = (y[i + 1] - y[i]) / d[i] - d[i] * (c[i + 1] + 2.0 * c[i]);
                d[i] = (c[i + 1] - c[i]) / d[i];
                c[i] = 3.0 * c[i];
            }
            c[nm1] = 3.0 * c[nm1];
            d[nm1] = d[n - 2];
        }
        else
        {
            b[0] = (y[1] - y[0]) / (x[1] - x[0]);
            c[0] = 0.0;
            d[0] = 0.0;
            b[1] = b[0];
            c[1] = 0.0;
            d[1] = 0.0;
        }
    LeaveSpline:
        return 0;
    }

    internal static double Seval(/*int ni,*/ double u,
                        int n, double[] x, double[] y,
                        double[] b, double[] c, double[] d,
                        ref int last)
    {
        int i, j, k;
        double w;
        i = last;
        if (i >= n - 1) i = 0;
        if (i < 0) i = 0;
        if ((x[i] > u) || (x[i + 1] < u))//??
        {
            i = 0;
            j = n;
            do
            {
                k = (i + j) / 2;
                if (u < x[k]) j = k;
                if (u >= x[k]) i = k;
            } while (j > i + 1);
        }
        last = i;
        w = u - x[i];
        w = y[i] + w * (b[i] + w * (c[i] + w * d[i]));
        return (w);
    }

    internal static void SPL(int n, double[] x, double[] y,
        int ni, double[] xi, double[] yi)
    {
        int last = 0;
        double[] b = new double[n];
        double[] c = new double[n];
        double[] d = new double[n];
        Spline(n, 0, 0, 0, 0, x, y, b, c, d, out int _);
        for (int i = 0; i < ni; i++)
            yi[i] = Seval(/*ni,*/ xi[i], n, x, y, b, c, d, ref last);
    }

}
