using static System.Math;

namespace Apeiron.Analysis;

/// <summary>
/// Предоставляет дополнительные математические методы.
/// </summary>
public static class SpecialMath
{
    private const double MACHEP = 1.11022302462515654042E-16;
    private const double MAXLOG = 7.09782712893383996732E2;
    private const double MINLOG = -7.451332191019412076235E2;
    private const double MAXGAM = 171.624376956302725;
    private const double SQTPI = 2.50662827463100050242E0;
    private const double SQRTH = 7.07106781186547524401E-1;
    private const double LOGPI = 1.14472988584940017414;

    /// <summary>
    /// Вычисляет десятичный логарифм.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="argument"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="argument"/> передано нулевое значение.
    /// </exception>
    public static double Log10(double argument)
    {
        //  Постоянная, определяющая обратную величину натурального логарифма 10.
        const double factor = 1.0 / 2.3025850929940456840179914546844;

        //  Проверка аргумента.
        IsPositive(argument, nameof(argument));

        //  Вычисление логарифма.
        return Log(argument) * factor;
    }

    /// <summary>
    /// Вычисляет гиперболический косинус.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Cosh(double argument)
    {
        //  Получение модуля аргумента.
        argument = Abs(argument);

        //  Вычисление экспоненты.
        argument = Exp(argument);

        //  Вычисление гиперболического косинуса.
        return 0.5 * (argument + 1.0 / argument);
    }

    /// <summary>
    /// Вычисляет гиперболический синус.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Sinh(double argument)
    {
        double a;
        if (argument == 0.0) return argument;
        a = argument;
        if (a < 0.0) a = Abs(argument);
        a = Exp(a);
        if (argument < 0.0) return -0.5 * (a - 1 / a);
        else return 0.5 * (a - 1 / a);
    }

    /// <summary>
    /// Вычисляет гиперболический тангенс.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Tanh(double argument)
    {
        double a;
        if (argument == 0.0) return argument;
        a = argument;
        if (a < 0.0) a = Abs(argument);
        a = Exp(2.0 * a);
        if (argument < 0.0) return -(1.0 - 2.0 / (a + 1.0));
        else return (1.0 - 2.0 / (a + 1.0));
    }

    /// <summary>
    /// Вычисляет ареакосинус.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="argument"/> передано значение,
    /// которое меньше 1.
    /// </exception>
    public static double Acosh(double argument)
    {
        //  Проверка аргумента.
        IsNotLess(argument, 1, nameof(argument));

        return Log(argument + Sqrt(argument * argument - 1));
    }

    /// <summary>
    /// Вычисляет ареасинус.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Asinh(double argument)
    {
        double x;
        int sign;
        if (argument == 0.0) return argument;
        if (argument < 0.0)
        {
            sign = -1;
            x = -argument;
        }
        else
        {
            sign = 1;
            x = argument;
        }
        return sign * Log(x + Sqrt(x * x + 1));
    }

    /// <summary>
    /// Вычисляет ареатангенс.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="argument"/> передано значение,
    /// которое меньше -1.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="argument"/> передано значение,
    /// которое превышает значение 1.
    /// </exception>
    public static double Atanh(double argument)
    {
        //  Проверка аргумента.
        IsNotLess(argument, -1, nameof(argument));
        IsNotLarger(argument, 1, nameof(argument));

        //  Вычисление функции.
        return 0.5 * Log((1.0 + argument) / (1.0 - argument));
    }

    /// <summary>
    /// Вычисляет функцию Бесселя порядка 0.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double J0(double argument)
    {
        double ax;

        if ((ax = Abs(argument)) < 8.0)
        {
            double y = argument * argument;
            double ans1 = 57568490574.0 + y * (-13362590354.0 + y * (651619640.7
                + y * (-11214424.18 + y * (77392.33017 + y * (-184.9052456)))));
            double ans2 = 57568490411.0 + y * (1029532985.0 + y * (9494680.718
                + y * (59272.64853 + y * (267.8532712 + y * 1.0))));

            return ans1 / ans2;

        }
        else
        {
            double z = 8.0 / ax;
            double y = z * z;
            double xx = ax - 0.785398164;
            double ans1 = 1.0 + y * (-0.1098628627e-2 + y * (0.2734510407e-4
                + y * (-0.2073370639e-5 + y * 0.2093887211e-6)));
            double ans2 = -0.1562499995e-1 + y * (0.1430488765e-3
                + y * (-0.6911147651e-5 + y * (0.7621095161e-6
                - y * 0.934935152e-7)));

            return Sqrt(0.636619772 / ax) *
                (Cos(xx) * ans1 - z * Sin(xx) * ans2);
        }
    }

