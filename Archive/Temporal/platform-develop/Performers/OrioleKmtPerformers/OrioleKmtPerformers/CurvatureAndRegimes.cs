/*using Apeiron.Mathematics;

namespace Apeiron.Platform.Performers.OrioleKmtPerformers;

/// <summary>
/// Предоставляет средства добавления каналов кривизны и режимов движения.
/// </summary>
public class CurvatureAndRegimes
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public CurvatureAndRegimes()
    {
    }

    /// <summary>
    /// Добавляет в кадр каналы кривизны и режимов движения.
    /// </summary>
    /// <param name="frame">
    /// Кадр.
    /// </param>
    public static void AddCurvatureAndRegimesKMT (Frame frame)
    {
        int length = frame.Channels["Lon_GPS"].Length;
        Channel H = new Channel("H", "m", 1200, 600, length);
        for (int i = 0; i < length; i++)
        {
            H[i] = 180;
        }
        frame.Channels.Add(H);
        
        AddCurvaturePriznak(frame, "Lat_GPS", "Lon_GPS", "H", 10, 1200, 1 / 3000, 1.0);
        AddRegimes(frame, "V_GPS", 10, 1200, 2, 0.2, 0.0, 50.0, -50.0, 30.0);

        frame.Channels.Remove(H);
    }

    /// <summary>
    /// Добавляет процессы кривизны и признака в кадр регистрации.
    /// </summary>
    /// <param name="cadr">
    /// Имя кадра регистрации (полный путь к кадру).
    /// </param>
    /// <param name="latName">
    /// Имя канала со значениями географической широты, град.
    /// </param>
    /// <param name="lonName">
    /// Имя канала со значениями географической долготы, град.
    /// </param>
    /// <param name="heightName">
    /// Имя канала со значениями высоты над уровнем моря, м.
    /// </param>
    /// <param name="nIt">
    /// Число итераций для сглаживания GPS-данных.
    /// </param>
    /// <param name="priznakSampleRate">
    /// Частота дискретизации для процесса "Признак", Гц.
    /// </param>
    /// <param name="tolerance2LineCurvature">
    /// Пограничное значение процесса "Признак", соответствующее переходу из прямой в кривую.
    /// </param>
    /// <param name="notNullValue">
    /// Значение процесса "Признак", соответствующее движению в кривой.
    /// </param>
    public static void AddCurvaturePriznak(Frame cadr, string latName, string lonName, string heightName, int nIt, double priznakSampleRate, double tolerance2LineCurvature, double notNullValue)
    {

        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] lat = cadr.Channels[latName].Vector;
        double[] lon = cadr.Channels[lonName].Vector;
        double[] height;    // = new double[length];
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(176.0, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector;
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, lat, lon, height, nIt);

        Channel curvatureChannel = new(curvature)
        {
            Sampling = 1.0,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.

        double[] priznak1 = new double[length];
        for (int i = 0; i < length; i++)
        {
            if (curvature[i] > tolerance2LineCurvature)
            {
                priznak1[i] = notNullValue;
            }
            else
            {
                priznak1[i] = 0.0;
            }
        }

        double[] priznak;   // = new double[length * (int)Math.Round(priznakSampleRate)];
        if (priznakSampleRate > 1.0)
        {
            priznak = Signals.ResamplingFrom1toHigher(priznak1, priznakSampleRate);
        }
        else
        {
            priznak = priznak1;
        }


        Channel priznakChannel = new(priznak)
        {
            Sampling = priznakSampleRate,
            Unit = string.Empty,
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

    }

    /// <summary>
    /// Добавляет процесс "Режимы" в кадр регистрации.
    /// </summary>
    /// <param name="cadr">
    /// Кадр.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="nIt">
    /// Число итераций для сглаживания GPS-данных.
    /// </param>
    /// <param name="regSampleRate">
    /// Частота дискретизации для процесса "Режимы", Гц.
    /// </param>
    /// <param name="speedTol">
    /// Значение скорости, км/ч, до которого режимом движения считается стоянка.
    /// </param>
    /// <param name="accTol">
    /// Значение ускорения, (м/с)/с, до которого режимом движения считается выбег.
    /// </param>
    /// <param name="stopValue">
    /// Значение процесса "Режимы" для стоянки.
    /// </param>
    /// <param name="pullValue">
    /// Значение процесса "Режимы" для тяги.
    /// </param>
    /// <param name="brakingValue">
    /// Значение процесса "Режимы" для торможения.
    /// </param>
    /// <param name="runValue">
    /// Значение процесса "Режимы" для выбега.
    /// </param>
    public static void AddRegimes(Frame cadr, string speedName, int nIt, double regSampleRate, double speedTol, double accTol, double stopValue, double pullValue, double brakingValue, double runValue)
    {

        speedTol /= 3.6; // Перевели в [м/с].

        // массив скоростей [м/с]:
        double[] v = Arrays.Scaling(cadr.Channels[speedName].Vector, 1.0 / 3.6);
        double[] vs = Calculus.SmoothingByIntDiff(v, nIt); // массив сглаженных скоростей
        double[] w = Calculus.Diff1Accuracy2(vs); // массив ускорений

        int nSites = v.Length; // Число отсчётов.

        double[] regime1 = new double[nSites];

        double currentV;
        double currentW;
        for (int i = 0; i < nSites; i++)
        {
            currentV = v[i];

            if (currentV <= speedTol)
            {
                regime1[i] = stopValue; // Стоянка.
            }
            else
            {
                currentW = w[i];

                if (Math.Abs(currentW) <= accTol)
                {
                    regime1[i] = runValue; // Выбег.
                }

                if (currentW > accTol)
                {
                    regime1[i] = pullValue; // Тяга.
                }

                if (currentW < -accTol)
                {
                    regime1[i] = brakingValue; // Торможение.
                }
            }
        }

        double[] regime;// = new double[nSites * (int)Math.Round(regSampleRate)];
        if (regSampleRate > 1.0)
        {
            regime = Signals.ResamplingFrom1toHigher(regime1, regSampleRate);
        }
        else
        {
            regime = regime1;
        }

        Channel regimeChannel = new(regime);
        regimeChannel.Sampling = regSampleRate;
        regimeChannel.Unit = String.Empty;
        regimeChannel.Name = "Regimes";

        cadr.Channels.Add(regimeChannel); // Добавили канал с режимами.

    }

    /// <summary>
    /// Вычисление кривизны по GPS-координатам.
    /// </summary>
    /// <param name="sampleRate">
    /// Частота опроса, Гц.
    /// </param>
    /// <param name="lat">
    /// Массив значений широты, град.
    /// </param>
    /// <param name="lon">
    /// Массив значений долготы, град.
    /// </param>
    /// <param name="height">
    /// массив значений высоты, м.
    /// </param>
    /// <param name="nIt">
    /// Число итераций для сглаживания данных (рекомендуемые значения: 40...50).
    /// </param>
    /// <returns>
    /// Массив значений кривизны.
    /// </returns>
    public static double[] CurvatureGPS(double sampleRate, double[] lat, double[] lon, double[] height, int nIt)
    {
        // Передискретизация к частоте опроса 1 Гц:
        if (sampleRate > 1)
        {
            lat = Signals.ResamplingLowering(lat, (int)sampleRate);
            lon = Signals.ResamplingLowering(lon, (int)sampleRate);
            height = Signals.ResamplingLowering(height, (int)sampleRate);
        }

        int n = lat.Length;

        // Сглаживание GPS-данных:
        lat = SmoothingByIntDiff(lat, nIt);
        lon = SmoothingByIntDiff(lon, nIt);
        height = SmoothingByIntDiff(height, nIt);

        // Переводим в USR-координаты:
        double[][] xyzData = GeoToUSR(lat, lon, height);
        double[] x = xyzData[0]; double[] y = xyzData[1]; double[] z = xyzData[2];

        // Массивы скоростей изменения координат:
        double[] vx = Diff1Accuracy2(x); double[] vy = Diff1Accuracy2(y); double[] vz = Diff1Accuracy2(z);

        double[] v = new double[n]; // Массив абсолютных значений скоростей.
        double[] crvtr = new double[n]; // Возвращаемый массив.

        // Массив абсолютных значений скоростей:
        for (int i = 0; i < n; i++)
        {
            v[i] = Math.Sqrt(Math.Pow(vx[i], 2) + Math.Pow(vy[i], 2) + Math.Pow(vz[i], 2));
        }

        double[] s = IntegralCumTrap(v); // Массив значений натурального параметра.

        // Массивы вторых производных по натуральному параметру:
        double[] wx = Diff2Accuracy2(s, x); double[] wy = Diff2Accuracy2(s, y); double[] wz = Diff2Accuracy2(s, z);

        // Массив значений кривизны:
        for (int i = 0; i < n; i++)
        {
            crvtr[i] = Math.Sqrt(Math.Pow(wx[i], 2) + Math.Pow(wy[i], 2) + Math.Pow(wz[i], 2));
        }

        return crvtr;
    }

    /// <summary>
    /// Вычисление 1-ой производной по формуле 2-ого порядка точности для равномерной сетки t[0],...,t[n-1] значений аргумента. Равномерность означает, что шаг сетки dt = t[k+1] - t[k] постоянен (не зависит от k).
    /// </summary>
    /// <param name="y">
    /// Массив значений функции.
    /// </param>
    /// <returns>
    /// Массив значений производной, умноженных на шаг сетки dt.
    /// </returns>
    public static double[] Diff1Accuracy2(double[] y)
    {
        // Вержбицкий В.М. Основы численных методов. М - "ВШ", 2002, стр. 516, 519.


        int n = y.Length;

        double[] dy = new double[n];

        if (n == 0)
        {
            return dy;
        }

        if (n == 1)
        {
            dy[0] = 0.0;
            return dy;
        }

        if (n == 2)
        {
            dy[0] = y[1] - y[0];
            dy[1] = dy[0];
            return dy;
        }

        dy[0] = 0.5 * (4.0 * y[1] - 3.0 * y[0] - y[2]);
        for (int k = 1; k < (n - 1); k++)
        {
            dy[k] = 0.5 * (y[k + 1] - y[k - 1]);
        }
        dy[n - 1] = 0.5 * (3.0 * y[n - 1] - 4.0 * y[n - 2] + y[n - 3]);


        return dy;
    }

    /// <summary>
    /// Вычисление 1-ой производной по формуле 2-ого порядка точности: y'[k]=(y[k+1]-y[k-1])/(t[k+1]-t[k-1])+O(dt^2).
    /// </summary>
    /// <param name="t">
    /// Массив значений аргумента.
    /// </param>
    /// <param name="y">
    /// Массив значений функции.
    /// </param>
    /// <returns>
    /// Массив значений производной.
    /// </returns>
    public static double[] Diff1Accuracy2(double[] t, double[] y)
    {
        // Вержбицкий В.М. Основы численных методов. М - "ВШ", 2002, стр. 516, 519.


        int n0 = t.Length; int n = y.Length;

        if (n0 != n)
        {
            throw new Exception("Не совпадают длины входных массивов! Дифференцирование не может быть выполнено.");
        }

        double[] dy = new double[n];

        if (n == 0)
        {
            return dy;
        }

        if (n == 1)
        {
            dy[0] = 0.0;
            return dy;
        }

        if (n == 2)
        {
            dy[0] = (y[1] - y[0]) / (t[1] - t[0]);
            dy[1] = (y[1] - y[0]) / (t[1] - t[0]);
            return dy;
        }

        dy[0] = (4.0 * y[1] - 3.0 * y[0] - y[2]) / (t[2] - t[0]);
        for (int k = 1; k < (n - 1); k++)
        {
            dy[k] = (y[k + 1] - y[k - 1]) / (t[k + 1] - t[k - 1]);
        }
        dy[n - 1] = (3.0 * y[n - 1] - 4.0 * y[n - 2] + y[n - 3]) / (t[n - 1] - t[n - 3]);


        return dy;
    }

    /// <summary>
    /// Передискретизация на более низкую частоту выборки.
    /// </summary>
    /// <param name="array">
    /// Массив числовых данных.
    /// </param>
    /// <param name="nSites">
    /// Число точек, "склеивающихся" в одну.
    /// </param>
    /// <returns>
    /// Передискретизированный массив.
    /// </returns>
    public static double[] ResamplingLowering(double[] array, int nSites)
    {
        int length = array.Length;

        int n = (int)Math.Floor(((double)length) / ((double)nSites)); // Примерное число кусков по nSites отсчётов.
        int newLength;

        if (n * nSites == length)
        {
            newLength = n;
        }
        else
        {
            newLength = n + 1;
        }

        double[] newArray = new double[newLength]; // Возвращаемый массив.

        for (int i = 0; i < n; i++)
        {
            int[] indexes = Arrays.IndexesArray(i * nSites, 1, (i + 1) * nSites - 1);

            newArray[i] = Arrays.SubArray(array, indexes).Average();
        }

        if (newLength > n)
        {
            int[] indexes = Arrays.IndexesArray(n * nSites, 1, length - 1);
            newArray[n] = Arrays.SubArray(array, indexes).Average();
        }

        return newArray;
    }

    /// <summary>
    /// Передискретизация с 1 герца на более высокую частоту выборки.
    /// </summary>
    /// <param name="array">
    /// Массив числовых данных.
    /// </param>
    /// <param name="sampleRate">
    /// Новая частота дискретизации, Гц.
    /// </param>
    /// <returns>
    /// Передискретизированный массив.
    /// </returns>
    public static double[] ResamplingFrom1toHigher(double[] array, double sampleRate)
    {
        int n = (int)Math.Round(sampleRate);
        int length = array.Length;

        double[] newArray = new double[n * length];

        double value0 = array[0];
        double value1;

        for (int j = 0; j < n; j++)
        {
            newArray[j] = value0;
        }

        for (int i = 1; i < length; i++)
        {
            value0 = array[i - 1];
            value1 = array[i];

            int j0 = i * n;
            int j1 = j0 + n;
            double c = (value1 - value0) / (n - 1);

            for (int j = j0; j < j1; j++)
            {
                //newArray[j] = array[i];
                newArray[j] = value0 + c * (j - j0);
            }
        }

        return newArray;
    }


    /// <summary>
    /// Масштабирование вещественного массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="scale">
    /// Масштабный вещественный множитель.
    /// </param>
    /// <returns>
    /// Отмасштабированный массив.
    /// </returns>
    public static double[] Scaling(double[] array, double scale)
    {
        int n = array.Length;
        double[] scaledArray = new double[n];

        for (int i = 0; i < n; i++)
        {
            scaledArray[i] = scale * array[i];
        }

        return scaledArray;
    }
    /// <summary>
    /// Сглаживание интегрированием-дифференцированием.
    /// </summary>
    /// <param name="y">
    /// Массив значений функции (процесса), подлежащий сглаживанию.
    /// </param>
    /// <param name="nit">
    /// Число итераций.
    /// </param>
    /// <returns>
    /// Массив сглаженных значений процесса.
    /// </returns>
    public static double[] SmoothingByIntDiff(double[] y, int nit)
    {
        Check.IsNotNull(y, nameof(y));
        Check.IsNotNull(nit, nameof(nit));

        if (nit == 0)
        {
            return y;
        }

        double[] Y;

        for (int i = 0; i < nit; i++)
        {
            Y = Calculus.IntegralCumTrap(y);
            y = Calculus.Diff1Accuracy2(Y);
        }

        return y;
    }

    /// <summary>
    /// Вычисление 2-ой производной по формуле 2-ого порядка точности: y''[k]=(y[k+1]-2y[k]+y[k-1])/(t[k+1]-t[k])^2+O(dt^2).
    /// </summary>
    /// <param name="t">
    /// Массив значений аргумента.
    /// </param>
    /// <param name="y">
    /// Массив значений функции.
    /// </param>
    /// <returns>
    /// Массив значений второй производной.
    /// </returns>
    public static double[] Diff2Accuracy2(double[] t, double[] y)
    {
        // Вержбицкий В.М. Основы численных методов. М - "ВШ", 2002, стр. 516, 517.

        Check.IsNotNull(t, nameof(t));
        Check.IsNotNull(y, nameof(y));

        int n0 = t.Length; int n = y.Length;

        if (n0 != n)
        {
            throw new Exception("Не совпадают длины входных массивов! Дифференцирование не может быть выполнено.");
        }

        double[] d2y = new double[n];

        if (n == 0)
        {
            return d2y;
        }

        if (n == 1)
        {
            d2y[0] = 0.0;
            return d2y;
        }

        if (n == 2)
        {
            d2y[0] = 0;
            d2y[1] = 0;
            return d2y;
        }

        // Для вычисления в начальный момент времени:
        double[] tb = { t[0], t[1], t[2] };
        double[] yb = { y[0], y[1], y[2] };

        double[] vb = Diff1Accuracy2(tb, yb);
        double[] wb = Diff1Accuracy2(tb, vb);

        // Для вычисления в конечный момент времени:
        double[] te = { t[n - 3], t[n - 2], t[n - 1] };
        double[] ye = { y[n - 3], y[n - 2], y[n - 1] };

        double[] ve = Diff1Accuracy2(te, ye);
        double[] we = Diff1Accuracy2(te, ve);

        d2y[0] = wb[0];
        for (int k = 1; k < (n - 1); k++)
        {
            double dt2 = Math.Pow((t[k + 1] - t[k - 1]), 2);
            d2y[k] = 4.0 * (y[k + 1] - 2.0 * y[k] + y[k - 1]) / dt2;
        }
        d2y[n - 1] = we[2];

        return d2y;
    }



    /// <summary>
    /// Вычисление 2-ой производной по формуле 2-ого порядка точности для равномерной сетки t[0],...,t[n-1] значений аргумента. Равномерность означает, что шаг сетки dt = t[k+1] - t[k] постоянен (не зависит от k).
    /// </summary>
    /// <param name="y">
    /// Массив значений функции.
    /// </param>
    /// <returns>
    /// Массив значений второй производной, умноженных на квадрат dt^2 шага сетки dt.
    /// </returns>
    public static double[] Diff2Accuracy2(double[] y)
    {
        // Вержбицкий В.М. Основы численных методов. М - "ВШ", 2002, стр. 516, 517.

        Check.IsNotNull(y, nameof(y));

        int n = y.Length;

        double[] d2y = new double[n];

        if (n == 0)
        {
            return d2y;
        }

        if (n == 1)
        {
            d2y[0] = 0.0;
            return d2y;
        }

        if (n == 2)
        {
            d2y[0] = 0;
            d2y[1] = 0;
            return d2y;
        }

        // Для вычисления в начальный момент времени:            
        double[] yb = { y[0], y[1], y[2] };

        double[] vb = Diff1Accuracy2(yb);
        double[] wb = Diff1Accuracy2(vb);

        // Для вычисления в конечный момент времени:            
        double[] ye = { y[n - 3], y[n - 2], y[n - 1] };

        double[] ve = Diff1Accuracy2(ye);
        double[] we = Diff1Accuracy2(ve);

        d2y[0] = wb[0];
        for (int k = 1; k < (n - 1); k++)
        {
            d2y[k] = y[k + 1] - 2.0 * y[k] + y[k - 1];
        }
        d2y[n - 1] = we[2];

        return d2y;
    }





    /// <summary>
    /// Интеграл с переменным верхним пределом по методу трапеций для равномерной сетки t[1],...,t[n] значений аргумента. Равномерность означает, что шаг сетки dt = t[i+1] - t[i] постоянен (не зависит от i).
    /// </summary>
    /// <param name="y">
    /// Массив значений подынтегральной функции.
    /// </param>
    /// <returns>
    /// Массив значений интеграла, делённых на шаг интегрирования dt.
    /// </returns>
    public static double[] IntegralCumTrap(double[] y)
    {
        Check.IsNotNull(y, nameof(y));

        int n = y.Length;

        double s = 0.0;

        double[] cs = new double[n]; // Массив значений интеграла.
        cs[0] = 0.0;

        double y0 = y[0];
        double y1;
        for (int i = 1; i < n; i++)
        {
            y1 = y[i];
            s += 0.5 * (y1 + y0);
            cs[i] = s;
            y0 = y1;
        }

        return cs;
    }

    /// <summary>
    /// Одномерный массив одинаковых значений заданной длины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип.
    /// </typeparam>
    /// <param name="constValue">
    /// Значение всех элементов массива.
    /// </param>
    /// <param name="length">
    /// Длина массива.
    /// </param>
    /// <returns>
    /// Одномерный массив одинаковых значений constValue длины length.
    /// </returns>
    public static T[] ConstArray<T>(T constValue, int length)
    {
        T[] array = new T[length];

        Array.Fill(array, constValue);

        //for (int i = 0; i < length; i++)
        //{
        //    array[i] = constValue;
        //}

        return array;
    }

    /// <summary>
    /// Пересчёт географических координат в декартовы геоцентрической системы координат USR (Universal Space Rectangular).
    /// </summary>
    /// <param name="lat">
    /// Массив широт, град.
    /// </param>
    /// <param name="lon">
    /// Массив долгот, град.
    /// </param>
    /// <param name="height">
    /// Массив высот, м.
    /// </param>
    /// <returns>
    /// Массив из трёх массивов: x-координат, y-координат и z-координат. 
    /// </returns>
    public static double[][] GeoToUSR(double[] lat, double[] lon, double[] height)
    {
        int length = lat.Length;
        double[] x = new double[length]; // Массив x-координат.
        double[] y = new double[length]; // Массив y-координат.
        double[] z = new double[length]; // Массив z-координат.

        double[][] outputData = new double[3][]; // Возвращаемые массивы координат.

        double R = 6371000.0; // - средний радиус Земли, м.

        for (int i = 0; i < length; i++)
        {
            double theta = Math.PI * lat[i] / 180.0; // Текущая широта в радианах.
            double phi = Math.PI * lon[i] / 180.0; // Текущая долгота в радианах.
            double rho = R + height[i]; // Текущая радиальная координата в метрах.

            double rc = rho * Math.Cos(theta);

            x[i] = rc * Math.Cos(phi);
            y[i] = rc * Math.Sin(phi);
            z[i] = rho * Math.Sin(theta);
        }

        outputData[0] = x; outputData[1] = y; outputData[2] = z;

        return outputData;
    }


}

*/