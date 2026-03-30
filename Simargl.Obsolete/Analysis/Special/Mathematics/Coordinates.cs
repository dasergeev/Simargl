using System;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, реализующие преобразования координат.
/// </summary>
class Coordinates
{
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