    /// <summary>
    /// Вычисляет функцию Бесселя порядка 1.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double J1(double argument)
    {
        double ax;
        double y;
        double ans1, ans2;

        if ((ax = Abs(argument)) < 8.0)
        {
            y = argument * argument;
            ans1 = argument * (72362614232.0 + y * (-7895059235.0 + y * (242396853.1
                + y * (-2972611.439 + y * (15704.48260 + y * (-30.16036606))))));
            ans2 = 144725228442.0 + y * (2300535178.0 + y * (18583304.74
                + y * (99447.43394 + y * (376.9991397 + y * 1.0))));
            return ans1 / ans2;
        }
        else
        {
            double z = 8.0 / ax;
            double xx = ax - 2.356194491;
            y = z * z;

            ans1 = 1.0 + y * (0.183105e-2 + y * (-0.3516396496e-4
                + y * (0.2457520174e-5 + y * (-0.240337019e-6))));
            ans2 = 0.04687499995 + y * (-0.2002690873e-3
                + y * (0.8449199096e-5 + y * (-0.88228987e-6
                + y * 0.105787412e-6)));
            double ans = Sqrt(0.636619772 / ax) *
                (Cos(xx) * ans1 - z * Sin(xx) * ans2);
            if (argument < 0.0) ans = -ans;
            return ans;
        }
    }

    /// <summary>
    /// Вычисляет функцию Бесселя заданного порядка.
    /// </summary>
    /// <param name="n">
    /// Порядок функции Бесселя.
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Jn(int n, double argument)
    {
        int j, m;
        double ax, bj, bjm, bjp, sum, tox, ans;
        bool jsum;

        double ACC = 40.0;
        double BIGNO = 1.0e+10;
        double BIGNI = 1.0e-10;

        if (n == 0) return J0(argument);
        if (n == 1) return J1(argument);

        ax = Abs(argument);
        if (ax == 0.0) return 0.0;
        else if (ax > (double)n)
        {
            tox = 2.0 / ax;
            bjm = J0(ax);
            bj = J1(ax);
            for (j = 1; j < n; j++)
            {
                bjp = j * tox * bj - bjm;
                bjm = bj;
                bj = bjp;
            }
            ans = bj;
        }
        else
        {
            tox = 2.0 / ax;
            m = 2 * ((n + (int)Sqrt(ACC * n)) / 2);
            jsum = false;
            bjp = ans = sum = 0.0;
            bj = 1.0;
            for (j = m; j > 0; j--)
            {
                bjm = j * tox * bj - bjp;
                bjp = bj;
                bj = bjm;
                if (Abs(bj) > BIGNO)
                {
                    bj *= BIGNI;
                    bjp *= BIGNI;
                    ans *= BIGNI;
                    sum *= BIGNI;
                }
                if (jsum) sum += bj;
                jsum = !jsum;
                if (j == n) ans = bjp;
            }
            sum = 2.0 * sum - bj;
            ans /= sum;
        }
        return argument < 0.0 && n % 2 == 1 ? -ans : ans;
    }

    /// <summary>
    /// Вычисляет функцию Бесселя второго рода порядка 0.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Y0(double argument)
    {
        if (argument < 8.0)
        {
            double y = argument * argument;

            double ans1 = -2957821389.0 + y * (7062834065.0 + y * (-512359803.6
                + y * (10879881.29 + y * (-86327.92757 + y * 228.4622733))));
            double ans2 = 40076544269.0 + y * (745249964.8 + y * (7189466.438
                + y * (47447.26470 + y * (226.1030244 + y * 1.0))));

            return (ans1 / ans2) + 0.636619772 * J0(argument) * Log(argument);
        }
        else
        {
            double z = 8.0 / argument;
            double y = z * z;
            double xx = argument - 0.785398164;

            double ans1 = 1.0 + y * (-0.1098628627e-2 + y * (0.2734510407e-4
                + y * (-0.2073370639e-5 + y * 0.2093887211e-6)));
            double ans2 = -0.1562499995e-1 + y * (0.1430488765e-3
                + y * (-0.6911147651e-5 + y * (0.7621095161e-6
                + y * (-0.934945152e-7))));
            return Sqrt(0.636619772 / argument) *
                (Sin(xx) * ans1 + z * Cos(xx) * ans2);
        }
    }

