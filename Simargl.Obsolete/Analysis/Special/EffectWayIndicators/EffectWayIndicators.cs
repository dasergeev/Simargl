using Simargl.Frames;
using Simargl.Frames.OldStyle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Analysis.EffectWayIndicators;

/// <summary>
/// Предоставляет методы для расчёта показателей воздействия на путь.
/// </summary>
public static class EffectWayIndicators
{
    /// <summary>
    /// Определяет среднее.
    /// </summary>
    /// <param name="list">
    /// Список значений.
    /// </param>
    /// <returns>
    /// Среднее.
    /// </returns>
    public static double Average(List<double> list)
    {
        double mean = 0;
        if (list.Count !=0)
        {
            mean = list.Average();
        }
        return mean;
    }

    /// <summary>
    /// Определяет максимальное.
    /// </summary>
    /// <param name="list">
    /// Список значений.
    /// </param>
    /// <returns>
    /// Максимальное.
    /// </returns>
    public static double Max(List<double> list)
    {
        double max = 0;
        if (list.Count != 0)
        {
            max = list.Max();
        }
        return max;
    }

    /// <summary>
    /// Определяет среднее квадратическое отклонение.
    /// </summary>
    /// <param name="list">
    /// Список значений.
    /// </param>
    /// <returns>
    /// Среднее квадратическое отклонение.
    /// </returns>
    public static double Deviation(List<double> list)
    {
        double deviation = 0;
        if (list.Count >= 2)
        {
            Channel channel = new(new(1, new(list)));
            deviation = channel.StandardDeviation();
        }
        return deviation;
    }

    /// <summary>
    /// Определяет максимальное вероятное значение.
    /// </summary>
    /// <param name="list">
    /// Список значений.
    /// </param>
    /// <param name="probable">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимальное вероятное значение.
    /// </returns>
    public static double MaximumProbable (List<double> list, double probable)
    {
        double max = 0;
        if (list.Count != 0)
        {
            if (list.Count >= 2)
            {
                Channel channel = new(new(1, new(list)));
                max = channel.GetStandardMaxProbable(probable);
            }
            else
            {
                max = list[0];
            }
        }
        return max;
    }

    /// <summary>
    /// Определяет максимальное вероятное значение по эмпирическому квантилю.
    /// </summary>
    /// <param name="list">
    /// Список значений.
    /// </param>
    /// <param name="probable">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимальное вероятное значение по эмпирическому квантилю.
    /// </returns>
    public static double MaximumProbableEmp(List<double> list, double probable)
    {
        list.Sort();
        double max = 0;
        if (list.Count != 0)
        {
            if (list.Count == 1)
            {
                max = probable * list[0];
            }
            else
            {
                int k2 = (int)Math.Ceiling(probable * list.Count) - 1;
                int k1 = k2 - 1;
                int N = list.Count;
                double x1 = list[k1];
                double x2 = list[k2];
                double F1 = (double)(k1 + 1) / N;
                double F2 = (double)(k2 + 1) / N;
                max = probable * (x2 - x1) / (F2 - F1) + (F2 * x1 - F1 * x2) / (F2 - F1);
            }
        }
        return max;
    }

