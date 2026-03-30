using RailTest.Algebra;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет пик.
    /// </summary>
    public class Peak
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="centralIndex">
        /// Центральный индекс пика.
        /// </param>
        /// <param name="rail">
        /// Рельс.
        /// </param>
        public Peak(int centralIndex, RailGroup rail)
        {
            CentralIndex = centralIndex;
            Rail = rail;
            Time = CentralIndex / rail.Continuous.Sampling;
            Factors = new ComplexVector(1024);
            Source = new RealVector(3072);
        }

        /// <summary>
        /// Возвращает центаральный индекс пика.
        /// </summary>
        public int CentralIndex { get; }

        /// <summary>
        /// Возвращает рельс.
        /// </summary>
        public RailGroup Rail { get; }

        /// <summary>
        /// Возвращает время, когда колесо находилось в центре сечений.
        /// </summary>
        public double Time { get; }

        /// <summary>
        /// Возвращает или задаёт скорость.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Возвращает или задаёт нагрузку.
        /// </summary>
        public double Load { get; set; }

        /// <summary>
        /// Возвращает исходные данные.
        /// </summary>
        public RealVector Source { get; }

        /// <summary>
        /// Возвращает комплексные коэффициенты.
        /// </summary>
        public ComplexVector Factors { get; }

        /// <summary>
        /// Возвращает масштаб по оси абсцисс.
        /// </summary>
        public double Scale { get; private set; }

        /// <summary>
        /// Возвращает среднее значение схемы.
        /// </summary>
        public double Average { get; private set; }

        /// <summary>
        /// Выполняет нормализацию пиков.
        /// </summary>
        /// <param name="range">
        /// Область пика.
        /// </param>
        public void Normalization(double range)
        {
            Scale = 0.5 * range / 512;
            Average = 0;
            for (int i = 0; i != 256; ++i)
            {
                double x = Scale * (i - 128);
                int index = CentralIndex + (int)(Rail.Continuous.Sampling * x / Speed);
                Average += Rail.Continuous[index] / Load;
            }
            Average /= 256;

            for (int i = 0; i != 1024; ++i)
            {
                double x = Scale * (i - 512);
                int index = CentralIndex + (int)(Rail.Continuous.Sampling * x / Speed);
                Factors[i] = Rail.Continuous[index] / Load;
            }
            Factors.FourierTransform();
            for (int i = 0; i != 3072; ++i)
            {
                double x = Scale * (i - 1536);
                int index = CentralIndex + (int)(Rail.Continuous.Sampling * x / Speed);
                Source[i] = Rail.Continuous[index] / Load;
                Rail.Standard[index] = Source[i];
            }
        }
    }
}