    /// <summary>
    /// Вычисляет функцию Бесселя второго рода порядка 1.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Y1(double argument)
    {
        if (argument < 8.0)
        {
            double y = argument * argument;
            double ans1 = argument * (-0.4900604943e13 + y * (0.1275274390e13
                + y * (-0.5153438139e11 + y * (0.7349264551e9
                + y * (-0.4237922726e7 + y * 0.8511937935e4)))));
            double ans2 = 0.2499580570e14 + y * (0.4244419664e12
                + y * (0.3733650367e10 + y * (0.2245904002e8
                + y * (0.1020426050e6 + y * (0.3549632885e3 + y)))));
            return (ans1 / ans2) + 0.636619772 * (J1(argument) * Log(argument) - 1.0 / argument);
        }
        else
        {
            double z = 8.0 / argument;
            double y = z * z;
            double xx = argument - 2.356194491;
            double ans1 = 1.0 + y * (0.183105e-2 + y * (-0.3516396496e-4
                + y * (0.2457520174e-5 + y * (-0.240337019e-6))));
            double ans2 = 0.04687499995 + y * (-0.2002690873e-3
                + y * (0.8449199096e-5 + y * (-0.88228987e-6
                + y * 0.105787412e-6)));
            return Sqrt(0.636619772 / argument) *
                (Sin(xx) * ans1 + z * Cos(xx) * ans2);
        }
    }

    /// <summary>
    /// Вычисляет функцию Бесселя второго рода заданного порядка.
    /// </summary>
    /// <param name="n">
    /// Порядок функции.
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Yn(int n, double argument)
    {
        double by, bym, byp, tox;

        if (n == 0) return Y0(argument);
        if (n == 1) return Y1(argument);

        tox = 2.0 / argument;
        by = Y1(argument);
        bym = Y0(argument);
        for (int j = 1; j < n; j++)
        {
            byp = j * tox * by - bym;
            bym = by;
            by = byp;
        }
        return by;
    }

    /// <summary>
    /// Вычисляет факториал.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Factorial(double argument)
    {
        double d = Abs(argument);
        if (Floor(d) == d)
        {
            return Factorial((int)argument);
        }
        else
        {
            return Gamma(argument + 1.0);
        }
    }

    /// <summary>
    /// Вычисляет факториал.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static int Factorial(int argument)
    {
        int i = argument;
        int d = 1;
        if (argument < 0) i = Abs(argument);
        while (i > 1)
        {
            d *= i--;
        }
        if (argument < 0) return -d;
        else return d;
    }

    /// <summary>
    /// Вычисляет гамма-функцию.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Gamma(double argument)
    {
        double[] P = {
                         1.60119522476751861407E-4,
                         1.19135147006586384913E-3,
                         1.04213797561761569935E-2,
                         4.76367800457137231464E-2,
                         2.07448227648435975150E-1,
                         4.94214826801497100753E-1,
                         9.99999999999999996796E-1
                     };
        double[] Q = {
                         -2.31581873324120129819E-5,
                         5.39605580493303397842E-4,
                         -4.45641913851797240494E-3,
                         1.18139785222060435552E-2,
                         3.58236398605498653373E-2,
                         -2.34591795718243348568E-1,
                         7.14304917030273074085E-2,
                         1.00000000000000000320E0
                     };

        double p, z;

        double q = Abs(argument);

        if (q > 33.0)
        {
            if (argument < 0.0)
            {
                p = Floor(q);
                if (p == q) throw new ArithmeticException("gamma: overflow");
                //int i = (int)p;
                z = q - p;
                if (z > 0.5)
                {
                    p += 1.0;
                    z = q - p;
                }
                z = q * Sin(PI * z);
                if (z == 0.0) throw new ArithmeticException("gamma: overflow");
                z = Abs(z);
                z = PI / (z * GammaStirling(q));

                return -z;
            }
            else
            {
                return GammaStirling(argument);
            }
        }

        z = 1.0;
        while (argument >= 3.0)
        {
            argument -= 1.0;
            z *= argument;
        }

        while (argument < 0.0)
        {
            if (argument == 0.0)
            {
                throw new ArithmeticException("gamma: singular");
            }
            else if (argument > -1.0E-9)
            {
                return (z / ((1.0 + 0.5772156649015329 * argument) * argument));
            }
            z /= argument;
            argument += 1.0;
        }

        while (argument < 2.0)
        {
            if (argument == 0.0)
            {
                throw new ArithmeticException("gamma: singular");
            }
            else if (argument < 1.0E-9)
            {
                return (z / ((1.0 + 0.5772156649015329 * argument) * argument));
            }
            z /= argument;
            argument += 1.0;
        }

        if ((argument == 2.0) || (argument == 3.0)) return z;

        argument -= 2.0;
        p = Polevl(argument, P, 6);
        q = Polevl(argument, Q, 7);
        return z * p / q;

    }

