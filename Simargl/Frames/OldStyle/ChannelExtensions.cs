using Simargl.Algebra;
using Simargl.Analysis;
using Simargl.Analysis.Transforms;
using System.Linq;

using Complex = System.Numerics.Complex;
using Simargl.Designing;

namespace Simargl.Frames.OldStyle;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="Channel"/>,
/// реализующие методы старого стиля работы с кадрами.
/// </summary>
public static class ChannelExtensions
{
    /// <summary>
    /// Evaluates polynomial of degree N
    /// </summary>
    /// <param name="x"></param>
    /// <param name="coef"></param>
    /// <param name="N"></param>
    /// <returns></returns>
    private static double Polevl(double x, double[] coef, int N)
    {
        double ans;

        ans = coef[0];

        for (int i = 1; i <= N; i++)
        {
            ans = ans * x + coef[i];
        }

        return ans;
    }

    /// <summary>
    /// Evaluates polynomial of degree N with assumtion that coef[N] = 1.0
    /// </summary>
    /// <param name="x"></param>
    /// <param name="coef"></param>
    /// <param name="N"></param>
    /// <returns></returns>		
    private static double P1evl(double x, double[] coef, int N)
    {
        double ans;

        ans = x + coef[0];

        for (int i = 1; i < N; i++)
        {
            ans = ans * x + coef[i];
        }

        return ans;
    }

