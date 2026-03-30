using Simargl.Frames;
using System;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции для работы с кадрами регистрации.
/// </summary>
public static class Cadrs
{
    /// <summary>
    /// Добавляет процесс "Режимы" в кадр регистрации.
    /// </summary>
    /// <param name="fileName">
    /// Имя кадра регистрации (полный путь к кадру).
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
    /// <param name="path2Save">
    /// Путь, куда сохранить кадр с добавленным каналом "Режимы".
    /// </param>
    public static void AddRegimes(string fileName, string speedName, int nIt, double regSampleRate, double speedTol, double accTol, double stopValue, double pullValue, double brakingValue, double runValue, string path2Save)
    {
        //  Открываем кадр.
        Frame cadr = new(fileName);

        speedTol /= 3.6; // Перевели в [м/с].

        // массив скоростей [м/с]:
        double[] v = Arrays.Scaling(cadr.Channels[speedName].Vector.ToArray(), 1.0/3.6);
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

        Channel regimeChannel = new(new(1, new(regime)))
        {
            Sampling = regSampleRate,
            Unit = string.Empty,
            Name = "Regimes"
        };

        cadr.Channels.Add(regimeChannel); // Добавили канал с режимами.

        cadr.Save(path2Save, cadr.Format);
    }

    /// <summary>
    /// Добавляет процесс кривизны в кадр регистрации.
    /// </summary>
    /// <param name="fileName">
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
    /// <param name="path2Save">
    /// Путь, куда сохранить кадр с добавленным каналом кривизны.
    /// </param>
    public static void AddCurvature(string fileName, string latName, string lonName, string heightName, int nIt, string path2Save)
    {
        //  Открываем кадр.
        Frame cadr = new(fileName);            

        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;    // = new double[length];
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(176.0, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        double[] curvature = Calculus.CurvatureGPS(sampleRate, lat, lon, height, nIt);

        Channel curvatureChannel = new(new(1, new(curvature)))
        {
            Sampling = 1.0,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel);           

        cadr.Save(path2Save, cadr.Format);
    }

    /// <summary>
    /// Добавляет процесс кривизны в кадр регистрации.
    /// </summary>
    /// <param name="cadr">
    /// Кадр.
    /// </param>
    /// <param name="curvatureName">
    /// Имя для канала с процессом кривизны.
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
    public static void AddCurvature(Frame cadr, string curvatureName,
                       string latName, string lonName, string heightName)
    {
        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height = new double[length];
        if (heightName.Length > 0)
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }
        
        double[] curvature = Calculus.CurvatureGPS(sampleRate, lat, lon, height, 50);

        Channel curvatureChannel = new(new(1, new(curvature)))
        {
            Sampling = 1.0,
            Unit = "1/m",
            Name = curvatureName
        };

        cadr.Channels.Add(curvatureChannel);
    }