    /// <summary>
    /// Вычисляет гамма-функцию по формуле Стирлинга.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    private static double GammaStirling(double argument)
    {
        double[] STIR = {
                            7.87311395793093628397E-4,
                            -2.29549961613378126380E-4,
                            -2.68132617805781232825E-3,
                            3.47222221605458667310E-3,
                            8.33333333333482257126E-2,
        };
        double MAXSTIR = 143.01608;

        double w = 1.0 / argument;
        double y = Exp(argument);

        w = 1.0 + w * Polevl(w, STIR, 4);

        if (argument > MAXSTIR)
        {
            /* Avoid overflow in Math.Pow() */
            double v = Pow(argument, 0.5 * argument - 0.25);
            y = v * (v / y);
        }
        else
        {
            y = Pow(argument, argument - 0.5) / y;
        }
        y = SQTPI * y * w;
        return y;
    }

    /// <summary>
    /// Вычисляет дополненную неполную гамма-функцию.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double IGammaC(double a, double x)
    {
        double big = 4.503599627370496e15;
        double biginv = 2.22044604925031308085e-16;
        double ans, ax, c, yc, r, t, y, z;
        double pk, pkm1, pkm2, qk, qkm1, qkm2;

        if (x <= 0 || a <= 0) return 1.0;

        if (x < 1.0 || x < a) return 1.0 - IGamma(a, x);

        ax = a * Log(x) - x - LnGamma(a);
        if (ax < -MAXLOG) return 0.0;

        ax = Exp(ax);

        /* continued fraction */
        y = 1.0 - a;
        z = x + y + 1.0;
        c = 0.0;
        pkm2 = 1.0;
        qkm2 = x;
        pkm1 = x + 1.0;
        qkm1 = z * x;
        ans = pkm1 / qkm1;

        do
        {
            c += 1.0;
            y += 1.0;
            z += 2.0;
            yc = y * c;
            pk = pkm1 * z - pkm2 * yc;
            qk = qkm1 * z - qkm2 * yc;
            if (qk != 0)
            {
                r = pk / qk;
                t = Abs((ans - r) / r);
                ans = r;
            }
            else
                t = 1.0;

            pkm2 = pkm1;
            pkm1 = pk;
            qkm2 = qkm1;
            qkm1 = qk;
            if (Abs(pk) > big)
            {
                pkm2 *= biginv;
                pkm1 *= biginv;
                qkm2 *= biginv;
                qkm1 *= biginv;
            }
        } while (t > MACHEP);

        return ans * ax;
    }

    /// <summary>
    /// Возвращает неполную гамма-функцию.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double IGamma(double a, double x)
    {
        double ans, ax, c, r;

        if (x <= 0 || a <= 0) return 0.0;

        if (x > 1.0 && x > a) return 1.0 - IGammaC(a, x);

        /* Compute  x**a * exp(-x) / gamma(a)  */
        ax = a * Log(x) - x - LnGamma(a);
        if (ax < -MAXLOG) return (0.0);

        ax = Exp(ax);

        /* power series */
        r = a;
        c = 1.0;
        ans = 1.0;

        do
        {
            r += 1.0;
            c *= x / r;
            ans += c;
        } while (c / ans > MACHEP);

        return (ans * ax / a);

    }

