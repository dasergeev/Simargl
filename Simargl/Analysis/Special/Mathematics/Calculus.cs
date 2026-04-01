using Simargl.Frames;
using System.IO;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, реализующие алгоритмы численного дифференцирования и интегрирования.
/// </summary>
public class Calculus
{
    /// <summary>
    /// Интеграл по методу трапеций.
    /// </summary>
    /// <param name="t">
    /// Массив значений аргумента.
    /// </param>
    /// <param name="y">
    /// Массив значений подынтегральной функции.
    /// </param>
    /// <returns>
    /// Значение интеграла.
    /// </returns>
    public static double IntegralTrap(double[] t, double[] y)
    {
        IsNotNull(t, nameof(t));
        IsNotNull(y, nameof(y));

        int n0 = t.Length; int n = y.Length;

        if (n0 != n)
        {
            throw new Exception("Не совпадают длины входных массивов! Интегрирование не может быть выполнено.");
        }

        double s = 0.0;
        double t0 = t[0]; double y0 = y[0];
        double t1; double y1;
        for (int i = 1; i < n; i++)
        {
            t1 = t[i];
            y1 = y[i];
            s += 0.5 * (t1 - t0) * (y1 + y0);
                
            t0 = t1;
            y0 = y1;
        }

        return s;
    }

    /// <summary>
    /// Интеграл по методу трапеций для равномерной сетки t[1],...,t[n] значений аргумента. Равномерность означает, что шаг сетки dt = t[i+1] - t[i] постоянен (не зависит от i).
    /// </summary>
    /// <param name="y">
    /// Массив значений подынтегральной функции.
    /// </param>
    /// <returns>
    /// Значение интеграла, делённое на шаг интегрирования dt.
    /// </returns>
    public static double IntegralTrap(double[] y)
    {
        IsNotNull(y, nameof(y));

        int n = y.Length;

        double s = 0.0;
            
        double y0 = y[0];
        double y1;
        for (int i = 1; i < n; i++)
        {
            y1 = y[i];
            s += 0.5 * (y1 + y0);             
            y0 = y1;
        }

        return s;
    }

