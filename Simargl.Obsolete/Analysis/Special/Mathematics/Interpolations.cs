namespace Simargl.Mathematics
{
    /// <summary>
    /// Содержит статические функции, реализующие алгоритмы интерполяции.
    /// </summary>
    public class Interpolations
    {
        /// <summary>
        /// Линейная интерполяция. Все входные массивы предполагаются упорядоченными по возрастанию.
        /// </summary>
        /// <param name="x">
        /// Массив абсцисс.
        /// </param>
        /// <param name="y">
        /// Массив ординат.
        /// </param>
        /// <param name="xNew">
        /// Массив абсцисс, для которого надо получить массив значений посредством линейной интерполяции.
        /// </param>
        /// <returns>
        /// Массив значений, полученных линейной интерполяцией.
        /// </returns>
        public static double[] Linear(double[] x, double[] y, double[] xNew)
        {
            double[] yNew= new double[xNew.Length]; // Возвращаемый массив.

            int nPoints = x.Length;
            int nKnots = xNew.Length;

            double knot = xNew[0];
            int ic = 0; // Текущий индекс узла knot в массиве xNew.

            double x0 = x[0]; double xLast = x[nPoints - 1];
            double y0 = y[0]; double yLast = y[nPoints - 1];

            bool run = true;

            while (run)
            {
                if (knot < x0)
                {
                    yNew[ic] = y0;
                    ic += 1;

                    if (ic < nKnots)
                    {
                        knot = xNew[ic];
                    }                    
                }
                else
                {
                    run = false;
                }
            }

            if (ic >= nKnots)
            {
                return yNew;
            }

            // Закончили 1-й этап.

            int iCurrent = 0; // Текущий индекс элемента в массиве x.
            double xCurrent; // Текущий элемент в массиве x.

            int iL; int iR;
            double xL; double yL; double xR; double yR;
            double slope;

            for (int k = ic; k < nKnots; k++)
            {
                knot = xNew[k];
                run = true;

                while (run)
                {
                    xCurrent = x[iCurrent];

                    if ( (xCurrent <= knot) && (iCurrent < nPoints - 1) )
                    {
                        iCurrent += 1;
                    }
                    else
                    {
                        run = false;
                    }
                }

                iL = iCurrent - 1; iR = iCurrent;

                if ( (iR < nPoints) && (knot < xLast) )
                {
                    xL = x[iL]; xR = x[iR];
                    yL = y[iL]; yR = y[iR];

                    // Наклон прямой, проходящей через (xL,yL) и (xR,yR):
                    slope = (yR - yL) / (xR - xL);

                    yNew[k] = slope * (knot - xL) + yL;
                }
                else
                {
                    yNew[k] = yLast;
                }

            }

            return yNew;
        }
    }
}