    /// <summary>
    /// Возвращает функцию хи-квадрат (хвост слева).
    /// </summary>
    /// <param name="df">
    /// степени свободы
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double ChiSquare(double df, double argument)
    {
        if (argument < 0.0 || df < 1.0) return 0.0;

        return IGamma(df / 2.0, argument / 2.0);

    }

    /// <summary>
    /// Returns the chi-square function (right hand tail).
    /// </summary>
    /// <param name="df">
    /// Степени свободы.
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double ChiSquareC(double df, double argument)
    {
        if (argument < 0.0 || df < 1.0) return 0.0;

        return IGammaC(df / 2.0, argument / 2.0);

    }

    /// <summary>
    /// Возвращает сумму первых k членов распределения Пуассона.
    /// </summary>
    /// <param name="k">
    /// Количество членов.
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Poisson(int k, double argument)
    {
        if (k < 0 || argument < 0) return 0.0;

        return IGammaC((double)(k + 1), argument);
    }

    /// <summary>
    /// Возвращает сумму членов начиная с k + 1 до бесконечности распределения Пуассона.
    /// </summary>
    /// <param name="k">
    /// Начальный член.
    /// </param>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double PoissonC(int k, double argument)
    {
        if (k < 0 || argument < 0) return 0.0;

        return IGamma((double)(k + 1), argument);
    }

    /// <summary>
    /// Вычисляет площадь под функцией плотности вероятности Гаусса,
    /// интегрированную от минус бесконечности до <paramref name="argument"/>.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Normal(double argument)
    {
        double x, y, z;

        x = argument * SQRTH;
        z = Abs(x);

        if (z < SQRTH) y = 0.5 + 0.5 * Erf(x);
        else
        {
            y = 0.5 * ErfC(z);
            if (x > 0) y = 1.0 - y;
        }

        return y;
    }

    /// <summary>
    /// Возвращает дополнительную функцию ошибки указанного числа.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double ErfC(double argument)
    {
        double x, y, z, p, q;

        double[] P = {
                         2.46196981473530512524E-10,
                         5.64189564831068821977E-1,
                         7.46321056442269912687E0,
                         4.86371970985681366614E1,
                         1.96520832956077098242E2,
                         5.26445194995477358631E2,
                         9.34528527171957607540E2,
                         1.02755188689515710272E3,
                         5.57535335369399327526E2
                     };
        double[] Q = {
						 1.32281951154744992508E1,
                         8.67072140885989742329E1,
                         3.54937778887819891062E2,
                         9.75708501743205489753E2,
                         1.82390916687909736289E3,
                         2.24633760818710981792E3,
                         1.65666309194161350182E3,
                         5.57535340817727675546E2
                     };

        double[] R = {
                         5.64189583547755073984E-1,
                         1.27536670759978104416E0,
                         5.01905042251180477414E0,
                         6.16021097993053585195E0,
                         7.40974269950448939160E0,
                         2.97886665372100240670E0
                     };
        double[] S = {
						 2.26052863220117276590E0,
                         9.39603524938001434673E0,
                         1.20489539808096656605E1,
                         1.70814450747565897222E1,
                         9.60896809063285878198E0,
                         3.36907645100081516050E0
                     };

        if (argument < 0.0) x = -argument;
        else x = argument;

        if (x < 1.0) return 1.0 - Erf(argument);

        z = -argument * argument;

        if (z < -MAXLOG)
        {
            if (argument < 0) return (2.0);
            else return (0.0);
        }

        z = Exp(z);

        if (x < 8.0)
        {
            p = Polevl(x, P, 8);
            q = P1evl(x, Q, 8);
        }
        else
        {
            p = Polevl(x, R, 5);
            q = P1evl(x, S, 6);
        }

        y = (z * p) / q;

        if (argument < 0) y = 2.0 - y;

        if (y == 0.0)
        {
            if (argument < 0) return 2.0;
            else return (0.0);
        }


        return y;
    }