    /// <summary>
    /// Интеграл с переменным верхним пределом по методу трапеций.
    /// </summary>
    /// <param name="t">
    /// Массив значений аргумента.
    /// </param>
    /// <param name="y">
    /// Массив значений подынтегральной функции.
    /// </param>
    /// <returns>
    /// Массив значений интеграла.
    /// </returns>
    public static double[] IntegralCumTrap(double[] t, double[] y)
    {
        IsNotNull(t, nameof(t));
        IsNotNull(y, nameof(y));

        int n0 = t.Length; int n = y.Length;

        if (n0 != n)
        {
            throw new Exception("Не совпадают длины входных массивов! Интегрирование не может быть выполнено.");
        }

        double s = 0.0;

        double[] cs = new double[n]; // Массив значений интеграла.
        cs[0] = 0.0;

        double t0 = t[0]; double y0 = y[0];
        double t1; double y1;
        for (int i = 1; i < n; i++)
        {
            t1 = t[i];
            y1 = y[i];
            s += 0.5 * (t1 - t0) * (y1 + y0);
            cs[i] = s;
            t0 = t1;
            y0 = y1;
        }

        return cs;
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
        IsNotNull(y, nameof(y));

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
    /// Массив конечных разностей.
    /// </summary>
    /// <param name="y">
    /// Исходный массив.
    /// </param>
    /// <returns>
    /// (y[1] - y[0], y[2] - y[1], ... , y[n - 1] - y[n - 2]).
    /// </returns>
    public static double[] Diff(double[] y)
    {
        IsNotNull(y, nameof(y));

        int n = y.Length;

        if (n <= 1)
        {
            return y;
        }

        int n1 = n - 1;

        double[] dy = new double[n1];

        for (int i = 0; i < n1; i++)
        {
            dy[i] = y[i + 1] - y[i];
        }

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

        IsNotNull(t, nameof(t));
        IsNotNull(y, nameof(y));

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

        IsNotNull(y, nameof(y));

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

        IsNotNull(t, nameof(t));
        IsNotNull(y, nameof(y));

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
            double dt2 = Math.Pow(t[k + 1] - t[k - 1], 2);
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

        IsNotNull(y, nameof(y));

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
    /// Сглаживание интегрированием-дифференцированием.
    /// </summary>
    /// <param name="t">
    /// Массив "моментов времени" (значений аргумента).
    /// </param>
    /// <param name="y">
    /// Массив значений функции (процесса), подлежащий сглаживанию.
    /// </param>
    /// <param name="nit">
    /// Число итераций.
    /// </param>
    /// <returns>
    /// Массив сглаженных значений процесса.
    /// </returns>
    public static double[] SmoothingByIntDiff(double[] t, double[] y, int nit)
    {
        IsNotNull(t, nameof(t));
        IsNotNull(y, nameof(y));
        IsNotNull(nit, nameof(nit));

        if (nit == 0)
        {
            return y;
        }

        double[] Y;

        for (int i = 0; i < nit; i++)
        {
            Y = IntegralCumTrap(t, y);
            y = Diff1Accuracy2(t, Y);
        }

        return y;
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
        IsNotNull(y, nameof(y));
        IsNotNull(nit, nameof(nit));

        if (nit == 0)
        {
            return y;
        }

        double[] Y;

        for (int i = 0; i < nit; i++)
        {
            Y = IntegralCumTrap(y);
            y = Diff1Accuracy2(Y);
        }

        return y;
    }

    /// <summary>
    /// Вычисление кривизны плоской кривой по углу (между вектором скорости и фиксированным направлением) и абсолютным значением скорости.
    /// </summary>
    /// <param name="course">
    /// Массив значений угла между вектором скорости и фиксированным направлением, рад.
    /// </param>
    /// <param name="speed">
    /// Массив абсолютных значений скорости, м/с.
    /// </param>
    /// <param name="t">
    /// Массив моментов времени, с.
    /// </param>
    /// <returns>
    /// Массив значений кривизны, 1/m.
    /// </returns>
    public static double[] CurvatureViaCourseSpeed(double[] course, double[] speed, double[] t)
    {
        IsNotNull(course, nameof(course));
        IsNotNull(speed, nameof(speed));
        IsNotNull(t, nameof(t));

        double[] s = IntegralCumTrap(t, speed); // Массив приращений пути.

        double[] curvature = Pointwise.Abs(Diff1Accuracy2(s, course));

        return curvature;               
    }

    /// <summary>
    /// Вычисление кривизны плоской кривой по курсу и скорости на основе данных из кадра регистрации.
    /// </summary>
    /// <param name="fileName">
    /// Полный путь к файлу.
    /// </param>
    /// <param name="courseName">
    /// Имя канала "Курс", значения в град.
    /// </param>
    /// <param name="speedName">
    /// Имя канала "Скорость", значения в км/ч.
    /// </param>
    /// <returns>
    /// Массив значений кривизны, 1/м. Частота выборки такая же как у каналов "Курс" и "Скорость".
    /// </returns>
    public static double[] CurvatureViaCourseSpeed(string fileName, string courseName, string speedName)
    {
        Frame cadr= new(fileName);

        // Массив значений канала "Курс" [градусы]:
        double[] courseInitial = cadr.Channels[courseName].Items;
        
        // Массив значений канала "Скорость" [км/ч]:
        double[] speedInitial = cadr.Channels[speedName].Items;

        // Частота опроса, Гц:
        double sampleRate = cadr.Channels[speedName].Sampling;

        int[] ind = Arrays.Thinning(courseInitial);
        int nSites = ind.Length;

        double[] t = new double[nSites];
        double[] speed = new double[nSites];
        double[] course = new double[nSites];

        for (int i = 0; i < nSites; i++)
        {
            t[i] = (ind[i] + 1) / sampleRate;
            speed[i] = speedInitial[ind[i]] / 3.6; // Скорость теперь в м/с.
            course[i] = Math.PI * courseInitial[ind[i]] / 180; // Курс в радианах.
        }

        double[] s = IntegralCumTrap(t, speed); // Массив приращений пути, м.

        double[] crvtr = Pointwise.Abs(Diff1Accuracy2(s, course)); // Кривизна.
        double[] crvtrs = SmoothingByIntDiff(t, crvtr, 5); // Сглаженная кривизна.

        // Приведение к исходным данным:

        // Исходная длина каналов:
        int nInitial = cadr.Channels[speedName].Length;

        // Массив моментов времени для исходных данных:
        double[] tInitial= new double[nInitial];

        for (int i = 0; i < nInitial; i++)
        {
            tInitial[i] = (i + 1) / sampleRate;
        }

        return Interpolations.Linear(t,crvtrs,tInitial);
    }

    /// <summary>
    /// Вычисление и добавление в кадр канала кривизны [1/м] (частота выборки канала "Кривизна" такая же как у каналов "Курс" и "Скорость") трека по курсу и скорости на основе данных из кадра регистрации.
    /// </summary>
    /// <param name="cadrFullName">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="pathDirSave">
    /// Путь к папке для перезаписанного кадра.
    /// </param>
    /// <param name="courseName">
    /// Имя канала "Курс", значения в град.
    /// </param>
    /// <param name="speedName">
    /// Имя канала "Скорость", значения в км/ч.
    /// </param>
    public static void AddCurvatureViaCourseSpeed(string cadrFullName, string pathDirSave,
        string courseName, string speedName)
    {
        Frame cadr = new(cadrFullName);

        // Массив значений канала "Курс" [градусы]:
        double[] courseInitial = cadr.Channels[courseName].Items;

        // Массив значений канала "Скорость" [км/ч]:
        double[] speedInitial = cadr.Channels[speedName].Items;

        // Частота опроса, Гц:
        double sampleRate = cadr.Channels[speedName].Sampling;

        int[] ind = Arrays.Thinning(courseInitial);
        int nSites = ind.Length;

        double[] t = new double[nSites];
        double[] speed = new double[nSites];
        double[] course = new double[nSites];

        for (int i = 0; i < nSites; i++)
        {
            t[i] = (ind[i] + 1) / sampleRate;
            speed[i] = speedInitial[ind[i]] / 3.6; // Скорость теперь в м/с.
            course[i] = Math.PI * courseInitial[ind[i]] / 180; // Курс в радианах.
        }

        double[] s = IntegralCumTrap(t, speed); // Массив приращений пути, м.

        double[] crvtr = Pointwise.Abs(Diff1Accuracy2(s, course)); // Кривизна.
        double[] crvtrs = SmoothingByIntDiff(t, crvtr, 5); // Сглаженная кривизна.

        // Приведение к исходным данным:

        // Исходная длина каналов:
        int nInitial = cadr.Channels[speedName].Length;

        // Массив моментов времени для исходных данных:
        double[] tInitial = new double[nInitial];

        for (int i = 0; i < nInitial; i++)
        {
            tInitial[i] = (i + 1) / sampleRate;
        }

        // Приводим кривизну к исходной частоте выборки:
        double[] curvature =  Interpolations.Linear(t, crvtrs, tInitial);

        Channel curvatureChannel = new(new(1, curvature))
        {
            Sampling = sampleRate,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.
                        
        // Имя кадра (без пути к нему):
        string cadrName = Path.GetFileName(cadrFullName);

        // Новый полный путь:
        string newCadrFullName = Path.Combine(pathDirSave, cadrName);

        cadr.Save(newCadrFullName, cadr.Format);
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
        double[][] xyzData = Coordinates.GeoToUSR(lat, lon, height);
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
    /// Вычисление кривизны по GPS-координатам с учётом возможной вырожденности траектории в точку.
    /// </summary>
    /// <param name="sampleRate">
    /// Частота опроса, Гц.
    /// </param>
    /// <param name="speed">
    /// Массив значений скорости, км/ч.
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
    public static double[] CurvatureGPS(double sampleRate, double[] speed, double[] lat, double[] lon, double[] height, int nIt)
    {                        
        // Передискретизация к частоте опроса 1 Гц:
        if (sampleRate > 1)
        {
            lat = Signals.ResamplingLowering(lat, (int)sampleRate);
            lon = Signals.ResamplingLowering(lon, (int)sampleRate);
            height = Signals.ResamplingLowering(height, (int)sampleRate);
        }

        int n = lat.Length;

        // Проверка траектории на вырожденность:
        if (Arrays.AbsValueMaximum(speed) <= 2)
        {
            return Arrays.IdenticalElements(0.0, n);
        }

        // Сглаживание GPS-данных:
        lat = SmoothingByIntDiff(lat, nIt);
        lon = SmoothingByIntDiff(lon, nIt);
        height = SmoothingByIntDiff(height, nIt);

        // Переводим в USR-координаты:
        double[][] xyzData = Coordinates.GeoToUSR(lat, lon, height);
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
    /// Линейная апроксимация временного ряда по методу наименьших квадратов.
    /// </summary>
    /// <param name="tb">
    /// Начальный момент времени.
    /// </param>
    /// <param name="te">
    /// Конечный момент времени.
    /// </param>
    /// <param name="y">
    /// y = (y[0],...,y[n-1]) - значения временного ряда в моменты времени t=(tb=t[0],...,t[n-1]=te). Предполагается, что t - арифметическая прогрессия с шагом dt=(te-tb)/(n-1): t[k]=tb+k*dt.
    /// </param>
    /// <returns>
    /// Коэффициенты coeffs[0] = slope, coeffs[1] = val аппроксимирующей функции: t |-> slope*t + val.
    /// </returns>
    public static double[] LinearLeastSquareApproximation(double tb, double te, double[] y)
    {
        int n = y.Length;
        double ndc = (double)n;


        if (n == 0)
        {
            throw new Exception("В функцию <<LinearLeastSquareApproximation>> передан пустой массив данных!");
        }

        double[] coeffs = new double[2];

        if (n == 1)
        {
            coeffs[0] = 0;
            coeffs[1] = y[0];
            return coeffs;
        }

        double T = te - tb;
        double mt = 0.5 * (te + tb);

        double my = y.Average();

        double s = 0.0;
        for (int i = 0; i < n; i++)
        {
            s += (i - 0.5 * (ndc - 1.0)) * (y[i] - my);
        }

        double slope = 12.0 * s / (T * ndc * (ndc + 1));
        double val = my - mt * slope;

        coeffs[0] = slope;
        coeffs[1] = val;
        return coeffs;
    }

    /// <summary>
    /// Наклон для линейной апроксимации временного ряда по методу наименьших квадратов.
    /// </summary>
    /// <param name="dT">
    /// "Ширина" по оси абсцисс, т.е. [конечный момент времени] [минус] [начальный момент времени].
    /// </param>
    /// <param name="y">
    /// y = (y[0],...,y[n-1]) - значения временного ряда в моменты времени t=(tb=t[0],...,t[n-1]=te). Предполагается, что t - арифметическая прогрессия с шагом dt=(te-tb)/(n-1): t[k]=tb+k*dt.
    /// </param>
    /// <returns>
    /// Коэффициент slope аппроксимирующей функции: t |-> slope*t + val.
    /// </returns>
    public static double SlopeLinearLSA(double dT, double[] y)
    {
        int n = y.Length;
        double ndc = (double)n;

        if (n == 0)
        {
            throw new Exception("В функцию <<LinearLeastSquareApproximation>> передан пустой массив данных!");
        }

        double slope;

        if (n == 1)
        {
            slope = 0.0;
            return slope;
        }

        double my = y.Average();

        double s = 0.0;
        for (int i = 0; i < n; i++)
        {
            s += (i - 0.5 * (ndc - 1.0)) * (y[i] - my);
        }

        slope = 12.0 * s / (dT * ndc * (ndc + 1));

        return slope;
    }

    /// <summary>
    /// Наклоны графика временного ряда y(t) по методу наименьших квадратов.
    /// </summary>
    /// <param name="sampleRate">
    /// Частота выборки, Гц.
    /// </param>
    /// <param name="y">
    /// Массив значений временного ряда.
    /// </param>
    /// <param name="timeStep">
    /// Шаг по времени для усреднения, c.
    /// </param>
    /// <returns>
    /// Четыре массива: наклоны на участках, значения на участках, индексы моментов времени: начало и конец участка.
    /// </returns>
    public static double[][] GetSlopes(double sampleRate, double[] y, double timeStep)
    {
        int n = y.Length; // Длина входного массива.            

        double[][] slopesData = new double[3][]; // Выходной массив.                                                     

        int di = (int)Math.Round(timeStep * sampleRate); // Шаг по индексу, соответствующий timeStep.            

        int m = (int)Math.Ceiling((double)(n / di)) + 1; // Длина массивов slopesData[c] с запасом.            

        double[] slopes = new double[m];       // slopesData[0]
        double[] indexesBegin = new double[m]; // slopesData[1]
        double[] indexesEnd = new double[m];   // slopesData[2]            

        int ib = 0; int ie = di - 1; // Индексы двух моментов времени с интервалом timeStep.

        int c = 0; // Счётчик.
        while ((ib < ie) && (ie < n))
        {
            slopes[c] = SlopeLinearLSA(timeStep, Arrays.SubArray(y, Arrays.IndexesArray(ib, 1, ie)));

            indexesBegin[c] = ib;
            indexesEnd[c] = ie;

            c++;
            ib = ie + 1;
            ie = Math.Min(ie + di, n - 1);
        }

        if (c < m)
        {
            Array.Resize(ref slopes, c);
            Array.Resize(ref indexesBegin, c);
            Array.Resize(ref indexesEnd, c);
        }

        slopesData[0] = slopes;
        slopesData[1] = indexesBegin;
        slopesData[2] = indexesEnd;

        return slopesData;
    }
}
