using System;
using System.Linq;

namespace Simargl.Mathematics
{
    /// <summary>
    /// Содержит функции, реализующие алгоритмы типа метода наименьших квадратов.
    /// </summary>
    public class LeastSquares
    {
        /// <summary>
        /// Окружность на плоскости по методу наименьших квадратов.
        /// </summary>
        /// <param name="x">
        /// Массив абсцисс точек.
        /// </param>
        /// <param name="y">
        /// Массив ординат точек.
        /// </param>
        /// <returns>
        /// [R,x0,y0] - радиус и координаты окружности.
        /// </returns>
        public static double[] Circle2D(double[] x, double[] y)
        {
            int n = x.Length;

            
            if (n != y.Length)
            {
                throw new Exception("Circle2D: Размеры массивов x- и y-координат должны совпадать!");
            }
            
            double[] x2 = Pointwise.Product(x, x);
            double[] y2 = Pointwise.Product(y, y);
            double[] x3 = Pointwise.Product(x2, x);
            double[] y3 = Pointwise.Product(y2, y);
            double[] xy = Pointwise.Product(x, y);

            double sx = x.Sum();
            double sy = y.Sum();
            double sx2 = x2.Sum();
            double sy2 = y2.Sum();
            double sxy = xy.Sum();
            double sx3 = x3.Sum();
            double sy3 = y3.Sum();
            double sxy2 = Pointwise.Product(x, y2).Sum();
            double sx2y = Pointwise.Product(x2, y).Sum();

            Matrix A = new(2, 2);

            A[0, 0] = 2 * (sx2 - sx * sx / n);
            A[0, 1] = 2 * (sxy - sx * sy / n);
            A[1, 0] = A[0, 1];
            A[1, 1] = 2 * (sy2 - sy * sy / n);

            Matrix w = new(2, 1);
            w[0, 0] = sx3 + sxy2 - (sx2 + sy2) * sx / n;
            w[1, 0] = sy3 + sx2y - (sx2 + sy2) * sy / n;

            Matrix c = Matrix.Inv2(A) * w;

            double x0 = c[0, 0];
            double y0 = c[1, 0];

            double R = Math.Sqrt(x0 * x0 + y0 * y0 + (sx2 + sy2 - 2 * (x0 * sx + y0 * sy)) / n);


            return new double[3] { R, x0, y0 };
        }        
    }
}
