using RailTest.Algebra;
using RailTest.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RailTest.Frames
{
    /// <summary>
    /// Представляет канал кадра регистрации.
    /// </summary>
    public class Channel : Ancestor
    {
        /// <summary>
        /// Поле для хранения вектора данных.
        /// </summary>
        private RealVector _Vector;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="values">
        /// Коллекция значений канала.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="values"/> передана пустая ссылка.
        /// </exception>
        public Channel(IEnumerable<double> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values), "Передана пустая ссылка.");
            }

            Header = new ChannelHeader();
            _Vector = new RealVector(values.ToArray());
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя канала.
        /// </param>
        /// <param name="unit">
        /// Единица измерения.
        /// </param>
        /// <param name="sampling">
        /// Частота дискретизации.
        /// </param>
        /// <param name="cutoff">
        /// Частота среза фильтра.
        /// </param>
        /// <param name="length">
        /// Длина массива данных.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если значение параметра <paramref name="sampling"/> меньше нуля
        /// - или -
        /// значение параметра <paramref name="length"/> меньше нуля.
        /// </exception>
        /// <exception cref="OutOfMemoryException">
        /// Происходит в случае, если недостаточно памяти для выполнения запроса.
        /// </exception>
        public Channel(string name, string unit, double sampling, double cutoff, int length)
        {
            Header = new ChannelHeader(name, unit, sampling, cutoff);

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Произошла попытка создать канал отрицательной длины.");
            }
            _Vector = new RealVector(length);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="header">
        /// Заголовок канала.
        /// </param>
        /// <param name="vector">
        /// Вектор данных.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Происходит в случае, если в параметре <paramref name="header"/> была передана пустая ссылка
        /// - или -
        /// если в параметре <paramref name="vector"/> была передана пустая ссылка.
        /// </exception>
        public Channel(ChannelHeader header, RealVector vector)
        {
            Header = header ?? throw new ArgumentNullException("header", "Передана пустая ссылка.");
            _Vector = vector ?? throw new ArgumentNullException("vector", "Передана пустая ссылка.");
        }

        /// <summary>
        /// Возвращает заголовок канала.
        /// </summary>
        public ChannelHeader Header { get; }

        /// <summary>
        /// Возвращает или задаёт вектор данных.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Происходит в случае, если в новом значении параметра была передана пустая ссылка.
        /// </exception>
        public RealVector Vector
        {
            get
            {
                return _Vector;
            }
            set
            {
                _Vector = value ?? throw new ArgumentNullException("value", "Передана пустая ссылка.");
            }
        }

        /// <summary>
        /// Возвращает формат кадра.
        /// </summary>
        public StorageFormat Format
        {
            get
            {
                return Header.Format;
            }
        }

        /// <summary>
        /// Возвращает или задаёт имя канала.
        /// </summary>
        public string Name
        {
            get
            {
                return Header.Name;
            }
            set
            {
                Header.Name = value ?? "";
            }
        }

        /// <summary>
        /// Возвращает или задаёт единицу измерения.
        /// </summary>
        public string Unit
        {
            get
            {
                return Header.Unit;
            }
            set
            {
                Header.Unit = value ?? "";
            }
        }

        /// <summary>
        /// Возвращает или задаёт частоту дискретизации.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если новое значение параметра меньше нуля.
        /// </exception>
        public double Sampling
        {
            get
            {
                return Header.Sampling;
            }
            set
            {
                Header.Sampling = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт частоту среза фильтра.
        /// </summary>
        public double Cutoff
        {
            get
            {
                return Header.Cutoff;
            }
            set
            {
                Header.Cutoff = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт длину массива данных.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если новое значение параметра отрицательное.
        /// </exception>
        /// <exception cref="OutOfMemoryException">
        /// Происходит в случае, если недостаточно памяти для выполнения запроса.
        /// </exception>
        public int Length
        {
            get
            {
                return Vector.Length;
            }
            set
            {
                Vector.Length = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт значение по указанному индексу.
        /// </summary>
        /// <param name="index">
        /// Индекс значения.
        /// </param>
        /// <returns>
        /// Значение.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если значение параметра <paramref name="index"/> меньше нуля
        /// - или -
        /// больше значения <see cref="Length"/>.
        /// </exception>
        public double this[int index]
        {
            get
            {
                return Vector[index];
            }
            set
            {
                Vector[index] = value;
            }
        }

        /// <summary>
        /// Возвращает значение, в указанное время.
        /// </summary>
        /// <param name="time">
        /// Время.
        /// </param>
        /// <returns>
        /// Значение.
        /// </returns>
        public double AtTimeSafe(double time)
        {
            int index = (int)(time * Sampling);
            if (index < 0)
            {
                index = 0;
            }
            if (index >= Length)
            {
                index = Length - 1;
            }
            return this[index];
        }

        /// <summary>
        /// Возвращает среднее значение канала.
        /// </summary>
        public double Average
        {
            get
            {
                return Vector.Average;
            }
        }

        /// <summary>
        /// Возвращает максимальное значение канала.
        /// </summary>
        public double Max
        {
            get
            {
                return Vector.Max;
            }
        }

        /// <summary>
        /// Возвращает минимальное значение канала.
        /// </summary>
        public double Min
        {
            get
            {
                return Vector.Min;
            }
        }

        /// <summary>
        /// Возвращает первую точку, в которой найдено максимальное значение канала.
        /// </summary>
        public ChannelPoint MaxPoint
        {
            get
            {
                int index = 0;
                double value = this[0];
                for (int i = 0; i != Length; ++i)
                {
                    if (value < this[i])
                    {
                        value = this[i];
                        index = i;
                    }
                }
                return new ChannelPoint(index, value);
            }
        }

        /// <summary>
        /// Возвращает первую точку, в которой найдено минимальное значение канала.
        /// </summary>
        public ChannelPoint MinPoint
        {
            get
            {
                int index = 0;
                double value = this[0];
                for (int i = 0; i != Length; ++i)
                {
                    if (value > this[i])
                    {
                        value = this[i];
                        index = i;
                    }
                }
                return new ChannelPoint(index, value);
            }
        }

        /// <summary>
        /// Возвращает стандартное отклонение.
        /// </summary>
        public double StandardDeviation
        {
            get
            {
                return Vector.StandardDeviation;
            }
        }

        /// <summary>
        /// Возвращает максимально вероятное значение выборки по теоретическому квантилю.
        /// </summary>
        /// <param name="probability">
        /// Вероятность.
        /// </param>
        /// <returns>
        /// Максимально вероятное значение.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        public double GetStandardMaxProbable(double probability)
        {
            return Vector.GetStandardMaxProbable(probability);
        }

        /// <summary>
        /// Возвращает минимально вероятное значение выборки по теоретическому квантилю.
        /// </summary>
        /// <param name="probability">
        /// Вероятность.
        /// </param>
        /// <returns>
        /// Максимально вероятное значение.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        public double GetStandardMinimumProbable(double probability)
        {
            return Vector.GetStandardMinProbable(probability);
        }

        /// <summary>
        /// Возвращает максимально вероятное значение выборки по эмпирическому квантилю.
        /// </summary>
        /// <param name="probability">
        /// Вероятность.
        /// </param>
        /// <returns>
        /// Максимально вероятное значение.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        public double GetEmpiricalMaxProbable(double probability)
        {
            return Vector.GetEmpiricalMaxProbable(probability);
        }

        /// <summary>
        /// Возвращает минимально вероятное значение выборки по эмпирическому квантилю.
        /// </summary>
        /// <param name="probability">
        /// Вероятность.
        /// </param>
        /// <returns>
        /// Максимально вероятное значение.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        public double GetEmpiricalMinProbable(double probability)
        {
            return Vector.GetEmpiricalMinProbable(probability);
        }

        ///// <summary>
        ///// Выполняет нормализацию по правилу трёх сигм.
        ///// </summary>
        ///// <exception cref="InvalidOperationException">
        ///// Происходит в случае, если неуправляемая библиотека выдала исключение.
        ///// </exception>
        //public void ThreeSigmaNormalization()
        //{
        //    Vector.ThreeSigmaNormalization();
        //}

        /// <summary>
        /// Масштабирует значения.
        /// </summary>
        /// <param name="factor">
        /// Масштабный множитель.
        /// </param>
        public void Scale(double factor)
        {
            Vector.Scale(factor);
        }

        /// <summary>
        /// Смещает значения.
        /// </summary>
        /// <param name="offset">
        /// Смещение.
        /// </param>
        public void Move(double offset)
        {
            Vector.Move(offset);
        }

        ///// <summary>
        ///// Выполняет циклический сдвиг.
        ///// </summary>
        ///// <param name="offset">
        ///// Смещение.
        ///// </param>
        //public void CyclicShift(int offset)
        //{
        //    Vector.CyclicShift(offset);
        //}

        ///// <summary>
        ///// Инвертирует канал.
        ///// </summary>
        //public void Invert()
        //{
        //    Vector.Invert();
        //}

        /// <summary>
        /// Смещает значения канала так, чтобы среднее значение
        /// за первые <paramref name="startTime"/> секунд было равно нулю.
        /// </summary>
        /// <param name="startTime">
        /// Время в секундах.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="startTime"/> передано отрицательное значение.
        /// </exception>
        unsafe public void SetZeroAtStart(double startTime)
        {
            if (startTime < 0)
            {
                throw new ArgumentOutOfRangeException("startTime", "Время для оценки смещения не может быть отрицательным.");
            }
            lock (SyncRoot)
            {
                if (Length != 0)
                {
                    int count = 1 + (int)(startTime * Sampling);
                    if (count > Length)
                    {
                        count = Length;
                    }
                    double* pointer = (double*)Vector.Pointer;
                    double mean = 0;
                    for (int i = 0; i != count; ++i)
                    {
                        mean += pointer[i];
                    }
                    mean /= count;
                    Vector.Move(-mean);
                }
            }
        }

        /// <summary>
        /// Представляет преобразователь амплитуд.
        /// </summary>
        /// <param name="frequency">
        /// Частота.
        /// </param>
        /// <param name="amplitude">
        /// Амплитуда.
        /// </param>
        /// <returns>
        /// Преобразованная амплитуда.
        /// </returns>
        public delegate Complex AmplitudeReformer(double frequency, Complex amplitude);

        /// <summary>
        /// Выполняет фильтрацию с помощью преобразования Фурье.
        /// </summary>
        /// <param name="reformer">
        /// Преобразователь амплитуд.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        unsafe public void FourierFiltering(AmplitudeReformer reformer)
        {
            RealSeries_FourierFilteringReformer((double*)_Vector.Pointer.ToPointer(), Length, Sampling, reformer);
        }

        /// <summary>
        /// Выполняет фильтрацию с помощью преобразования Фурье.
        /// </summary>
        /// <param name="lowerFrequency">
        /// Нижняя частота.
        /// </param>
        /// <param name="highFrequency">
        /// Верхняя частота.
        /// </param>
        /// <param name="isInversion">
        /// Значение, определяющее требуется ли выполнить инверсию частот:
        /// false - после фильтрации остаются частоты большие или равные <paramref name="lowerFrequency"/>,
        ///         но меньшие или равные <paramref name="highFrequency"/>
        /// - или -
        /// true -  после фильтрации остаются частоты меньшие <paramref name="lowerFrequency"/> или
        ///         большие <paramref name="highFrequency"/>.
        /// </param>
        unsafe public void FourierFiltering(double lowerFrequency, double highFrequency, bool isInversion = false)
        {
            RealSeries_FourierFiltering((double*)_Vector.Pointer.ToPointer(), Length, Sampling,
                lowerFrequency, highFrequency, isInversion);
        }

        /// <summary>
        /// Вычисляет скользящее среднее.
        /// </summary>
        /// <param name="weights">
        /// Массив, содержащий весовые коэффициенты для скользящего среднего.
        /// </param>
        /// <param name="weightsOffset">
        /// Смещение в массиве <paramref name="weights"/>,
        /// соответствующее центральному весовому коэффициенту.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Происходит в случае, если в параметре <paramref name="weights"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если в параметре <paramref name="weightsOffset"/> передано отрицательное значение
        /// - или -
        /// <paramref name="weightsOffset"/> >= <paramref name="weights.Length"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        unsafe public void MovingAverage(double[] weights, int weightsOffset = 0)
        {
            MovingAverage((double*)Vector.Pointer.ToPointer(), Length, weights, weightsOffset);
        }

        /// <summary>
        /// Изменяет частоту дискретизации.
        /// </summary>
        /// <param name="sampling">
        /// Новая частота дискретизации.
        /// </param>
        public void Resampling(double sampling)
        {
            int length = (int)Math.Floor(Length * sampling / Sampling);
            Resampling(sampling, length);
        }

        /// <summary>
        /// Изменяет частоту дискретизации.
        /// </summary>
        /// <param name="sampling">
        /// Новая частота дискретизации.
        /// </param>
        /// <param name="length">
        /// Новая длина канала.
        /// </param>
        unsafe public void Resampling(double sampling, int length)
        {
            RealVector vector = new RealVector(length);
            double* source = (double*)_Vector.Pointer.ToPointer();
            double* destination = (double*)vector.Pointer.ToPointer();

            for (int i = 0; i != length; ++i)
            {
                double time = i / sampling;
                int beginSourceIndex = (int)(Math.Round(time * Sampling));
                if (beginSourceIndex < 0)
                {
                    beginSourceIndex = 0;
                }
                if (beginSourceIndex > Length - 1)
                {
                    beginSourceIndex = Length - 1;
                }
                destination[i] = source[beginSourceIndex];
            }
            _Vector = vector;
            Sampling = sampling;
        }

        ///// <summary>
        ///// Определяет сдвиг относительно канала-шаблона.
        ///// </summary>
        ///// <param name="pattern">
        ///// Канал-шаблон.
        ///// </param>
        ///// <returns>
        ///// Сдвиг относительно канала-шаблона.
        ///// </returns>
        //public int GetShift(Channel pattern)
        //{
        //    return Vector.Shift(pattern.Vector);
        //}

        /// <summary>
        /// Возвращает подканал.
        /// </summary>
        /// <param name="index">
        /// Индекс, с которого начинается подканал.
        /// </param>
        /// <param name="length">
        /// Длина подканала.
        /// </param>
        /// <returns>
        /// Подканал.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если значение параметра <paramref name="index"/> меньше нуля
        /// - или -
        /// значение параметра <paramref name="length"/> меньше нуля
        /// - или -
        /// сумма значений параметров <paramref name="index"/> и <paramref name="length"/> больше свойства <see cref="Length"/>.
        /// </exception>
        public Channel GetSubChannel(int index, int length)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "Произошла попытка получить подканал отрицательного индекса.");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Произошла попытка получить подканал отрицательной длины.");
            }
            if (index + length > Length)
            {
                throw new ArgumentOutOfRangeException("index + length", "Произошла попытка получить подканал, который не умещается в канале.");
            }
            RealVector subVector = Vector.GetSubVector(index, length);
            return new Channel(Header.Clone(), subVector);
        }

        /// <summary>
        /// Создаёт копию канала.
        /// </summary>
        /// <returns>
        /// Копия канала.
        /// </returns>
        public Channel Clone()
        {
            return new Channel(Header.Clone(), Vector.Clone());
        }

        int Natural_TwoDegree(int argument)
        {
            if (argument <= 0) return 0;
            int result = 1;
            while (argument > result) result <<= 1;
            return result;
        }

        unsafe void RealSeries_ToComplex(Complex* target, double* source, int length)
        {
            for (int i = 0; i != length; ++i)
            {
                target[i] = new Complex(source[i], 0);
            }
        }

        unsafe void RealSeries_FourierFilteringReformer(double* series, int length, double sampling, AmplitudeReformer reformer)
        {
            Complex* target = null;
            try
            {
                int actualLength = Natural_TwoDegree(length);
                int halfLength = actualLength >> 1;
                target = (Complex*)MemoryManager.Alloc(actualLength * sizeof(Complex));
                RealSeries_ToComplex(target, series, length);
                ComplexSeries_FastFourierTransform(target, actualLength);
                double stepFrequency = sampling / (double)actualLength;
                *target = reformer(0, *target);
                for (int i = 1; i <= halfLength; ++i)
                {
                    double frequency = stepFrequency * (double)i;
                    *(target + i) = reformer(frequency, *(target + i));
                    *(target + (actualLength - i)) = reformer(frequency, *(target + (actualLength - i)));
                }

                ComplexSeries_FastInverseFourierTransform(target, actualLength);
                ComplexSeries_ToReal(series, target, length);
            }
            finally
            {
                MemoryManager.Free(new IntPtr(target));
            }
        }

        unsafe void ComplexSeries_FastInverseFourierTransform(Complex* series, int length)
        {
            Local_FastInverseFourierTransform(series, length);
        }

        unsafe void ComplexSeries_FastFourierTransform(Complex* series, int length)
        {
            Local_FastFourierTransform(series, length);
        }


        unsafe void ComplexSeries_ToReal(double* target, Complex* source, int length)
        {
            for (int i = 0; i != length; ++i)
            {
                target[i] = source[i].Real;
            }
        }

        unsafe static void Butterfly(Complex* x, Complex* y, Complex* w)
        {
            Complex p, q;
            p = *x;
            q = *y * (*w);
            *x = p + q;
            *y = p - q;
        }

        unsafe static void MassifButterfly(Complex* series, int size, Complex* w)
        {
            Complex power = new Complex(1.0, 0.0);
            int n = size >> 1;
            for (int i = 0; i != n; ++i)
            {
                Butterfly(series + i, series + i + n, &power);
                Complex oldPower = power;
                power = oldPower * (*w);
            }
        }

        static int Backwards(int x, int length)
        {
            int result = 0;
            int bit = 1;
            int reverse = 1 << (length - 1);
            for (int i = 0; i < length && x != 0; ++i)
            {
                if ((x & bit) != 0)
                {
                    result |= reverse;
                    x &= ~bit;
                }
                bit <<= 1;
                reverse >>= 1;
            }
            return result;
        }

        unsafe static void Reposition(Complex* series, int size)
        {
            int length = 0;
            while (1 << length < size) ++length;

            for (int i = 0; i != size; ++i)
            {
                int j = Backwards(i, length);
                if (i <= j)
                {
                    Complex temporal = series[j];
                    series[j] = series[i];
                    series[i] = temporal;
                }
            }
        }

        unsafe static void Conjugate(Complex* series, int size)
        {
            for (int i = 0; i != size; ++i)
            {
                series[i] = new Complex(series[i].Real, -series[i].Imaginary);
            }
        }

        unsafe void Local_FastFourierTransform(Complex* series, int length)
        {
            Complex root;
            {
                double angle = 6.283185307179586476925286766559 / (double)length;
                root = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            int stackIndex = 0;
            for (int step = length; step != 1; step >>= 1) ++stackIndex;

            Reposition(series, length);
            Complex* roots = null;
            try

            {
                roots = (Complex*)MemoryManager.Alloc(sizeof(Complex) * stackIndex);
                stackIndex = 0;
                for (int step = length; step != 1; step >>= 1)
                {
                    roots[stackIndex++] = root;
                    root *= root;
                }
                for (int step = 2; step <= length; step <<= 1)
                {
                    root = roots[--stackIndex];
                    for (int i = 0; i != length; i += step) MassifButterfly(series + i, step, &root);
                }
            }
            finally

            {
                MemoryManager.Free(new IntPtr(roots));
            }
        }

        unsafe void Local_FastInverseFourierTransform(Complex* series, int length)
        {
            Conjugate(series, length);
            Local_FastFourierTransform(series, length);
            Conjugate(series, length);
            double factor = 1.0 / (double)length;
            for (int i = 0; i != length; ++i)
            {
                series[i] *= factor;
            }
        }


        unsafe void RealSeries_FourierFiltering(double* series, int length, double sampling, double lowerFrequency, double highFrequency, bool isInversion)
        {
            Complex* target = null;
            try

            {
                int actualLength = Natural_TwoDegree(length);
                int halfLength = actualLength >> 1;
                target = (Complex*)MemoryManager.Alloc(actualLength * sizeof(Complex));
                RealSeries_ToComplex(target, series, length);
                ComplexSeries_FastFourierTransform(target, actualLength);
                double stepFrequency = sampling / (double)actualLength;

                try

                {
                    if (isInversion)
                    {
                        if (lowerFrequency <= 0)
                        {
                            target[0] = Complex.Zero;
                        }

                        for (int i = 1; i <= halfLength; ++i)
                        {
                            double frequency = stepFrequency * (double)i;
                            if (frequency >= lowerFrequency && frequency <= highFrequency)
                            {
                                target[i] = Complex.Zero;
                                target[actualLength - i] = Complex.Zero;
                            }
                        }
                    }
                    else
                    {
                        if (lowerFrequency > 0)
                        {
                            target[0] = Complex.Zero;
                        }
                        for (int i = 1; i <= halfLength; ++i)
                        {
                            double frequency = stepFrequency * (double)i;
                            if (frequency < lowerFrequency || frequency > highFrequency)
                            {
                                target[i] = Complex.Zero;
                                target[actualLength - i] = Complex.Zero;
                            }
                        }
                    }
                }
                catch
                {

                }

                ComplexSeries_FastInverseFourierTransform(target, actualLength);
                ComplexSeries_ToReal(series, target, length);
            }
            finally

            {
                MemoryManager.Free(new IntPtr(target));
            }
        }

        /// <summary>
        /// Вычисляет скользящее среднее.
        /// </summary>
        /// <param name="series">
        /// Указатель на область памяти, в котором содержится последовательность.
        /// Размер области памяти должен быть достаточно большим,
        /// чтобы уместить <paramref name="length"/> значений типа <see cref="double"/>.
        /// </param>
        /// <param name="length">
        /// Количество элементов типа <see cref="double"/> в последовательности <paramref name="series"/>.
        /// </param>
        /// <param name="weights">
        /// Массив, содержащий весовые коэффициенты для скользящего среднего.
        /// </param>
        /// <param name="weightsOffset">
        /// Смещение в массиве <paramref name="weights"/>,
        /// соответствующее центральному весовому коэффициенту.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Происходит в случае, если в параметре <paramref name="series"/> передан нулевой указатель
        /// - или -
        /// в параметре <paramref name="weights"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если в параметре <paramref name="length"/> передано отрицательное значение
        /// - или -
        /// в параметре <paramref name="weightsOffset"/> передано отрицательное значение
        /// - или -
        /// <paramref name="weightsOffset"/> >= <paramref name="weights.Length"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        unsafe static void MovingAverage(double* series, int length, double[] weights, int weightsOffset)
        {
            if (length == 0)
            {
                return;
            }
            if (weights == null)
            {
                throw new ArgumentNullException("weights", "Передана пустая ссылка.");
            }
            if (weights.Length == 0)
            {
                MovingAverage(series, series, length, null, 0, 0);
            }
            else
            {
                fixed (double* weightsPointer = weights)
                {
                    MovingAverage(series, series, length, weightsPointer, weights.Length, weightsOffset);
                }
            }
        }

        /// <summary>
        /// Вычисляет скользящее среднее.
        /// </summary>
        /// <param name="targetSeries">
        /// Целевой указатель на область памяти, в которую необходимо поместить результат вычислений.
        /// Размер области памяти должен быть достаточно большим,
        /// чтобы уместить <paramref name="length"/> значений типа <see cref="double"/>.
        /// Значение может совпадать с <paramref name="sourceSeries"/>.
        /// </param>
        /// <param name="sourceSeries">
        /// Исходный указатель на область памяти, из которой необходимо взять исходные данные.
        /// Размер области памяти должен быть достаточно большим,
        /// чтобы уместить <paramref name="length"/> значений типа <see cref="double"/>.
        /// Значение может совпадать с <paramref name="targetSeries"/>.
        /// </param>
        /// <param name="length">
        /// Количество элементов типа <see cref="double"/>
        /// в последовательностях <paramref name="targetSeries"/> и <paramref name="sourceSeries"/>.
        /// </param>
        /// <param name="weights">
        /// Указатель на область памяти, содержащую весовые коэффициенты для скользящего среднего.
        /// Размер области памяти должен быть достаточно большим,
        /// чтобы уместить <paramref name="weightsCount"/> значений типа <see cref="double"/>.
        /// </param>
        /// <param name="weightsCount">
        /// Количество элементов типа <see cref="double"/> в массиве <paramref name="weights"/>.
        /// </param>
        /// <param name="weightsOffset">
        /// Смещение в массиве <paramref name="weights"/>,
        /// соответствующее центральному весовому коэффициенту.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Происходит в случае, если в параметре <paramref name="targetSeries"/> передан нулевой указатель
        /// - или -
        /// в параметре <paramref name="sourceSeries"/> передан нулевой указатель
        /// - или -
        /// в параметре <paramref name="weights"/> передан нулевой указатель
        /// и значение <paramref name="weightsCount"/> больше нуля.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Происходит в случае, если <paramref name="weightsOffset"/> >= <paramref name="weightsCount"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Происходит в случае, если неуправляемая библиотека выдала исключение.
        /// </exception>
        unsafe static void MovingAverage(double* targetSeries, double* sourceSeries, int length,
            double* weights, int weightsCount, int weightsOffset)
        {
            if (length == 0)
            {
                return;
            }
            if (targetSeries == null)
            {
                throw new ArgumentNullException("targetSeries", "Передан нулевой указатель.");
            }
            if (sourceSeries == null)
            {
                throw new ArgumentNullException("sourceSeries", "Передан нулевой указатель.");
            }
            if (weights == null && weightsCount != 0)
            {
                throw new ArgumentNullException("weights", "Передан нулевой указатель.");
            }
            if (weightsOffset >= weightsCount)
            {
                throw new ArgumentOutOfRangeException("weightsOffset", "Смещение должно быть меньше количества весов.");
            }
            RealSeries_MovingAverage(
                    targetSeries, sourceSeries, length, weights, weightsCount, weightsOffset);
        }

        //	Вычисляет скользящее среднее.
        //		target - целевая последовательность.
        //		source - исходная последовательность.
        //		length - длина последовательностей.
        //		weights - весовые коэффициенты.
        //		weightsCount - количество весовых коэффициентов.
        //		weightsOffset - смещение весовых коэффициентов.
        //	Ограничения:
        //		weightsOffset < weightsCount
        unsafe static void RealSeries_MovingAverage(
            double* target, double* source, int length,
            double* weights, int weightsCount, int weightsOffset)
        {
            if (weightsCount == 0)
            {
                MemoryManager.Copy(new IntPtr(target), new IntPtr(source), length * sizeof(double));
                return;
            }
            if (target == source)
            {
                double* duplicate = null;
                try

                {
                    duplicate = (double*)MemoryManager.Alloc(length * sizeof(double));
                    MemoryManager.Copy(new IntPtr(duplicate), new IntPtr(source), length * sizeof(double));
                    RealSeries_MovingAverage(target, duplicate, length, weights, weightsCount, weightsOffset);
                    return;
                }
                finally

                {
                    MemoryManager.Free(new IntPtr(duplicate));
                }
            }
            for (int targetIndex = 0; targetIndex != length; ++targetIndex)
            {
                double targetValue = 0;
                for (int weightsIndex = 0; weightsIndex != weightsCount; ++weightsIndex)
                {
                    int sourceIndex = targetIndex + weightsIndex;
                    if (sourceIndex >= weightsOffset)
                    {
                        sourceIndex -= weightsOffset;
                        if (sourceIndex < length)
                        {
                            targetValue += weights[weightsIndex] * source[sourceIndex];
                        }
                        else
                        {
                            targetValue += weights[weightsIndex] * source[length - 1];
                        }
                    }
                    else
                    {
                        targetValue += weights[weightsIndex] * source[0];
                    }
                }
                target[targetIndex] = targetValue;
            }
        }
    }
}