    /// <summary>
    /// Добавляет процессы кривизны и признака в кадр регистрации.
    /// </summary>
    /// <param name="fileName">
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
    /// <param name="path2Save">
    /// Путь, куда сохранить кадр с добавленными каналами кривизны и признака.
    /// </param>
    public static void AddCurvaturePriznak(string fileName, string latName, string lonName, string heightName, int nIt, double priznakSampleRate, double tolerance2LineCurvature, double notNullValue, string path2Save)
    {
        //  Открываем кадр.
        Frame cadr = new(fileName);

        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;    // = new double[length];
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(176.0, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, lat, lon, height, nIt);

        Channel curvatureChannel = new(new(1, new(curvature)))
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


        Channel priznakChannel = new(new(1, new(priznak)))
        {
            Sampling = priznakSampleRate,
            Unit = string.Empty,
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

        cadr.Save(path2Save, cadr.Format);
    }

    // Добавление кривизны, признака, сил и моментов (проект "Иволга")
    /// <summary>
    /// Добавляет процессы кривизны и признака в кадр регистрации.
    /// </summary>
    /// <param name="fileName">
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
    /// <param name="ChannelsForRestoring"></param>
    /// <param name="factorsNames"></param>
    /// <param name="factorsUnits"></param>
    /// <param name="InvInfMatrix"></param>
    /// <param name="path2Save"></param>
    public static void Add4IvolgaCPFT(string fileName, string latName, string lonName, string heightName, int nIt, double priznakSampleRate, double tolerance2LineCurvature, double notNullValue, string[] ChannelsForRestoring, string[] factorsNames, string[] factorsUnits, double[,] InvInfMatrix, string path2Save)
    {
        //  Открываем кадр.
        Frame cadr = new(fileName);

        // Формируем процесс кривизны:
        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;    // = new double[length];
        if (heightName == "")
        {
            height = Arrays.ConstArray(176.0, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, lat, lon, height, nIt);

        Channel curvatureChannel = new(new(1, new(curvature)))
        {
            Sampling = 1.0,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.

        // Формируем процесс "Признак":
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

        Channel priznakChannel = new(new(1, new(priznak)))
        {
            Sampling = priznakSampleRate,
            Unit = string.Empty,
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

        // Восстанавливаем силы и моменты:                       

        double[][] S = new double[12][]; // Напряжения.
        for (int i = 0; i < 12; i++)
        {
            S[i] = cadr.Channels[ChannelsForRestoring[i]].Vector.ToArray();
        }

        int nTimes = S[0].Length; // Число моментов времени.

        double[][] QM = new double[18][]; // Силы и моменты.
        for (int i = 0; i < 18; i++)
        {
            QM[i] = new double[nTimes];
        }

        double currentSum1;
        double currentSum2;
        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {                
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < 6; i++)
            {                    
                currentSum1 = 0.0;
                currentSum2 = 0.0;
                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < 6; j++)
                {
                    currentSum1 += InvInfMatrix[i, j] * S[j][t];
                    currentSum2 += InvInfMatrix[i, j] * S[6 + j][t];
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)
                    
                QM[i][t] = currentSum1;
                QM[6 + i][t] = currentSum2;
            }
            // ...цикл по восстанавливаемым силовым факторам

            QM[12][t] = QM[0][t] - QM[6][t];
            QM[13][t] = QM[1][t] + QM[7][t];
            QM[14][t] = - QM[2][t] + QM[8][t];
            QM[15][t] = QM[3][t] - QM[9][t];
            QM[16][t] = - 0.3 * QM[2][t] + QM[4][t] - 0.3 * QM[8][t] + QM[10][t];
            QM[17][t] = 0.3 * QM[1][t] - QM[5][t] - 0.3 * QM[7][t] + QM[11][t];
        }
        // ...цикл по временнЫм отсчётам.

        // Формируем новык каналы и добавляем их в кадр:

        double sr4QM = cadr.Channels[ChannelsForRestoring[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < 18; i++)
        {
            Channel qmChannel = new(new(1, new(QM[i])))
            {
                Sampling = sr4QM,
                Unit = factorsUnits[i],
                Name = factorsNames[i]
            };

            cadr.Channels.Add(qmChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам

        cadr.Save(path2Save, cadr.Format);
    }

    /// <summary>
    /// Добавляет процессы кривизны,признака, режимов, сил и моментов в кадр регистрации.
    /// </summary>
    /// <param name="fileName">
    /// Имя кадра регистрации (полный путь к кадру).
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
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
    /// <param name="nItCr">
    /// Число итераций для сглаживания GPS-координат.
    /// </param>
    /// <param name="nItReg">
    /// Число итераций для сглаживания GPS-скорости.
    /// </param>
    /// <param name="addSampleRate">
    /// Частота дискретизации для признака и режима, Гц.
    /// </param>
    /// <param name="tolerance2LineCurve">
    /// Пограничное значение процесса "Признак", соответствующее переходу из прямой в кривую.
    /// </param>
    /// <param name="notNullValue">
    /// Значение процесса "Признак", соответствующее движению в кривой.
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
    /// <param name="ChannelsForRestoring">
    /// Имена каналов для восстановления силовых факторов.
    /// </param>
    /// <param name="factorsNames">
    /// Имена восстанавливаемых силовых факторов.
    /// </param>
    /// <param name="factorsUnits">
    /// Единицы измерения восстанавливаемых силовых факторов.
    /// </param>
    /// <param name="InvInfMatrix">
    /// Матрица, обратная к матрице влияния.
    /// </param>
    /// <param name="path2Save">
    /// Путь, куда сохранить кадр с добавленными каналами.
    /// </param>
    public static void Add4IvolgaCrPrRegFT(string fileName, string speedName, string latName, string lonName, string heightName, int nItCr, int nItReg, double addSampleRate, double tolerance2LineCurve, double notNullValue, double speedTol, double accTol, double stopValue, double pullValue, double brakingValue, double runValue, string[] ChannelsForRestoring, string[] factorsNames, string[] factorsUnits, double[,] InvInfMatrix, string path2Save)
    {
        //  Открываем кадр.
        Frame cadr = new(fileName);

        // Формируем процесс кривизны:
        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] speed = cadr.Channels[speedName].Vector.ToArray();  // скорость, км/ч
        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;    // = new double[length];
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(176.0, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, speed, lat, lon, height, nItCr);

        Channel curvatureChannel = new(new(1, new(curvature)))
        {
            Sampling = 1.0,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.

        // Формируем процесс "Признак":
        double[] priznak1 = new double[length];
        for (int i = 0; i < length; i++)
        {
            if (curvature[i] > tolerance2LineCurve)
            {
                priznak1[i] = notNullValue;
            }
            else
            {
                priznak1[i] = 0.0;
            }
        }

        double[] priznak;   // = new double[length * (int)Math.Round(addSampleRate)];
        if (addSampleRate > 1.0)
        {
            priznak = Signals.ResamplingFrom1toHigher(priznak1, addSampleRate);
        }
        else
        {
            priznak = priznak1;
        }

        Channel priznakChannel = new(new(1, new(priznak)))
        {
            Sampling = addSampleRate,
            Unit = string.Empty,
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

        // Формируем процесс "Режимы":

        speedTol /= 3.6; // Перевели в [м/с].

        // массив скоростей [м/с]:
        double[] v = Arrays.Scaling(speed, 1.0 / 3.6); // скорость, м/с.
        double[] vs = Calculus.SmoothingByIntDiff(v, nItReg); // массив сглаженных скоростей
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

        double[] regime;    // = new double[nSites * (int)Math.Round(addSampleRate)];
        if (addSampleRate > 1.0)
        {
            regime = Signals.ResamplingFrom1toHigher(regime1, addSampleRate);
        }
        else
        {
            regime = regime1;
        }

        Channel regimeChannel = new(new(1, new(regime)))
        {
            Sampling = addSampleRate,
            Unit = string.Empty,
            Name = "Regimes"
        };

        cadr.Channels.Add(regimeChannel); // Добавили канал с режимами.

        // Восстанавливаем силы и моменты:                       

        double[][] S = new double[12][]; // Напряжения.
        for (int i = 0; i < 12; i++)
        {
            S[i] = cadr.Channels[ChannelsForRestoring[i]].Vector.ToArray();
        }

        int nTimes = S[0].Length; // Число моментов времени.

        double[][] QM = new double[18][]; // Силы и моменты.
        for (int i = 0; i < 18; i++)
        {
            QM[i] = new double[nTimes];
        }

        double currentSum1;
        double currentSum2;
        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < 6; i++)
            {
                currentSum1 = 0.0;
                currentSum2 = 0.0;
                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < 6; j++)
                {
                    currentSum1 += InvInfMatrix[i, j] * S[j][t];
                    currentSum2 += InvInfMatrix[i, j] * S[6 + j][t];
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)

                QM[i][t] = currentSum1;
                QM[6 + i][t] = currentSum2;
            }
            // ...цикл по восстанавливаемым силовым факторам

            QM[12][t] = QM[0][t] - QM[6][t];
            QM[13][t] = QM[1][t] + QM[7][t];
            QM[14][t] = -QM[2][t] + QM[8][t];
            QM[15][t] = QM[3][t] - QM[9][t];
            QM[16][t] = -0.3 * QM[2][t] + QM[4][t] - 0.3 * QM[8][t] + QM[10][t];
            QM[17][t] = 0.3 * QM[1][t] - QM[5][t] - 0.3 * QM[7][t] + QM[11][t];
        }
        // ...цикл по временнЫм отсчётам.

        // Формируем новык каналы и добавляем их в кадр:

        double sr4QM = cadr.Channels[ChannelsForRestoring[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < 18; i++)
        {
            Channel qmChannel = new(new(1, new(QM[i])))
            {
                Sampling = sr4QM,
                Unit = factorsUnits[i],
                Name = factorsNames[i]
            };

            cadr.Channels.Add(qmChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам

        cadr.Save(path2Save, cadr.Format);
    }

    /// <summary>
    /// Добавляет процессы "угол поворота кузова" и "смещение центра кузова" в кадр регистрации.
    /// </summary>
    /// <param name="path2Cadr">
    /// Имя кадра регистрации (полный путь к кадру).
    /// </param>
    /// <param name="dL1Name">
    /// Имя процесса "ходомер-1".
    /// </param>
    /// <param name="dL2Name">
    /// Имя процесса "ходомер-2".
    /// </param>
    /// <param name="dL3Name">
    /// Имя процесса "ходомер-3".
    /// </param>
    /// <param name="Lc">
    /// Характерный масштаб, мм.
    /// </param>
    /// <param name="L">
    /// Длины гасителей в начальном состоянии, мм.
    /// </param>
    /// <param name="maxdL">
    /// Максимальные величины хода ходомеров, мм.
    /// </param>
    /// <param name="zT">
    /// z-координаты (обезразмеренные) точек крепления гасителей к тележке.
    /// </param>
    /// <param name="zK">
    /// z-координаты (обезразмеренные) точек крепления гасителей к кузову.
    /// </param>
    /// <param name="xT">
    /// x-координаты (обезразмеренные) точек крепления гасителей к тележке.
    /// </param>
    /// <param name="xK">
    /// x-координаты (обезразмеренные) точек крепления гасителей к кузову.
    /// </param>
    /// <param name="A0">
    /// Матрица в методе Ньютона для вычисления начального приближения.
    /// </param>
    /// <param name="tolU">
    /// Точность вычисления угла поворота, град.
    /// </param>
    /// <param name="tolL">
    /// Точность вычисления смещения центра кузова, мм.
    /// </param>
    /// <param name="path2Save">
    /// Путь, куда сохранить кадр с добавленными каналами.
    /// </param>
    public static void AddRotDisp(string path2Cadr, string dL1Name, string dL2Name, string dL3Name, double Lc, double[] L, double[] maxdL, double[] zT, double[] zK, double[] xT, double[] xK, double[,] A0, double tolU, double tolL, string path2Save)
    {
        //  Открываем кадр:
        Frame cadr = new(path2Cadr);

        // Считываем нужные процессы "ходомер":
        double[] dL1 = cadr.Channels[dL1Name].Vector.ToArray();
        double[] dL2 = cadr.Channels[dL2Name].Vector.ToArray();
        double[] dL3 = cadr.Channels[dL3Name].Vector.ToArray();            

        int nSites = dL1.Length; // Число отсчётов

        // Проверка на адекватность значений процессов "ходомер":
        double maxdL1 = maxdL[0]; double maxdL2 = maxdL[1]; double maxdL3 = maxdL[2];

        bool adequateData = true;
        for (int i = 0; i < nSites; i++)
        {
            if (Math.Abs(dL1[i]) > maxdL1 || Math.Abs(dL2[i]) > maxdL2 || Math.Abs(dL3[i]) > maxdL3)
            {
                adequateData = false;
                break;
            }
        }

        if (!adequateData)
        {
            return;
        }

        double sampleRate = cadr.Channels[dL1Name].Sampling;

        double[] u = new double[nSites]; // Массив углов поворота, град.
        double[] z = new double[nSites]; // Массив z-координат смещения кузова, мм.
        double[] x = new double[nSites]; // Массив x-координат смещения кузова, мм.
                        
        double[] dL = new double[3];
        for (int i = 0; i < nSites; i++)
        {
            dL[0] = dL1[i]; dL[1] = dL2[i]; dL[2] = dL3[i];

            double[] uzx = WagonKinematics.RotDisp(dL, Lc, L, zT, zK, xT, xK, A0, tolU, tolL);

            u[i] = uzx[0];
            z[i] = uzx[1];
            x[i] = uzx[2];
        }

        Channel uChannel = new(new(1, new(u)))
        {
            Sampling = sampleRate,
            Unit = "grad",
            Name = "dU(rot)"
        };
        cadr.Channels.Add(uChannel); // Добавили канал с углом поворота.

        Channel zChannel = new(new(1, new(z)))
        {
            Sampling = sampleRate,
            Unit = "mm",
            Name = "dZ(long)"
        };
        cadr.Channels.Add(zChannel); // Добавили канал с продольным смещением.

        Channel xChannel = new(new(1, new(x)))
        {
            Sampling = sampleRate,
            Unit = "mm",
            Name = "dX(lat)"
        };
        cadr.Channels.Add(xChannel); // Добавили канал с поперечным смещением.

        cadr.Save(path2Save, cadr.Format);
    }

    /// <summary>
    /// Подсчёт абсолютных значений перемещений, больших заданных величин.
    /// </summary>
    /// <param name="path2Cadr">
    /// Имя кадра регистрации (полный путь к кадру).
    /// </param>
    /// <param name="dUname">
    /// Имя процесса "угол поворота".
    /// </param>
    /// <param name="dZname">
    /// Имя процесса "продольное смещение кузова".
    /// </param>
    /// <param name="dXname">
    /// Имя процесса "поперечное смещение кузова".
    /// </param>
    /// <param name="nKnots">
    /// Число узлов.
    /// </param>
    /// <param name="rdU">
    /// Ширина диапазона угла поворота.
    /// </param>
    /// <param name="rdZ">
    /// Ширина диапазона
    /// </param>
    /// <param name="rdX">
    /// Ширина диапазона
    /// </param>
    /// <param name="tab">
    /// Матрица с абсолютными значениями перемещений.
    /// </param>
    public static void Tab4dUdZdX(string path2Cadr, string dUname, string dZname, string dXname, double nKnots, double rdU, double rdZ, double rdX, double[,] tab)
    {
        double stepdU = rdU / nKnots;
        double stepdZ = rdZ / nKnots;
        double stepdX = rdX / nKnots;

        int nIt = (int)(nKnots / 2) + 1;         

        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        double[] dU = cadr.Channels[dUname].Vector.ToArray();
        double[] dZ = cadr.Channels[dZname].Vector.ToArray();
        double[] dX = cadr.Channels[dXname].Vector.ToArray();

        int nSites = dU.Length;

        double currentdU; double valuedU;
        double currentdZ; double valuedZ;
        double currentdX; double valuedX;

        for (int i = 0; i < nSites; i++)
        {
            currentdU = Math.Abs(dU[i]);
            currentdZ = Math.Abs(dZ[i]);
            currentdX = Math.Abs(dX[i]);

            for (int k = 0; k < nIt; k++)
            {
                valuedU = stepdU * k;
                valuedZ = stepdZ * k;
                valuedX = stepdX * k;

                if (currentdU >= valuedU)
                {
                    tab[0, k] += currentdU;
                }

                if (currentdZ >= valuedZ)
                {
                    tab[1, k] += currentdZ;
                }

                if (currentdX >= valuedX)
                {
                    tab[2, k] += currentdX;
                }
            }
        }            
    }

    /// <summary>
    /// Пройденный путь, при условии, что перемещения больше заданных величин.
    /// </summary>
    /// <param name="path2Cadr">
    /// Имя кадра регистрации (полный путь к кадру).
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="dUname">
    /// Имя процесса "угол поворота".
    /// </param>
    /// <param name="dZname">
    /// Имя процесса "продольное смещение кузова".
    /// </param>
    /// <param name="dXname">
    /// Имя процесса "поперечное смещение кузова".
    /// </param>
    /// <param name="priznakName">
    /// Имя процесса "Признак".
    /// </param>
    /// <param name="nKnots">
    /// Число узлов.
    /// </param>
    /// <param name="rdU">
    /// Ширина диапазона угла поворота, град.
    /// </param>
    /// <param name="rdZ">
    /// Ширина диапазона продольного смещения, мм.
    /// </param>
    /// <param name="rdX">
    /// Ширина диапазона поперечного смещения, мм.
    /// </param>
    /// <param name="tab0">
    /// Матрица со значениями пройденного пути по прямой, км.
    /// </param>
    /// <param name="tab1">
    /// Матрица со значениями пройденного пути по кривой, км.
    /// </param>
    public static void TabdUdZdX(string path2Cadr, string speedName, string latName, string lonName,
                                 string dUname, string dZname, string dXname, string priznakName,
                                 double nKnots, double rdU, double rdZ, double rdX,
                                 double[,] tab0, double[,] tab1)
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return; // Если GPS отвалилась, выходим в головную программу.
        }

        double priznakSR = cadr.Channels[priznakName].Sampling; // Частота опроса.
        double[] priznak = cadr.Channels[priznakName].Vector.ToArray(); // Признак.

        // Передискретизированный массив скоростей, км/ч.
        double[] speed = Signals.ResamplingFrom1toHigher(cadr.Channels[speedName].Vector.ToArray(), priznakSR);
        
        double dt = 1 / (3600 * priznakSR); // Квант времени, ч.

        double stepdU = rdU / nKnots;
        double stepdZ = rdZ / nKnots;
        double stepdX = rdX / nKnots;

        int nIt = (int)(nKnots / 2) + 1;

        double[] dU = cadr.Channels[dUname].Vector.ToArray();
        double[] dZ = cadr.Channels[dZname].Vector.ToArray();
        double[] dX = cadr.Channels[dXname].Vector.ToArray();

        int nSites = dU.Length;

        double currentdU; double valuedU;
        double currentdZ; double valuedZ;
        double currentdX; double valuedX;

        double ds;

        int[][] ind = Arrays.FindIndexes(cadr.Channels[priznakName], x => x == 0.0, x => x > 0.0);
        int[] i0 = ind[0];
        int[] i1 = ind[1];

        if (i0.Length != 0)
        {
            for (int i = 0; i < i0.Length; i++)
            {
                currentdU = Math.Abs(dU[i0[i]]);
                currentdZ = Math.Abs(dZ[i0[i]]);
                currentdX = Math.Abs(dX[i0[i]]);

                ds = speed[i0[i]] * dt; // Текущее приращение пути, км.

                for (int k = 0; k < nIt; k++)
                {
                    valuedU = stepdU * k;
                    valuedZ = stepdZ * k;
                    valuedX = stepdX * k;

                    if (currentdU >= valuedU)
                    {
                        //tab0[0, k] += currentdU;
                        tab0[0, k] += ds;
                    }

                    if (currentdZ >= valuedZ)
                    {
                        //tab0[1, k] += currentdZ;
                        tab0[1, k] += ds;
                    }

                    if (currentdX >= valuedX)
                    {
                        //tab0[2, k] += currentdX;
                        tab0[2, k] += ds;
                    }
                }
            }
        }

        if (i1.Length != 0)
        {
            for (int i = 0; i < i1.Length; i++)
            {
                currentdU = Math.Abs(dU[i1[i]]);
                currentdZ = Math.Abs(dZ[i1[i]]);
                currentdX = Math.Abs(dX[i1[i]]);

                ds = speed[i1[i]] * dt; // Текущее приращение пути, км.

                for (int k = 0; k < nIt; k++)
                {
                    valuedU = stepdU * k;
                    valuedZ = stepdZ * k;
                    valuedX = stepdX * k;

                    if (currentdU >= valuedU)
                    {
                        //tab1[0, k] += currentdU;
                        tab1[0, k] += ds;
                    }

                    if (currentdZ >= valuedZ)
                    {
                        //tab1[1, k] += currentdZ;
                        tab1[1, k] += ds;
                    }

                    if (currentdX >= valuedX)
                    {
                        //tab1[2, k] += currentdX;
                        tab1[2, k] += ds;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Предварительная обработка по "Иволге".
    /// </summary>
    /// <param name="path2Cadr">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="speedDiapBnds">
    /// Границы диапазонов скоростей.
    /// </param>
    /// <param name="procNames">
    /// Имена процессов.
    /// </param>
    /// <param name="min">
    /// Матрица минимумов.
    /// </param>
    /// <param name="mean">
    /// Матрица средних значений.
    /// </param>
    /// <param name="max">
    /// Матрица максимумов.
    /// </param>
    /// <param name="vartn">
    /// Матрица дисперсий.
    /// </param>
    /// <param name="volumes">
    /// Матрица объёмов данных.
    /// </param>
    /// <param name="trackLength">
    /// Длина пробега.
    /// </param>
    /// <param name="cadrsNumber">
    /// Количество кадров с GPS.
    /// </param>
    /// <param name="timesOnSpeeds">
    /// Распределение времени движения по скоростям.
    /// </param>
    /// <returns>
    /// Длина пробега.
    /// </returns>
    public static double Ape90Predv(string path2Cadr, string speedName, string latName, string lonName, double[] speedDiapBnds,
                                    string[] procNames, double[,] min, double[,] mean, double[,] max, double[,] vartn,
                                    long[,] volumes, double trackLength, int[] cadrsNumber, double[] timesOnSpeeds)
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();

        if (Arrays.OnlyZeros(lat, lon))
        {
            return trackLength; // Если GPS отвалилась, выходим в головную программу.
        }

        cadrsNumber[0] += 1; // Уточнение числа кадров с GPS.

        int nProcs = procNames.Length; // Количество процессов.

        int sampleRate = (int)cadr.Channels[procNames[0]].Sampling; // Частота опроса для процессов, Гц.

        double[] speed = cadr.Channels[speedName].Vector.ToArray();  // массив скоростей, км/ч.

        trackLength += Calculus.IntegralTrap(speed) / 3600.0; // Пробег + приращение пробега, км.

        double[][] allData = new double[nProcs][];
        for (int np = 0; np < nProcs; np++)
        {
            allData[np] = cadr.Channels[procNames[np]].Vector.ToArray();
        }

        int nTimeIntervals = speed.Length - 1; // Количество временнЫх интервалов между GPS-сигналами.
                        
        double currentSpeed; int ns;
        for (int t = 0; t < nTimeIntervals; t++) // Цикл по временнЫм интервалам...
        {
            currentSpeed = 0.5 * (speed[t] + speed[t + 1]); // Скорость на текущем временнОм интервале.

            ns = Arrays.IntervalNumber(speedDiapBnds, currentSpeed); // Определили номер диапазона скорости.
            timesOnSpeeds[ns] += 1 / 3600.0;

            double currentMin;
            double newMean;
            double currentMax;
            double oldVar;

            double oldMean; long oldVolume;
            for (int np = 0; np < nProcs; np++) // Цикл по процессам...
            {
                double[] currentData = 
                Arrays.SubArray(allData[np], Arrays.IndexesArray(t * sampleRate, 1, (t + 1) * sampleRate - 1));

                oldVolume = volumes[np, ns];

                if (oldVolume == 0)
                {
                    volumes[np, ns] = currentData.Length;

                    min[np, ns] = currentData.Min();
                    mean[np, ns] = currentData.Average();
                    max[np, ns] = currentData.Max();
                    vartn[np, ns] = Statistics.Var(currentData);
                        
                }
                else
                {
                    volumes[np, ns] += currentData.Length;

                    currentMin = currentData.Min();                        
                    currentMax = currentData.Max();                        

                    if (currentMin < min[np, ns])
                    {
                        min[np, ns] = currentMin; // Уточняем минимум.
                    }

                    if (currentMax > max[np, ns])
                    {
                        max[np, ns] = currentMax; // Уточняем максимум.
                    }

                    oldMean = mean[np, ns];
                    newMean = Statistics.MeanReCalcByAddData(oldMean, oldVolume, currentData); // Корректируем среднее.
                    mean[np, ns] = newMean;

                    oldVar = vartn[np, ns];
                    vartn[np, ns] = Statistics.VarReCalcByAddData(oldMean, newMean, oldVar, oldVolume, currentData);
                }
            } // ...цикл по процессам.
        }// ...цикл по временнЫм интервалам.

        return trackLength;
    }

    /*
    /// <summary>
    /// Сохраняет файлы в формате Testlab.
    /// </summary>
    /// <param name="file">
    /// Файл.
    /// </param>
    public static void ConvertFrameToTestlab(FileInfo file)
    {
        string fileName = file.Name.Replace(file.Extension, ".Testlab");
        string fileFullName = Path.Combine(FullName.Replace(file.Name, fileName));
        frame.Save(fileFullName, StorageFormat.TestLab);
        file.Delete();
    }
    */

    /// <summary>
    /// Пройденный путь по диапазонам скоростей и типам трека (прямая/кривая).
    /// </summary>
    /// <param name="path2Cadr">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="priznakName">
    /// Имя процесса "Признак".
    /// </param>
    /// <param name="speedRanges">
    /// Границы диапазонов скоростей, км/ч.
    /// </param>    
    /// <param name="trackLengths">
    /// Матрица пройденных путей, км.
    /// </param>
    /// <returns>
    /// Уточнённая матрица пройденных путей, км.
    /// </returns>
    public static double[,] TrackLengths(string path2Cadr, string speedName, string latName,
                            string lonName, string priznakName,
                            double[] speedRanges, double[,] trackLengths)
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);               

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return trackLengths; // Если GPS отвалилась, выходим в головную программу.
        }

        double priznakSR = cadr.Channels[priznakName].Sampling; // Частота опроса.
                
        double[] priznak = cadr.Channels[priznakName].Vector.ToArray(); // Признак.

        // Передискретизированный массив скоростей, км/ч.
        double[] speed = Signals.ResamplingFrom1toHigher(cadr.Channels[speedName].Vector.ToArray(), priznakSR);
        //IEnumerable<double> speed = cadr.Channels[speedName];

        double ds = 0.5 * (speedRanges[1] - speedRanges[0]); // Полуширина диапазонов скоростей, км/ч.

        double dt = 1 / (3600 * priznakSR); // Квант времени, ч.

        int nTypeTrack;
        for (int i = 0; i < speedRanges.Length; i++)
        {
            double currentRange = speedRanges[i];
            double v1 = currentRange - ds;
            double v2 = currentRange + ds;

            int[] ind = Arrays.FindIndexes(speed, x => x >= v1 && x < v2);

            if (ind.Length != 0)
            {
                for (int k = 0; k < ind.Length; k++)
                {
                    int currentIndex = ind[k];
                    double curentSpeed = speed[currentIndex];
                    double currentPriznak = priznak[currentIndex];

                    if (currentPriznak == 0)
                    {
                        nTypeTrack = 0;
                    }
                    else
                    {
                        nTypeTrack = 1;
                    }
                    
                    trackLengths[i, nTypeTrack] += curentSpeed * dt;
                }
            }
        }

        return trackLengths;
    }

    /// <summary>
    /// Формирование эмпирических распределений (гистограмм).
    /// </summary>
    /// <param name="path2Cadr">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="dUname">
    /// Имя процесса "угол поворота".
    /// </param>
    /// <param name="dZname">
    /// Имя процесса "продольное смещение кузова".
    /// </param>
    /// <param name="dXname">
    /// Имя процесса "поперечное смещение кузова".
    /// </param>
    /// <param name="priznakName">
    /// Имя процесса "Признак".
    /// </param>
    /// <param name="distrUZX">
    /// Массив распределений.
    /// </param>
    /// <returns>
    /// Массив уточнённых распределений.
    /// </returns>
    public static Distribution1D[,] DistributionsCorrection(string path2Cadr, string latName, string lonName,
                       string dUname, string dZname, string dXname, string priznakName,
                       Distribution1D[,] distrUZX)
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return distrUZX; // Если GPS отвалилась, выходим в головную программу.
        }
                        
        int[][] ind = Arrays.FindIndexes(cadr.Channels[priznakName], x => x == 0.0, x => x > 0.0);
        int[] i0 = ind[0];
        int[] i1 = ind[1];

        if (i0.Length != 0)
        {
            distrUZX[0, 0].AddData(Arrays.SubArray(cadr.Channels[dUname], i0));
            distrUZX[1, 0].AddData(Arrays.SubArray(cadr.Channels[dZname], i0));
            distrUZX[2, 0].AddData(Arrays.SubArray(cadr.Channels[dXname], i0));
        }

        if (i1.Length != 0)
        {
            distrUZX[0, 1].AddData(Arrays.SubArray(cadr.Channels[dUname], i1));
            distrUZX[1, 1].AddData(Arrays.SubArray(cadr.Channels[dZname], i1));
            distrUZX[2, 1].AddData(Arrays.SubArray(cadr.Channels[dXname], i1));
        }

        return distrUZX;
    }

    /// <summary>
    /// Уточнение таблицы экстремумов.
    /// </summary>
    /// <param name="path2Cadr">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="dUname">
    /// Имя процесса "угол поворота".
    /// </param>
    /// <param name="dZname">
    /// Имя процесса "продольное смещение кузова".
    /// </param>
    /// <param name="dXname">
    /// Имя процесса "поперечное смещение кузова".
    /// </param>
    /// <param name="priznakName">
    /// Имя процесса "Признак".
    /// </param>
    /// <param name="extremums">
    /// Матрица экстремумов.
    /// </param>
    /// <returns>
    /// Матрица уточнённых экстремумов.
    /// </returns>
    public static double[,] ExtremumsCorrection(string path2Cadr, string latName, string lonName,
                       string dUname, string dZname, string dXname, string priznakName,
                       double[,] extremums)
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return extremums; // Если GPS отвалилась, выходим в головную программу.
        }

        int[][] ind = Arrays.FindIndexes(cadr.Channels[priznakName], x => x == 0.0, x => x > 0.0);
        int[] i0 = ind[0];
        int[] i1 = ind[1];

        double[,] currentExtrs = new double[3, 4];

        if (i0.Length != 0)
        {
            currentExtrs[0, 0] = Arrays.SubArray(cadr.Channels[dUname], i0).Min();
            currentExtrs[0, 2] = Arrays.SubArray(cadr.Channels[dUname], i0).Max();

            currentExtrs[1, 0] = Arrays.SubArray(cadr.Channels[dZname], i0).Min();
            currentExtrs[1, 2] = Arrays.SubArray(cadr.Channels[dZname], i0).Max();

            currentExtrs[2, 0] = Arrays.SubArray(cadr.Channels[dXname], i0).Min();
            currentExtrs[2, 2] = Arrays.SubArray(cadr.Channels[dXname], i0).Max();
        }

        if (i1.Length != 0)
        {
            currentExtrs[0, 1] = Arrays.SubArray(cadr.Channels[dUname], i1).Min();
            currentExtrs[0, 3] = Arrays.SubArray(cadr.Channels[dUname], i1).Max();

            currentExtrs[1, 1] = Arrays.SubArray(cadr.Channels[dZname], i1).Min();
            currentExtrs[1, 3] = Arrays.SubArray(cadr.Channels[dZname], i1).Max();

            currentExtrs[2, 1] = Arrays.SubArray(cadr.Channels[dXname], i1).Min();
            currentExtrs[2, 3] = Arrays.SubArray(cadr.Channels[dXname], i1).Max();
        }

        double totExtr;
        double locExtr;

        for (int i = 0; i < 3; i++)
        {            
            for (int j = 0; j < 2; j++)
            {
                totExtr = extremums[i, j];
                locExtr = currentExtrs[i, j];

                if (locExtr < totExtr)
                {
                    extremums[i, j] = locExtr;
                }
            }

            for (int j = 2; j < 4; j++)
            {
                totExtr = extremums[i, j];
                locExtr = currentExtrs[i, j];

                if (locExtr > totExtr)
                {
                    extremums[i, j] = locExtr;
                }
            }
        }

        return extremums;
    }

    /*
    public static void DynamicQuantization(string path2Cadr, string latName, string lonName,
                       string dUname, string dZname, string dXname, string priznakName, )
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return; // Если GPS отвалилась, выходим в головную программу.
        }

        int[][] ind = Arrays.FindIndexes(cadr.Channels[priznakName], x => x == 0.0, x => x > 0.0);
        int[] i0 = ind[0];
        int[] i1 = ind[1];

        if (i0.Length != 0)
        {
            IEnumerable<double> dU0 = Arrays.SubArray(cadr.Channels[dUname], i0);
            IEnumerable<double> dZ0 = Arrays.SubArray(cadr.Channels[dUname], i0);
            IEnumerable<double> dX0 = Arrays.SubArray(cadr.Channels[dUname], i0);

            double duMin = dU0.Min();
            double duMax = dU0.Max();

            double dzMin = dZ0.Min();
            double dzMax = dZ0.Max();

            double dxMin = dX0.Min();
            double dxMax = dX0.Max();
        }

        

    }
    */

    /// <summary>
    /// Добавление в кадр кривизны и силовых факторов.
    /// </summary>
    /// <param name="path2Cadr">
    /// Полный путь к кадру.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="latName">
    /// Имя процесса "Широта".
    /// </param>
    /// <param name="lonName">
    /// Имя процесса "Долгота".
    /// </param>
    /// <param name="heightName">
    /// Имя процесса "Высота".
    /// </param>
    /// <param name="channels4Balka">
    /// Имена каналов с напряжениями на балке.
    /// </param>
    /// <param name="factorsNames4Balka">
    /// Имена силовых факторов на балке.
    /// </param>
    /// <param name="matrix4Balka">
    /// Матрица для восстановления силовых факторов на балке (псевдообратная к матрице влияния).
    /// </param>
    /// <param name="channels4Rama">
    /// Имена каналов с напряжениями на раме.
    /// </param>
    /// <param name="factorsNames4Rama">
    /// Имена силовых факторов на раме.
    /// </param>
    /// <param name="matrix4Rama">
    /// Матрица для восстановления силовых факторов на раме (псевдообратная к матрице влияния).
    /// </param>
    /// <param name="path2Save">
    /// Путь для сохранения перезаписанного кадра.
    /// </param>
    public static void AddCurvatureFactors(string path2Cadr, string speedName, string latName, string lonName, string heightName,
                                           string[] channels4Balka, string[] factorsNames4Balka, 
                                           double[,] matrix4Balka,
                                           string[] channels4Rama, string[] factorsNames4Rama,
                                           double[,] matrix4Rama,
                                           string path2Save)                                           
    {
        // Открываем кадр:
        Frame cadr = new(path2Cadr);

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return; // Если GPS отвалилась, выходим в головную программу.
        }

        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] speed = cadr.Channels[speedName].Vector.ToArray();  // скорость, км/ч
        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(328.6, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, speed, lat, lon, height, 50);
        // Вычислили кривизну.