    /// <summary>
    /// Вычисляет функцию ошибки.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double Erf(double argument)
    {
        double y, z;
        double[] T = {
                         9.60497373987051638749E0,
                         9.00260197203842689217E1,
                         2.23200534594684319226E3,
                         7.00332514112805075473E3,
                         5.55923013010394962768E4
                     };
        double[] U = {
						 3.35617141647503099647E1,
                         5.21357949780152679795E2,
                         4.59432382970980127987E3,
                         2.26290000613890934246E4,
                         4.92673942608635921086E4
                     };

        if (Abs(argument) > 1.0) return (1.0 - ErfC(argument));
        z = argument * argument;
        y = argument * Polevl(z, T, 4) / P1evl(z, U, 5);
        return y;
    }

    /// <summary>
    /// Вычисляет многочлен степени N.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <param name="coef"></param>
    /// <param name="N"></param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    private static double Polevl(double argument, double[] coef, int N)
    {
        double ans;

        ans = coef[0];

        for (int i = 1; i <= N; i++)
        {
            ans = ans * argument + coef[i];
        }

        return ans;
    }

    /// <summary>
    /// Вычисляет многочлен степени N с предположением, что коэффициент[N] = 1,0
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <param name="coef"></param>
    /// <param name="N"></param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    private static double P1evl(double argument, double[] coef, int N)
    {
        double ans;

        ans = argument + coef[0];

        for (int i = 1; i < N; i++)
        {
            ans = ans * argument + coef[i];
        }

        return ans;
    }

    /// <summary>
    /// Возвращает логарифм гамма-функции.
    /// </summary>
    /// <param name="argument">
    /// Аргумент функции.
    /// </param>
    /// <returns>
    /// Результат вычислений.
    /// </returns>
    public static double LnGamma(double argument)
    {
        double p, q, w, z;

        double[] A = {
                         8.11614167470508450300E-4,
                         -5.95061904284301438324E-4,
                         7.93650340457716943945E-4,
                         -2.77777777730099687205E-3,
                         8.33333333333331927722E-2
                     };
        double[] B = {
                         -1.37825152569120859100E3,
                         -3.88016315134637840924E4,
                         -3.31612992738871184744E5,
                         -1.16237097492762307383E6,
                         -1.72173700820839662146E6,
                         -8.53555664245765465627E5
                     };
        double[] C = {
						 -3.51815701436523470549E2,
                         -1.70642106651881159223E4,
                         -2.20528590553854454839E5,
                         -1.13933444367982507207E6,
                         -2.53252307177582951285E6,
                         -2.01889141433532773231E6
                     };

        if (argument < -34.0)
        {
            q = -argument;
            w = LnGamma(q);
            p = Floor(q);
            if (p == q) throw new ArithmeticException("lgam: Overflow");
            z = q - p;
            if (z > 0.5)
            {
                p += 1.0;
                z = p - q;
            }
            z = q * Sin(PI * z);
            if (z == 0.0) throw new
                              ArithmeticException("lgamma: Overflow");
            z = LOGPI - Log(z) - w;
            return z;
        }

        if (argument < 13.0)
        {
            z = 1.0;
            while (argument >= 3.0)
            {
                argument -= 1.0;
                z *= argument;
            }
            while (argument < 2.0)
            {
                if (argument == 0.0) throw new
                                  ArithmeticException("lgamma: Overflow");
                z /= argument;
                argument += 1.0;
            }
            if (z < 0.0) z = -z;
            if (argument == 2.0) return Log(z);
            argument -= 2.0;
            p = argument * Polevl(argument, B, 5) / P1evl(argument, C, 6);
            return (Log(z) + p);
        }

        if (argument > 2.556348e305) throw new
                                  ArithmeticException("lgamma: Overflow");

        q = (argument - 0.5) * Log(argument) - argument + 0.91893853320467274178;
        if (argument > 1.0e8) return (q);

        p = 1.0 / (argument * argument);
        if (argument >= 1000.0)
            q += ((7.9365079365079365079365e-4 * p
                - 2.7777777777777777777778e-3) * p
                + 0.0833333333333333333333) / argument;
        else
            q += Polevl(p, A, 4) / argument;
        return q;
    }