    /// <summary>
    /// Возвращает среднее из трёх максимальных значений.
    /// </summary>
    /// <param name="collection">
    /// Коллекция значений.
    /// </param>
    /// <returns>
    /// Среднее из трёх максимальных значений.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="collection"/> передана пустая коллекция.
    /// </exception>
    public static double MeanOf3Max(IEnumerable<double> collection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection), "Передана пустая ссылка.");
        }
        List<double> list = collection.ToList();
        int count = list.Count;
        if (count == 0)
        {
            return 0;
        }
        list.Sort();
        if (count <= 3)
        {
            return list.Average();
        }
        return (list[count - 1] + list[count - 2] + list[count - 3]) / 3;
    }

    /// <summary>
    /// Возвращает ординату линии влияния нагрузки соседнего колеса.
    /// </summary>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <returns>
    /// Ордината линии влияния нагрузки соседнего колеса.
    /// </returns>
    public static double CoefficientNu(double k, double @base) =>
        Math.Exp(-k * @base) * (Math.Cos(k * @base) + Math.Sin(k * @base));

    /// <summary>
    /// Возвращает эквивалентную силу над расчётной шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <returns>
    /// Эквивалентная сила над расчётной шпалой.
    /// </returns>
    public static double Pekv2(double Pmax, double Psr, double k, double @base) =>
        Pmax + CoefficientNu(k, @base) * Psr;

    /// <summary>
    /// Возвращает напряжение в балласте под шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <returns>
    /// Напряжение в балласте под шпалой.
    /// </returns>
    public static double SigmaB2(double Pmax, double Psr, double k, double @base, double lsh, double omega) =>
        k * lsh * Pekv2(Pmax, Psr, k, @base) / (2 * omega) * 10.19367992;

    /// <summary>
    /// Возвращает напряжение в шпале на смятие под подкладкой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="Sp">
    /// Площадь рельсовой подкладки.
    /// </param>
    /// <returns>
    /// Напряжение в шпале на смятие под подкладкой.
    /// </returns>
    public static double SigmaSh(double Pmax, double Psr, double k, double @base, double lsh, double Sp) =>
        k * lsh * Pekv2(Pmax, Psr, k, @base) / (2 * Sp) * 10.19367992;

    /// <summary>
    /// Возвращает коэффициент С1.
    /// </summary>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <returns>
    /// Коэффициент С1.
    /// </returns>
    public static double CoefficientC1(double b, double h) =>
        (b / (2 * h)) - (Math.Pow(b, 3) / (24 * Math.Pow(h, 3)));

    /// <summary>
    /// Возвращает коэффициент С2.
    /// </summary>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <returns>
    /// Коэффициент С2.
    /// </returns>
    public static double CoefficientC2(double b, double h) =>
        b * h / (Math.Pow(b, 2) + 4 * Math.Pow(h, 2));

    /// <summary>
    /// Возвращает коэффициент m.
    /// </summary>
    /// <param name="sigmaB2">
    /// Напряжение в балласте под шпалой.
    /// </param>
    /// <returns>
    /// Коэффициент m.
    /// </returns>
    public static double Coefficientm(double sigmaB2) => 8.9 / (sigmaB2 + 4.35);

    /// <summary>
    /// Возвращает напряжение под расчётной шпалой на глубине h.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="ae">
    /// Коэффициент, учитывающий неравномерность распределения давления вдоль шпалы и пространственность приложения нагрузки.
    /// </param>
    /// <returns>
    /// Напряжение под расчётной шпалой на глубине h.
    /// </returns>
    public static double Sigmah2(double Pmax, double Psr, double k, double @base, double lsh, double omega, double b, double h, double ae)
    {
        var sigmb2 = SigmaB2(Pmax, Psr, k, @base, lsh, omega);
        return sigmb2 * ae * (2.55 * CoefficientC2(b, h) + (0.635 * CoefficientC1(b, h) - 1.275 * CoefficientC2(b, h)) * Coefficientm(sigmb2));
    }

    /// <summary>
    /// Возвращает ординату линии влияния нагрузки соседнего колеса.
    /// </summary>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Ордината линии влияния нагрузки соседнего колеса.
    /// </returns>
    public static double CoefficientNulsh(double k, double lsh) =>
        Math.Exp(-k * lsh) * (Math.Cos(k * lsh) + Math.Sin(k * lsh));

    /// <summary>
    /// Возвращает ординату линии влияния нагрузки соседнего колеса.
    /// </summary>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Ордината линии влияния нагрузки соседнего колеса.
    /// </returns>
    public static double CoefficientNuMinuslsh(double k, double @base, double lsh) =>
        Math.Exp(-k * (@base - lsh)) * (Math.Cos(k * (@base - lsh)) + Math.Sin(k * (@base - lsh)));

    /// <summary>
    /// Возвращает ординату линии влияния нагрузки соседнего колеса.
    /// </summary>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Ордината линии влияния нагрузки соседнего колеса.
    /// </returns>
    public static double CoefficientNuPluslsh(double k, double @base, double lsh) =>
        Math.Exp(-k * (@base + lsh)) * (Math.Cos(k * (@base + lsh)) + Math.Sin(k * (@base + lsh)));

    /// <summary>
    /// Возвращает эквивалентную силу над соседней шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Эквивалентная сила над соседней шпалой.
    /// </returns>
    public static double Pekv1(double Pmax, double Psr, double k, double @base, double lsh)
    {
        var nulsh = CoefficientNulsh(k, lsh);
        var nu = CoefficientNuMinuslsh(k, @base, lsh);
        return Pmax * nulsh + nu * Psr;
    }

    /// <summary>
    /// Возвращает эквивалентную силу над соседней шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Эквивалентная сила над соседней шпалой.
    /// </returns>
    public static double Pekv3(double Pmax, double Psr, double k, double @base, double lsh)
    {
        var nulsh = CoefficientNulsh(k, lsh);
        var nu = CoefficientNuPluslsh(k, @base, lsh);
        return Pmax * nulsh + nu * Psr;
    }

    /// <summary>
    /// Возвращает напряжение в балласте под соседней шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <returns>
    /// Напряжение в балласте под соседней шпалой.
    /// </returns>
    public static double SigmaB1(double Pmax, double Psr, double k, double @base, double lsh, double omega) =>
        k * lsh * Pekv1(Pmax, Psr, k, @base, lsh) / (2 * omega) * 10.19367992;

    /// <summary>
    /// Возвращает напряжение в балласте под соседней шпалой.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <returns>
    /// Напряжение в балласте под соседней шпалой.
    /// </returns>
    public static double SigmaB3(double Pmax, double Psr, double k, double @base, double lsh, double omega) =>
        k * lsh * Pekv3(Pmax, Psr, k, @base, lsh) / (2 * omega) * 10.19367992;

    /// <summary>
    /// Возвращает коэффициент A.
    /// </summary>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Коэффициент A.
    /// </returns>
    private static double CoefficientA(double b, double h, double lsh)
    {
        var Teta1 = Math.Atan((lsh + 0.5 * b) / h);
        var Teta2 = Math.Atan((lsh - 0.5 * b) / h);
        return Teta1 - Teta2 + 0.5 * (Math.Sin(2 * Teta1) - Math.Sin(2 * Teta2));
    }

    /// <summary>
    /// Возвращает напряжение в балласте под соседней шпалой на глубине h.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <returns>
    /// Напряжение в балласте под соседней шпалой на глубине h.
    /// </returns>
    public static double Sigmah1(double Pmax, double Psr, double k, double @base, double lsh, double omega, double b, double h)
    {
        var sigma1 = SigmaB1(Pmax, Psr, k, @base, lsh, omega);
        var A = CoefficientA(b, h, lsh);
        return 0.25 * sigma1 * A;
    }

    /// <summary>
    /// Возвращает напряжение в балласте под соседней шпалой на глубине h.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <returns>
    /// Напряжение в балласте под соседней шпалой на глубине h.
    /// </returns>
    public static double Sigmah3(double Pmax, double Psr, double k, double @base, double lsh, double omega, double b, double h)
    {
        var sigma3 = SigmaB3(Pmax, Psr, k, @base, lsh, omega);
        var A = CoefficientA(b, h, lsh);
        return 0.25 * sigma3 * A;
    }

    /// <summary>
    /// Возвращает напряжение на основной площадке земляного полотна.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="ae">
    /// Коэффициент, учитывающий неравномерность распределения давления вдоль шпалы и пространственность приложения нагрузки.
    /// </param>
    /// <returns>
    /// Напряжение на основной площадке земляного полотна.
    /// </returns>
    public static double Sigmah(double Pmax, double Psr, double k, double @base, double lsh, double omega, double b, double h, double ae)
    {
        var sigma1 = Sigmah1(Pmax, Psr, k, @base, lsh, omega, b, h);
        var sigma2 = Sigmah2(Pmax, Psr, k, @base, lsh, omega, b, h, ae);
        var sigma3 = Sigmah3(Pmax, Psr, k, @base, lsh, omega, b, h);
        return sigma1 + sigma2 + sigma3;
    }

    /// <summary>
    /// Возвращает коэффициент устойчивости рельсошпальной решётки.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная вертикальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя вертикальная сила для соседней оси.
    /// </param>
    /// <param name="Fmax">
    /// Максимальная боковая сила для оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="Kh">
    /// Коэффициент относительной жёсткости рельсового основания и колеса в горизонтальной плоскости.
    /// </param>
    /// <returns>
    /// Коэффициент устойчивости рельсошпальной решётки.
    /// </returns>
    public static double Kust(double Pmax, double Psr, double Fmax, double k, double @base, double lsh, double Kh)
    {
        var Pekv = Pekv2(Pmax, Psr, k, @base);
        var Fshmax = 0.5 * Kh * lsh * Fmax;
        var Pshsr = 0.75 * 0.5 * k * lsh * Pekv;
        return Fshmax / Pshsr;
    }

    /// <summary>
    /// Возвращает коэффициент устойчивости рельсошпальной решётки с учётом сил трения.
    /// </summary>
    /// <param name="Psr">
    /// Средняя вертикальная сила.
    /// </param>
    /// <param name="Fmax">
    /// Максимальная боковая сила.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="fp">
    /// Коэффициент трения колеса по рельсу.
    /// </param>
    /// <param name="fsb">
    /// Коэффициент трения шпалы по балласту.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="CO">
    /// Cопротивление шпалы поперечному сдвигу в балласте при отсутствии поезда.
    /// </param>
    /// <param name="Kh">
    /// Коэффициент относительной жёсткости рельсового основания и колеса в горизонтальной плоскости.
    /// </param>
    /// <returns>
    /// Коэффициент устойчивости рельсошпальной решётки с учётом сил трения.
    /// </returns>
    public static double KustFriction(double Psr, double Fmax, double k, double fp, double fsb, double lsh, double CO, double Kh)
    {
        return 0.5 * Math.Abs(Fmax - Psr * fp) * Kh * lsh / (CO + Psr * k * lsh * fsb);
    }

    //Расчёты по распоряжению ВНИИЖТ 2017.

    /// <summary>
    /// Определяет угол видимости влияния соседней шпалы 1 по методике 2017.
    /// </summary>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <returns>
    /// Угол видимости влияния соседней шпалы 1.
    /// </returns>
    public static double Alfa1 (double h, double lsh, double b)
    {
        double a = Math.Atan(h / (lsh + b / 2));
        return a;
    }

    /// <summary>
    /// Определяет угол видимости влияния соседней шпалы 2 по методике 2017.
    /// </summary>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <returns>
    /// Угол видимости влияния соседней шпалы 2.
    /// </returns>
    public static double Alfa2(double h, double lsh, double b)
    {
        double a = Math.Atan(h / (lsh - b / 2));
        return a;
    }

    /// <summary>
    /// Определяет напряжение на основной площадке земляного полотна под расчётной шпалой по методике 2017.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="b">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Напряжение на основной площадке земляного полотна под расчётной шпалой.
    /// </returns>
    public static double SigmaZr (double Pmax, double Psr, double k, double @base, double lsh, double omega, double h, double b)
    {
        var sigmaB = SigmaB2(Pmax, Psr, k, @base, lsh, omega);
        double s = -2 * sigmaB / Math.PI * (Math.Atan(b / 2 / h) + 2 * b * h / (Math.Pow(b, 2) + 4 * Math.Pow(h, 2)));
        return s;
    }

    /// <summary>
    /// Определяет напряжение на основной площадке земляного полотна под расчётной шпалой по методике 2017.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="b">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Напряжение на основной площадке земляного полотна под расчётной шпалой.
    /// </returns>
    public static double SigmaZ1(double Pmax, double Psr, double k, double @base, double lsh, double omega, double h, double b)
    {
        var sigmaB = SigmaB1(Pmax, Psr, k, @base, lsh, omega);
        var alfa1 = Alfa1(h, lsh, b);
        var alfa2 = Alfa2(h, lsh, b);
        double s = -sigmaB / Math.PI * (alfa2-alfa1-(Math.Sin(2*alfa2)- Math.Sin(2 * alfa1)) /2);
        return s;
    }

    /// <summary>
    /// Определяет напряжение на основной площадке земляного полотна под расчётной шпалой по методике 2017.
    /// </summary>
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="b">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Напряжение на основной площадке земляного полотна под расчётной шпалой.
    /// </returns>
    public static double SigmaZ3(double Pmax, double Psr, double k, double @base, double lsh, double omega, double h, double b)
    {
        var sigmaB = SigmaB3(Pmax, Psr, k, @base, lsh, omega);
        var alfa1 = Alfa1(h, lsh, b);
        var alfa2 = Alfa2(h, lsh, b);
        double s = -sigmaB / Math.PI * (alfa2 - alfa1 - (Math.Sin(2 * alfa2) - Math.Sin(2 * alfa1)) / 2);
        return s;
    }

    /// <summary>
    /// Определяет напряжение на основной площадке земляного полотна под расчётной шпалой по методике 2017.
    /// </summary>\
    /// <param name="Pmax">
    /// Максимальная сила для оси.
    /// </param>
    /// <param name="Psr">
    /// Средняя сила для соседней оси.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="base">
    /// База тележки.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="b">
    /// Расстояние между осями шпал.
    /// </param>
    /// <returns>
    /// Напряжение на основной площадке земляного полотна под расчётной шпалой.
    /// </returns>
    public static double SigmaZh(double Pmax, double Psr, double k, double @base, double lsh, double omega, double h, double b)
    {
        var sigma1 = SigmaZ1(Pmax, Psr, k, @base, lsh, omega, b, h);
        var sigma2 = SigmaZr(Pmax, Psr, k, @base, lsh, omega, b, h);
        var sigma3 = SigmaZ3(Pmax, Psr, k, @base, lsh, omega, b, h);
        return Math.Abs(sigma1 + sigma2 + sigma3);
    }
}