        // Формирование процесса "Признак"...

        //double[] priznak = new double[curvature.Length];
        double[] priznak = Arrays.ConstArray(10.0, curvature.Length);

        double currentCurvature;
        double currentRadius;
        for (int i = 0; i < curvature.Length; i++)
        {
            currentCurvature = curvature[i];

            if (currentCurvature > Double.Epsilon)
            {
                currentRadius = 1 / currentCurvature;

                if (currentRadius >= 1000)
                {
                    priznak[i] = 0.0;
                }

                if (600 <= currentRadius && currentRadius < 950)
                {
                    priznak[i] = 1.0;
                }

                if (300 <= currentRadius && currentRadius < 400)
                {
                    priznak[i] = 4.0;
                }
            }
            else
            {
                priznak[i] = 0.0;
            }
        }

        //...формирование процесса "Признак".

        // Восстанавливаем силовые факторы на балке...

        int nSonBalka = channels4Balka.Length; // Число напряжений на Балке.

        double[][] sB = new double[nSonBalka][]; // Напряжения для восстановления силовых факторов на Балке.
        for (int i = 0; i < nSonBalka; i++)
        {
            sB[i] = cadr.Channels[channels4Balka[i]].Vector.ToArray();
        }

        int nForcesOnBalka = factorsNames4Balka.Length;