    /// <summary>
    /// Возвращает незавершенную бета-функцию, оцененную от нуля до xx.
    /// </summary>
    /// <param name="aa"></param>
    /// <param name="bb"></param>
    /// <param name="xx"></param>
    /// <returns></returns>
    public static double IBeta(double aa, double bb, double xx)
    {
        double a, b, t, x, xc, w, y;
        bool flag;

        if (aa <= 0.0 || bb <= 0.0) throw new
                                        ArithmeticException("ibeta: Domain error!");

        if ((xx <= 0.0) || (xx >= 1.0))
        {
            if (xx == 0.0) return 0.0;
            if (xx == 1.0) return 1.0;
            throw new ArithmeticException("ibeta: Domain error!");
        }

        flag = false;
        if ((bb * xx) <= 1.0 && xx <= 0.95)
        {
            t = Pseries(aa, bb, xx);
            return t;
        }

        w = 1.0 - xx;

        /* Reverse a and b if x is greater than the mean. */
        if (xx > (aa / (aa + bb)))
        {
            flag = true;
            a = bb;
            b = aa;
            xc = xx;
            x = w;
        }
        else
        {
            a = aa;
            b = bb;
            xc = w;
            x = xx;
        }

        if (flag && (b * x) <= 1.0 && x <= 0.95)
        {
            t = Pseries(a, b, x);
            if (t <= MACHEP) t = 1.0 - MACHEP;
            else t = 1.0 - t;
            return t;
        }

        /* Choose expansion for better convergence. */
        y = x * (a + b - 2.0) - (a - 1.0);
        if (y < 0.0)
            w = Incbcf(a, b, x);
        else
            w = Incbd(a, b, x) / xc;

        /* Multiply w by the factor
			   a      b   _             _     _
			  x  (1-x)   | (a+b) / ( a | (a) | (b) ) .   */

        y = a * Log(x);
        t = b * Log(xc);
        if ((a + b) < MAXGAM && Abs(y) < MAXLOG && Abs(t) < MAXLOG)
        {
            t = Pow(xc, b);
            t *= Pow(x, a);
            t /= a;
            t *= w;
            t *= Gamma(a + b) / (Gamma(a) * Gamma(b));
            if (flag)
            {
                if (t <= MACHEP) t = 1.0 - MACHEP;
                else t = 1.0 - t;
            }
            return t;
        }
        /* Resort to logarithms.  */
        y += t + LnGamma(a + b) - LnGamma(a) - LnGamma(b);
        y += Log(w / a);
        if (y < MINLOG)
            t = 0.0;
        else
            t = Exp(y);

        if (flag)
        {
            if (t <= MACHEP) t = 1.0 - MACHEP;
            else t = 1.0 - t;
        }
        return t;
    }

    /// <summary>
    /// Возвращает непрерывное разложение дроби #1 для неполного бета-интеграла.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double Incbcf(double a, double b, double x)
    {
        double xk, pk, pkm1, pkm2, qk, qkm1, qkm2;
        double k1, k2, k3, k4, k5, k6, k7, k8;
        double r, t, ans, thresh;
        int n;
        double big = 4.503599627370496e15;
        double biginv = 2.22044604925031308085e-16;

        k1 = a;
        k2 = a + b;
        k3 = a;
        k4 = a + 1.0;
        k5 = 1.0;
        k6 = b - 1.0;
        k7 = k4;
        k8 = a + 2.0;

        pkm2 = 0.0;
        qkm2 = 1.0;
        pkm1 = 1.0;
        qkm1 = 1.0;
        ans = 1.0;
        r = 1.0;
        n = 0;
        thresh = 3.0 * MACHEP;
        do
        {
            xk = -(x * k1 * k2) / (k3 * k4);
            pk = pkm1 + pkm2 * xk;
            qk = qkm1 + qkm2 * xk;
            pkm2 = pkm1;
            pkm1 = pk;
            qkm2 = qkm1;
            qkm1 = qk;

            xk = (x * k5 * k6) / (k7 * k8);
            pk = pkm1 + pkm2 * xk;
            qk = qkm1 + qkm2 * xk;
            pkm2 = pkm1;
            pkm1 = pk;
            qkm2 = qkm1;
            qkm1 = qk;

            if (qk != 0) r = pk / qk;
            if (r != 0)
            {
                t = Abs((ans - r) / r);
                ans = r;
            }
            else
                t = 1.0;

            if (t < thresh) return ans;

            k1 += 1.0;
            k2 += 1.0;
            k3 += 2.0;
            k4 += 2.0;
            k5 += 1.0;
            k6 -= 1.0;
            k7 += 2.0;
            k8 += 2.0;

            if ((Abs(qk) + Abs(pk)) > big)
            {
                pkm2 *= biginv;
                pkm1 *= biginv;
                qkm2 *= biginv;
                qkm1 *= biginv;
            }
            if ((Abs(qk) < biginv) || (Abs(pk) < biginv))
            {
                pkm2 *= big;
                pkm1 *= big;
                qkm2 *= big;
                qkm1 *= big;
            }
        } while (++n < 300);

        return ans;
    }