    /// <summary>
    /// Returns the error function of the specified number.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double Erf(double x)
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
						 //1.00000000000000000000E0,
						 3.35617141647503099647E1,
                         5.21357949780152679795E2,
                         4.59432382970980127987E3,
                         2.26290000613890934246E4,
                         4.92673942608635921086E4
                     };

        if (Math.Abs(x) > 1.0) return 1.0 - Erfc(x);
        z = x * x;
        y = x * Polevl(z, T, 4) / P1evl(z, U, 5);
        return y;
    }

    private const double MAXLOG = 7.09782712893383996732E2;


    /// <summary>
    /// Returns the complementary error function of the specified number.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static double Erfc(double a)
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
						 //1.0
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
						 //1.00000000000000000000E0, 
						 2.26052863220117276590E0,
                         9.39603524938001434673E0,
                         1.20489539808096656605E1,
                         1.70814450747565897222E1,
                         9.60896809063285878198E0,
                         3.36907645100081516050E0
                     };

        if (a < 0.0) x = -a;
        else x = a;

        if (x < 1.0) return 1.0 - Erf(a);

        z = -a * a;

        if (z < -MAXLOG)
        {
            if (a < 0) return 2.0;
            else return 0.0;
        }

        z = Math.Exp(z);

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

        y = z * p / q;

        if (a < 0) y = 2.0 - y;

        if (y == 0.0)
        {
            if (a < 0) return 2.0;
            else return 0.0;
        }


        return y;
    }


    private static double Real_ErrorFunction(double argument)
    {
        return Erf(argument);
    }

    private static double Real_InverseErrorFunction(double argument)
    {
        const double epsilon = 2 * double.Epsilon;
        const double maxArgument = 0.5 * double.MaxValue;
        if (argument < 0) return -Real_InverseErrorFunction(-argument);

        double leftArgument = 0;
        double rightArgument = maxArgument;
        double centerArgument = 0.5 * (leftArgument + rightArgument);
        double leftValue = Real_ErrorFunction(leftArgument) - argument;
        double rightValue = Real_ErrorFunction(rightArgument) - argument;
        double centerValue = Real_ErrorFunction(centerArgument) - argument;

        if (rightValue < 0) return rightArgument;
        while (centerValue > epsilon || centerValue < -epsilon)
        {
            if (leftValue < 0 && centerValue < 0)
            {
                leftArgument = centerArgument;
                leftValue = Real_ErrorFunction(leftArgument) - argument;
            }
            else
            {
                rightArgument = centerArgument;
                //rightValue = Real_ErrorFunction(rightArgument) - argument;
            }
            centerArgument = 0.5 * (leftArgument + rightArgument);
            centerValue = Real_ErrorFunction(centerArgument) - argument;
        }
        return centerArgument;
    }

    private static double Real_StandardNormalQuantile(double argument)
    {
        return 1.4142135623730950488016887242097 * Real_InverseErrorFunction(2 * argument - 1);
    }

    /// <summary>
    /// Добавляет значение в конец канала.
    /// </summary>
    /// <param name="channel">
    /// Канал, в который необходимо добавить значение.
    /// </param>
    /// <param name="value">
    /// Значение, добавляемое в конец канала.
    /// </param>
    public static void Add(this Channel channel, double value)
    {
        //  Изменение длины канала.
        channel.Length++;

        //  Установка последнего значения.
        channel[^1] = value;
    }

    /// <summary>
    /// Возвращает минимально вероятное значение.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <param name="type">
    /// Значение, определяющее тип закона распределения.
    /// </param>
    /// <returns>
    /// Минимально вероятное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> меньше или равно нулю
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Значение <paramref name="type"/> не содержится в перечислении <see cref="ProbabilityType"/>.
    /// </exception>
    public static double GetMinProbable(this Channel channel, double probability, ProbabilityType type)
    {
        if (probability <= 0 || probability >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(probability), "Вероятность должны быть больше нуля и меньше единицы.");
        }
        switch (type)
        {
            case ProbabilityType.Standard:
                {
                    double quantile = Real_StandardNormalQuantile(probability);
                    double mean = channel.Average();
                    double deviation = channel.StandardDeviation();
                    return mean - quantile * deviation;
                }
            case ProbabilityType.Empirical:
                return channel.GetMaxProbable(1 - probability, ProbabilityType.Empirical);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), "Значение не содержится в перечислении.");
        }
    }

    /// <summary>
    /// Возвращает максимально вероятное значение.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <param name="type">
    /// Значение, определяющее тип закона распределения.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> меньше или равно нулю
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Значение <paramref name="type"/> не содержится в перечислении <see cref="ProbabilityType"/>.
    /// </exception>
    public static double GetMaxProbable(this Channel channel, double probability, ProbabilityType type)
    {
        if (probability <= 0 || probability >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(probability), "Вероятность должны быть больше нуля и меньше единицы.");
        }
        switch (type)
        {
            case ProbabilityType.Standard:
                {
                    double quantile = Real_StandardNormalQuantile(probability);
                    double mean = channel.Average();
                    double deviation = channel.StandardDeviation();
                    return mean + quantile * deviation;
                }
            case ProbabilityType.Empirical:
                {
                    int length = channel.Length;
                    double[] duplicate = channel.ToArray();
                    Array.Sort(duplicate);
                    int index = (length * probability > 1) ? (int)((length * probability) - 1) : 0;
                    if (index >= length) index = length - 1;
                    double result = duplicate[index];
                    return result;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(type), "Значение не содержится в перечислении.");
        }
    }

    /// <summary>
    /// Возвращает максимально вероятное значение выборки по теоретическому квантилю.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static double GetStandardMaxProbable(this Channel channel, double probability)
    {
        return channel.GetMaxProbable(probability, ProbabilityType.Standard);
    }

    /// <summary>
    /// Возвращает минимально вероятное значение выборки по теоретическому квантилю.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static double GetStandardMinimumProbable(this Channel channel, double probability)
    {
        return channel.GetMinProbable(probability, ProbabilityType.Standard);
    }

    /// <summary>
    /// Возвращает максимально вероятное значение выборки по эмпирическому квантилю.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static double GetEmpiricalMaxProbable(this Channel channel, double probability)
    {
        return channel.GetMaxProbable(probability, ProbabilityType.Empirical);
    }

    /// <summary>
    /// Возвращает минимально вероятное значение выборки по эмпирическому квантилю.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static double GetEmpiricalMinProbable(this Channel channel, double probability)
    {
        return channel.GetMinProbable(probability, ProbabilityType.Empirical);
    }

    /// <summary>
    /// Возвращает СКО.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если длина канала меньше 2.
    /// </exception>
    public static double StandardDeviation(this Channel channel)
    {
        int length = channel.Length;
        if (length < 2)
        {
            throw new InvalidOperationException("Нельзя определить СКО вектора длины меньшей 2.");
        }
        double mean = channel.Average();
        double result = 0;
        for (int i = 0; i != length; ++i)
        {
            double value = channel[i] - mean;
            result += value * value;
        }
        return Math.Sqrt(result / (length - 1));
    }

    /// <summary>
    /// Выполняет фильтрацию с помощью преобразования Фурье.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="reformer">
    /// Преобразователь амплитуд.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static void FourierFiltering(this Channel channel, SpectrumReformer reformer)
    {
        CustomFilter transform = new(reformer);
        transform.Invoke(channel.Signal, channel.Signal);
    }

    /// <summary>
    /// Выполняет фильтрацию с помощью преобразования Фурье.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
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
    public static void FourierFiltering(this Channel channel, double lowerFrequency, double highFrequency, bool isInversion = false)
    {
        CustomFilter transform;
        if (isInversion)
        {
            transform = new((double frequency, Complex amplitude) =>
            {
                if (frequency < lowerFrequency || frequency > highFrequency)
                {
                    return amplitude;
                }
                else
                {
                    return 0;
                }
            });
        }
        else
        {
            transform = new((double frequency, Complex amplitude) =>
            {
                if (lowerFrequency <= frequency && frequency <= highFrequency)
                {
                    return amplitude;
                }
                else
                {
                    return 0;
                }
            });
        }
        transform.Invoke(channel.Signal, channel.Signal);
    }

    /// <summary>
    /// Возвращает подканал.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
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
    /// сумма значений параметров <paramref name="index"/> и <paramref name="length"/> больше свойства длины канала.
    /// </exception>
    public static Channel GetSubChannel(this Channel channel, int index, int length)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Произошла попытка получить подканал отрицательного индекса.");
        }
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Произошла попытка получить подканал отрицательной длины.");
        }
        if (index + length > channel.Length)
        {
            throw new ArgumentOutOfRangeException("index + length", "Произошла попытка получить подканал, который не умещается в канале.");
        }
        //  Создание копии канала.
        channel = channel.Clone();

        //  Перемещение значений.
        for (int i = 0; i < length; i++)
        {
            channel[i] = channel[index + i];
        }

        //  Обрезка канала.
        channel.Length = length;

        //  Возврат подканала.
        return channel;
    }

    /// <summary>
    /// Масштабирует значения.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="factor">
    /// Масштабный множитель.
    /// </param>
    public static void Scale(this Channel channel, double factor)
    {
        channel.Vector.Scale(factor);
    }

    /// <summary>
    /// Смещает значения.
    /// </summary>
    /// <param name="channel">
    /// Канал.
    /// </param>
    /// <param name="offset">
    /// Смещение.
    /// </param>
    public static void Move(this Channel channel, double offset)
    {
        channel.Vector.Move(offset);
    }

    /// <summary>
    /// Изменяет частоту дискретизации.
    /// </summary>
    /// <param name="channel">
    /// Канал, для которого необходимо изменить частоту дискретизации.
    /// </param>
    /// <param name="sampling">
    /// Новая частота дискретизации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channel"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/>  передано нулевое значение.
    /// </exception>
    public static void Resampling(this Channel channel, double sampling)
    {
        //  Проверка ссылки на канал.
        IsNotNull(channel, nameof(channel));

        //  Проверка частоты дискретизации.
        IsPositive(sampling, nameof(sampling));

        int length = (int)Math.Floor(channel.Length * sampling / channel.Sampling);
        Resampling(channel, sampling, length);
    }

    /// <summary>
    /// Изменяет частоту дискретизации.
    /// </summary>
    /// <param name="channel">
    /// Канал, для которого необходимо изменить частоту дискретизации.
    /// </param>
    /// <param name="sampling">
    /// Новая частота дискретизации.
    /// </param>
    /// <param name="length">
    /// Новая длина канала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channel"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/>  передано нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    public static void Resampling(this Channel channel, double sampling, int length)
    {
        //  Проверка ссылки на канал.
        IsNotNull(channel, nameof(channel));

        //  Проверка частоты дискретизации.
        IsPositive(sampling, nameof(sampling));

        //  Проверка длины канала.
        IsNotNegative(length, nameof(length));

        //  Проверка нулевой длины.
        if (length == 0)
        {
            //  Изменение свойств текущего канала.
            channel.Length = length;
            channel.Sampling = sampling;

            //  Завершение работы.
            return;
        }

        //  Создание копии вектора канала.
        Vector<double> source = channel.Vector.Clone();

        //  Сохранение исходной частоты дискретизации.
        double sourceSampling = channel.Sampling;

        //  Изменение свойств текущего канала.
        channel.Length = length;
        channel.Sampling = sampling;

        for (int i = 0; i != length; ++i)
        {
            double time = i / sampling;
            int sourceIndex = (int)Math.Round(time * sourceSampling);
            if (sourceIndex < 0)
            {
                sourceIndex = 0;
            }
            if (sourceIndex > source.Length - 1)
            {
                sourceIndex = source.Length - 1;
            }
            channel[i] = source[sourceIndex];
        }
    }
}