        double[][] fB = new double[nForcesOnBalka][]; // Силовые факторы на Балке.
        
        int nTimes = sB[0].Length; // Число моментов времени.
                
        for (int i = 0; i < nForcesOnBalka; i++)
        {
            fB[i] = new double[nTimes];
        }
        
        double currentSum;        
        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < nForcesOnBalka; i++)
            {
                currentSum = 0.0;
                
                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < nSonBalka; j++)
                {
                    currentSum += matrix4Balka[i, j] * sB[j][t];                    
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)

                fB[i][t] = currentSum;   
            }
            // ...цикл по восстанавливаемым силовым факторам            
        }
        // ...цикл по временнЫм отсчётам.

        //...восстановили силовые факторы на балке.

        //---------------------------

        // Восстанавливаем силовые факторы на РАМЕ...

        int nSonRama = channels4Rama.Length; // Число напряжений на РАМЕ.

        double[][] sR = new double[nSonRama][]; // Напряжения для восстановления силовых факторов на РАМЕ.
        for (int i = 0; i < nSonRama; i++)
        {
            sR[i] = cadr.Channels[channels4Rama[i]].Vector.ToArray();
        }

        int nForcesOnRama = factorsNames4Rama.Length;

        double[][] fR = new double[nForcesOnRama][]; // Силовые факторы на РАМЕ.

        nTimes = sR[0].Length; // Число моментов времени.

        for (int i = 0; i < nForcesOnRama; i++)
        {
            fR[i] = new double[nTimes];
        }
                
        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < nForcesOnRama; i++)
            {
                currentSum = 0.0;

                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < nSonRama; j++)
                {
                    currentSum += matrix4Rama[i, j] * sR[j][t];
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)

                fR[i][t] = currentSum;
            }
            // ...цикл по восстанавливаемым силовым факторам            
        }
        // ...цикл по временнЫм отсчётам.

        //...восстановили силовые факторы на раме.

        // Формируем новые каналы и добавляем их в кадр:

        double smplRt = cadr.Channels[channels4Balka[0]].Sampling;
        double[] Curvature = Signals.ResamplingFrom1toHigher(curvature, smplRt);
        double[] Priznak = Signals.ResamplingFrom1toHigher(priznak, smplRt);

        Channel curvatureChannel = new(new(1, new(Curvature)))
        {
            Sampling = smplRt,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.

        Channel priznakChannel = new(new(1, new(Priznak)))
        {
            Sampling = smplRt,
            Unit = "",
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

        double srB = cadr.Channels[channels4Balka[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < nForcesOnBalka; i++)
        {
            Channel bChannel = new(new(1, new(fB[i])))
            {
                Sampling = srB,
                Unit = "kN",
                Name = factorsNames4Balka[i]
            };

            cadr.Channels.Add(bChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам.

        double srR = cadr.Channels[channels4Rama[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < nForcesOnRama; i++)
        {
            Channel rChannel = new(new(1, new(fR[i])))
            {
                Sampling = srR,
                Unit = "kN",
                Name = factorsNames4Rama[i]
            };

            cadr.Channels.Add(rChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам.

        cadr.Save(path2Save, cadr.Format);

        return;
    }


    /// <summary>
    /// Добавление в кадр кривизны и силовых факторов.
    /// </summary>
    /// <param name="cadr">
    /// Кадр.
    /// </param>
    /// <param name="matrix4Balka">
    /// Матрица для балки.
    /// </param>
    /// <param name="matrix4Rama">
    /// Матрица для рамы.
    /// </param>
    public static void AddCurvatureFactors(Frame cadr, double[,] matrix4Balka, double[,] matrix4Rama)
    {
        

        string speedName = "GPS_Speed";
        string latName = "GPS_Latitude";
        string lonName = "GPS_Longitude";
        string heightName = "GPS_Altitude";

        // Имена каналов с напряжениями на балке:
        string[] channels4Balka = { "Sb019", "Sb23", "Sb014", "Sb015", "Sb021'", "Sb09",
            "Sb011", "Sb013", "Sb05", "Sb025", "Sb25", "Sb8", "Sb13", "Sb22'",
            "Sb6", "Sb12", "Sb11", "Sb03", "Sb04", "Sb017", "Sb01", "Sb018", "Sb023",
            "Sb9", "Sb21'", "Sb17", "Sb14", "Sb19", "Sb15", "Sb3", "Sb5", "Sb18", "Sb2",
            "Sb1", "Sb4", "Sb022'", "Sb012", "Sb02" };

        // Имена восстанавливаемых сил на балке:
        string[] factorsNames4Balka = {"VertPin", "VertPinS1", "VertPinS2", "VertSlip1", "VertSlip2",
            "LongSide1", "LongSide2", "TransSide1", "TransSide2"};



        // Имена каналов с напряжениями на раме:
        string[] channels4Rama = { "Sr25", "Sr14", "Sr26", "Sr1", "Sr11", "Sr16", "Sr02", "Sr013",
            "Sr08", "Sr014", "Sr022", "Sr020", "Sr07", "Sr09", "Sr011",
            "Sr24", "Sr6", "Sr7", "Sr10", "Sr4", "Sr19", "Sr9", "Sr5", "Sr2", "Sr21", "Sr27",
            "Sr03", "Sr010", "Sr024", "Sr06", "Sr04", "Sr027", "Sr22", "Sr17", "Sr3",
            "Sr025", "Sr019", "Sr017", "Sr012", "Sr15", "Sr12", "Sr8", "Sr13", "Sr20", "Sr021",
            "Sr05", "Sr01", "Sr026" };

        // Имена восстанавливаемых сил на раме:
        string[] factorsNames4Rama = { "BuksProemPravVnutr", "Rog07", "Rog7", "Fy1S1", "Fy1S2",
                                           "PoperResProem14", "PoperResProem24", "PoperResProem34", "PoperResProem44",
                                           "VertResProem", "VertPlanki", "Vert+Poper1+2",
                                           "Poper1+2VnutrBuks", "Poper1+2", "Poper3+4VneshBuks",
                                           "Poper3+4VnutrBuks", "Poper3+4", "PopResPr24", "PopResPr34",
                                           "PopResPr44", "ProdBuksa+Res07", "ProdBuksa+Res7",
                                           "ProdResProem", "RasporBuksa07" };

  

        //...константы.

        if (Arrays.OnlyZeros(cadr.Channels[latName], cadr.Channels[lonName]))
        {
            return; // Если GPS отвалилась, выходим в головную программу.
        }

        int length = cadr.Channels[latName].Length;
        double sampleRate = cadr.Channels[latName].Sampling;

        double[] speed = cadr.Channels[speedName].Vector.ToArray();  // скорость, км/ч
        double[] lat = cadr.Channels[latName].Vector.ToArray();
        double[] lon = cadr.Channels[lonName].Vector.ToArray();
        double[] height;
        if (heightName.Length == 0)
        {
            height = Arrays.ConstArray(328.6, length);
        }
        else
        {
            height = cadr.Channels[heightName].Vector.ToArray();
        }

        // Вычисление кривизны:
        double[] curvature = Calculus.CurvatureGPS(sampleRate, speed, lat, lon, height, 50);
        // Вычислили кривизну.

        // Формирование процесса "Признак"...

        //double[] priznak = new double[curvature.Length];
        double[] priznak = Arrays.ConstArray(10.0, curvature.Length);

        double currentCurvature;
        double currentRadius;
        for (int i = 0; i < curvature.Length; i++)
        {
            currentCurvature = curvature[i];

            if (currentCurvature > Double.Epsilon)
            {
                currentRadius = 1 / currentCurvature;

                if (currentRadius >= 1000)
                {
                    priznak[i] = 0.0;
                }

                if (600 <= currentRadius && currentRadius < 950)
                {
                    priznak[i] = 1.0;
                }

                if (300 <= currentRadius && currentRadius < 400)
                {
                    priznak[i] = 4.0;
                }
            }
            else
            {
                priznak[i] = 0.0;
            }
        }

        //...формирование процесса "Признак".

        // Восстанавливаем силовые факторы на балке...

        int nSonBalka = channels4Balka.Length; // Число напряжений на Балке.

        double[][] sB = new double[nSonBalka][]; // Напряжения для восстановления силовых факторов на Балке.
        for (int i = 0; i < nSonBalka; i++)
        {
            sB[i] = cadr.Channels[channels4Balka[i]].Vector.ToArray();
        }

        int nForcesOnBalka = factorsNames4Balka.Length;

        double[][] fB = new double[nForcesOnBalka][]; // Силовые факторы на Балке.

        int nTimes = sB[0].Length; // Число моментов времени.

        for (int i = 0; i < nForcesOnBalka; i++)
        {
            fB[i] = new double[nTimes];
        }

        double currentSum;
        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < nForcesOnBalka; i++)
            {
                currentSum = 0.0;

                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < nSonBalka; j++)
                {
                    currentSum += matrix4Balka[i, j] * sB[j][t];
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)

                fB[i][t] = currentSum;
            }
            // ...цикл по восстанавливаемым силовым факторам            
        }
        // ...цикл по временнЫм отсчётам.

        //...восстановили силовые факторы на балке.

        //---------------------------

        // Восстанавливаем силовые факторы на РАМЕ...

        int nSonRama = channels4Rama.Length; // Число напряжений на РАМЕ.

        double[][] sR = new double[nSonRama][]; // Напряжения для восстановления силовых факторов на РАМЕ.
        for (int i = 0; i < nSonRama; i++)
        {
            sR[i] = cadr.Channels[channels4Rama[i]].Vector.ToArray();
        }

        int nForcesOnRama = factorsNames4Rama.Length;

        double[][] fR = new double[nForcesOnRama][]; // Силовые факторы на РАМЕ.

        nTimes = sR[0].Length; // Число моментов времени.

        for (int i = 0; i < nForcesOnRama; i++)
        {
            fR[i] = new double[nTimes];
        }

        // Цикл по временнЫм отсчётам...
        for (int t = 0; t < nTimes; t++)
        {
            // Цикл по восстанавливаемым силовым факторам...
            for (int i = 0; i < nForcesOnRama; i++)
            {
                currentSum = 0.0;

                // Цикл по напряжениям (суммирование в процессе матричного умножения)...
                for (int j = 0; j < nSonRama; j++)
                {
                    currentSum += matrix4Rama[i, j] * sR[j][t];
                }
                // ...цикл по напряжениям (суммирование в процессе матричного умножения)

                fR[i][t] = currentSum;
            }
            // ...цикл по восстанавливаемым силовым факторам            
        }
        // ...цикл по временнЫм отсчётам.

        //...восстановили силовые факторы на раме.

        // Формируем новые каналы и добавляем их в кадр:

        double smplRt = cadr.Channels[channels4Balka[0]].Sampling;
        double[] Curvature = Signals.ResamplingFrom1toHigher(curvature, smplRt);
        double[] Priznak = Signals.ResamplingFrom1toHigher(priznak, smplRt);

        Channel curvatureChannel = new(new(1, new(Curvature)))
        {
            Sampling = smplRt,
            Unit = "1/m",
            Name = "Curvature"
        };

        cadr.Channels.Add(curvatureChannel); // Добавили канал с кривизной.

        Channel priznakChannel = new(new(1, new(Priznak)))
        {
            Sampling = smplRt,
            Unit = "",
            Name = "Priznak"
        };

        cadr.Channels.Add(priznakChannel); // Добавили канал с признаком.

        double srB = cadr.Channels[channels4Balka[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < nForcesOnBalka; i++)
        {
            Channel bChannel = new(new(1, new(fB[i])))
            {
                Sampling = srB,
                Unit = "kN",
                Name = factorsNames4Balka[i]
            };

            cadr.Channels.Add(bChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам.

        double srR = cadr.Channels[channels4Rama[0]].Sampling;
        // Цикл по восстанавливаемым силовым факторам...
        for (int i = 0; i < nForcesOnRama; i++)
        {
            Channel rChannel = new(new(1, new(fR[i])))
            {
                Sampling = srR,
                Unit = "kN",
                Name = factorsNames4Rama[i]
            };

            cadr.Channels.Add(rChannel); // Добавили канал с силовым фактором.
        }
        // ...цикл по восстанавливаемым силовым факторам.

        //cadr.Save(path2Save);

        return;
    }

}

/*
            // Границы скоростных диапазонов:
            double[] speedBounds = new double[] { 0.0, 5.0, 10.0, 15.0, 20.0, 25.0, 30.0, 35.0, 40.0, 45.0, 50.0, 55.0, 60.0, 65.0, 70.0, 75.0, 80.0, 85.0, 90.0, 95.0, 100.0, 105.0, 110.0, 115.0, 120.0, 125.0};
            // Число скоростных диапазонов:
            int nSpeeds = speedBounds.Length - 1;

            // Граничные значения кривизны:
            double[] curvatureBounds = new double[] { 1.0 / 300.0, 1.0 / 600.0, 1.0 / 900.0, 1.0 / 3000.0 };
            int nTrackTypes = curvatureBounds.Length; // Число типов пути.

            // Значения процесса "Режимы" (движения):
            double[] regimeValues = new double[] { 0.0, 30.0, -50.0, 50.0 };

            double[,] trackLengths = new double[nSpeeds, nTrackTypes]; // Протяжённость участков по скоростям и типам пути.
            for (int i = 0; i < nSpeeds; i++)
            {
                for (int j = 0; j < nTrackTypes; j++)
                {
                    trackLengths[i, j] = 0.0;
                }
            }
            */

/*
double dUmin = 0.0; double dUmax = 0.0;
double dZmin = 0.0; double dZmax = 0.0;
double dXmin = 0.0; double dXmax = 0.0;
*/


/*
                    for (int i = 0; i < nSites; i++)
                    {
                        currentdU = dU[i];
                        currentdZ = dZ[i];
                        currentdX = dX[i];

                        if (currentdU < dUmin)
                        {
                            dUmin = currentdU;
                        }

                        if (currentdU > dUmax)
                        {
                            dUmax = currentdU;
                        }

                        if (currentdZ < dZmin)
                        {
                            dZmin = currentdZ;
                        }

                        if (currentdZ > dZmax)
                        {
                            dZmax = currentdZ;
                        }

                        if (currentdX < dXmin)
                        {
                            dXmin = currentdX;
                        }

                        if (currentdX > dXmax)
                        {
                            dXmax = currentdX;
                        }
                    }
                    */

/*
// Массив скоростей [м/с]:
double[] speed = cadr.Channels["V_GPS"].Vector;

// Массив значений кривизны [1/м]:
double[] curvature = cadr.Channels["Curvature"].Vector;

int length = speed.Length;

double s1; double s2;
double c1; double c2;

double v; double c;

for (int i = 0; i < nSpeeds - 1; i++)
{
    s1 = speedBounds[i]; s2 = speedBounds[i + 1];
    for (int j = 0; j < nTrackTypes; j++)
    {
        c1 = curvatureBounds[j]; c2 = curvatureBounds[j + 1];

        int[] ind = new int[length];
        int q = 0;
        for (int k = 0; k < length; k++)
        {
            v = speed[k]; c = curvature[k];
            if (v >= s1 && v < s2 && c > c2 && c <= c1)
            {
                ind[q] = k;
                q = q + 1;
            }
        }
        Array.Resize(ref ind, q);

        if (q > 0)
        {
            trackLengths[i, j] += 0.7; // !!!!!!!!!
        }
    }                        
}
*/