    /// <summary>
    /// Возвращает непрерывное разложение дроби #2 для неполного бета-интеграла.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double Incbd(double a, double b, double x)
    {
        double xk, pk, pkm1, pkm2, qk, qkm1, qkm2;
        double k1, k2, k3, k4, k5, k6, k7, k8;
        double r, t, ans, z, thresh;
        int n;
        double big = 4.503599627370496e15;
        double biginv = 2.22044604925031308085e-16;

        k1 = a;
        k2 = b - 1.0;
        k3 = a;
        k4 = a + 1.0;
        k5 = 1.0;
        k6 = a + b;
        k7 = a + 1.0;
        ;
        k8 = a + 2.0;

        pkm2 = 0.0;
        qkm2 = 1.0;
        pkm1 = 1.0;
        qkm1 = 1.0;
        z = x / (1.0 - x);
        ans = 1.0;
        r = 1.0;
        n = 0;
        thresh = 3.0 * MACHEP;
        do
        {
            xk = -(z * k1 * k2) / (k3 * k4);
            pk = pkm1 + pkm2 * xk;
            qk = qkm1 + qkm2 * xk;
            pkm2 = pkm1;
            pkm1 = pk;
            qkm2 = qkm1;
            qkm1 = qk;

            xk = (z * k5 * k6) / (k7 * k8);
            pk = pkm1 + pkm2 * xk;
            qk = qkm1 + qkm2 * xk;
            pkm2 = pkm1;
            pkm1 = pk;
            qkm2 = qkm1;
            qkm1 = qk;

            if (qk != 0) r = pk / qk;
            if (r != 0)
            {
                t = Abs((ans - r) / r);
                ans = r;
            }
            else
                t = 1.0;

            if (t < thresh) return ans;

            k1 += 1.0;
            k2 -= 1.0;
            k3 += 2.0;
            k4 += 2.0;
            k5 += 1.0;
            k6 += 1.0;
            k7 += 2.0;
            k8 += 2.0;

            if ((Abs(qk) + Abs(pk)) > big)
            {
                pkm2 *= biginv;
                pkm1 *= biginv;
                qkm2 *= biginv;
                qkm1 *= biginv;
            }
            if ((Abs(qk) < biginv) || (Abs(pk) < biginv))
            {
                pkm2 *= big;
                pkm1 *= big;
                qkm2 *= big;
                qkm1 *= big;
            }
        } while (++n < 300);

        return ans;
    }

    /// <summary>
    /// Возвращает степенной ряд для неполного бета-интеграла. Используйте, когда b * x мало, а x не слишком близко к 1.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double Pseries(double a, double b, double x)
    {
        double s, t, u, v, n, t1, z, ai;

        ai = 1.0 / a;
        u = (1.0 - b) * x;
        v = u / (a + 1.0);
        t1 = v;
        t = u;
        n = 2.0;
        s = 0.0;
        z = MACHEP * ai;
        while (Abs(v) > z)
        {
            u = (n - b) * x / n;
            t *= u;
            v = t / (a + n);
            s += v;
            n += 1.0;
        }
        s += t1;
        s += ai;

        u = a * Log(x);
        if ((a + b) < MAXGAM && Abs(u) < MAXLOG)
        {
            t = Gamma(a + b) / (Gamma(a) * Gamma(b));
            s = s * t * Pow(x, a);
        }
        else
        {
            t = LnGamma(a + b) - LnGamma(a) - LnGamma(b) + u + Log(s);
            if (t < MINLOG) s = 0.0;
            else s = Exp(t);
        }
        return s;
    }
}
