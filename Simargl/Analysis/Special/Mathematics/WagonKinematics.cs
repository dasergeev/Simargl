using System;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, связанные с расчётами кинематики вагона.
/// </summary>
public static class WagonKinematics
{
    /// <summary>
    /// Вектор-функция, чей нуль ищем (разности длин).
    /// </summary>
    /// <param name="v">
    /// Тройка (du, dz, dx) = (угол поворота, координаты центра кузова).
    /// </param>
    /// <param name="Lc">
    /// Характерный масштаб, мм.
    /// </param>
    /// <param name="L">
    /// Длины гасителей в начальном состоянии, мм.
    /// </param>
    /// <param name="dL">
    /// Приращения длин гасителей (значения процессов "ходомеры"), мм.
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
    /// <returns>
    /// Значение вектор-функции: массив (r[0], r[1], r[2]).
    /// </returns>
    public static double[] LengthDifferencesFunction(double[] v, double Lc, double[] L, double[] dL, double[] zT, double[] zK, double[] xT, double[] xK)
    {
        double du = v[0]; double dz = v[1]; double dx = v[2];

        double ds2 = 0.5 * (dz * dz + dx * dx);

        double L1 = L[0]; double L2 = L[1]; double L3 = L[2];

        double dL1 = dL[0]; double dL2 = dL[1]; double dL3 = dL[2];

        double LL = Lc * Lc;

        double z1T = zT[0]; double z2T = zT[1]; double z3T = zT[2];
        double z1K = zK[0]; double z2K = zK[1]; double z3K = zK[2];

        double x1T = xT[0]; double x2T = xT[1]; double x3T = xT[2];
        double x1K = xK[0]; double x2K = xK[1]; double x3K = xK[2];

        double su = Math.Sin(du); double cu = Math.Cos(du); double cu1 = 1 - cu;

        double[] r = new double[3]; // Возвращаемое значение.

        r[0] = ds2 - (dz * z1T + dx * x1T) + cu * (dz * z1K + dx * x1K) + su * (dx * z1K - dz * x1K) + cu1 * (z1K * z1T + x1K * x1T) + su * (z1T * x1K - z1K * x1T) - L1 * dL1 / LL - 0.5 * dL1 * dL1 / LL; ;
        r[1] = ds2 - (dz * z2T + dx * x2T) + cu * (dz * z2K + dx * x2K) + su * (dx * z2K - dz * x2K) + cu1 * (z2K * z2T + x2K * x2T) + su * (z2T * x2K - z2K * x2T) - L2 * dL2 / LL - 0.5 * dL2 * dL2 / LL; ;
        r[2] = ds2 - (dz * z3T + dx * x3T) + cu * (dz * z3K + dx * x3K) + su * (dx * z3K - dz * x3K) + cu1 * (z3K * z3T + x3K * x3T) + su * (z3T * x3K - z3K * x3T) - L3 * dL3 / LL - 0.5 * dL3 * dL3 / LL;

        return r;
    }

    /// <summary>
    /// Матрица дифференциала вектор-функции LengthDifferencesFunction.
    /// </summary>
    /// <param name="v">
    /// Тройка (du, dz, dx) = (угол поворота, координаты центра кузова).
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
    /// <returns>
    /// Матрица дифференциала.
    /// </returns>
    public static double[,] DifferentialMatrix(double[] v, double[] zT, double[] zK, double[] xT, double[] xK)
    {
        double du = v[0]; double dz = v[1]; double dx = v[2];

        double z1T = zT[0]; double z2T = zT[1]; double z3T = zT[2];
        double z1K = zK[0]; double z2K = zK[1]; double z3K = zK[2];

        double x1T = xT[0]; double x2T = xT[1]; double x3T = xT[2];
        double x1K = xK[0]; double x2K = xK[1]; double x3K = xK[2];

        double su = Math.Sin(du); double cu = Math.Cos(du);

        double[,] J = new double[3, 3];

        J[0, 0] = su * ((z1T - dz) * z1K + (x1T - dx) * x1K) + cu * ((z1T - dz) * x1K + (dx - x1T) * z1K);
        J[0, 1] = dz - z1T + cu * z1K - su * x1K;
        J[0, 2] = dx - x1T + cu * x1K + su * z1K;

        J[1, 0] = su * ((z2T - dz) * z2K + (x2T - dx) * x2K) + cu * ((z2T - dz) * x2K + (dx - x2T) * z2K);
        J[1, 1] = dz - z2T + cu * z2K - su * x2K;
        J[1, 2] = dx - x2T + cu * x2K + su * z2K;

        J[2, 0] = su * ((z3T - dz) * z3K + (x3T - dx) * x3K) + cu * ((z3T - dz) * x3K + (dx - x3T) * z3K);
        J[2, 1] = dz - z3T + cu * z3K - su * x3K;
        J[2, 2] = dx - x3T + cu * x3K + su * z3K;

        return J;
    }

    /// <summary>
    /// Восстановление угла поворота и смещения центра кузова по показаниям ходомеров.
    /// </summary>
    /// <param name="dL">
    /// Приращения длин гасителей (значения процессов "ходомеры"), мм.
    /// </param>
    /// <param name="Lc">
    /// Характерный масштаб, мм.
    /// </param>
    /// <param name="L">
    /// Длины гасителей в начальном состоянии, мм.
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
    /// <returns>
    /// Три числа: угол поворота, z-смещение и x-смещение.
    /// </returns>
    public static double[] RotDisp(double[] dL, double Lc, double[] L, double[] zT, double[] zK, double[] xT, double[] xK, double[,] A0, double tolU, double tolL)
    {
        double[] uzx = new double[3]; // Возвращаемое значение.
            
        double LL = Lc * Lc;                       

        double[] s = new double[L.Length];
        for (int i = 0; i < L.Length; i++)
        {
            s[i] = ( L[i] * dL[i] + 0.5 * dL[i] * dL[i] ) / LL;
        }
                        
        double[] v = Arrays.MatrixVectorProduct(A0, s); // Начальное приближение.
                        
        bool run = true;

        double uPr; double zPr; double xPr;
        double uNext; double zNext; double xNext;
        double d; // Детерминант матрицы дифференциала.
        while (run)
        {
            uPr = v[0] * 180 / Math.PI; zPr = v[1] * Lc; xPr = v[2] * Lc; // Предыдущее приближение.

            double[] r = LengthDifferencesFunction(v, Lc, L, dL, zT, zK, xT, xK);

            double[,] J = DifferentialMatrix(v, zT, zK, xT, xK);
            d = Arrays.Det3(J); // Определитель матрицы дифференциала.

            if (d > Double.Epsilon)
            {                    
                v = Pointwise.Difference(v, Arrays.MatrixVectorProduct(Arrays.Inv3(J, d), r));
            }
            else
            {                    
                v = Pointwise.Sum(v, r);
            }

            uNext = v[0] * 180 / Math.PI; zNext = v[1] * Lc; xNext = v[2] * Lc; // Следующее приближение.

            if (Math.Abs(uNext - uPr) < tolU && Math.Abs(zNext - zPr) < tolL && Math.Abs(xNext - xPr) < tolL)
            {
                uzx[0] = uNext;
                uzx[1] = zNext;
                uzx[2] = xNext;

                run = false;
            }
        }

        return uzx;
    }
}
