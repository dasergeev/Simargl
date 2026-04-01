namespace Simargl.Analysis.EffectWayIndicators;

/// <summary>
/// Класс, который содержит данные.
/// </summary>
public sealed class WayParameters
{
    /// <summary>
    /// Возвращает данные для конструкции пути R652000GBS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R652000GBS.
    /// </returns>
    public static WayParameters R652000GBS { get; } = new(WayParametersKey.R652000GBS, "Р65(6)2000(ЖБ)Щ",
                                                                 1100, 0.01421, 51, 417, 0.403, 518, 3092, 27.6, 0.7, 55, 0.45, 0.0215992);
    /// <summary>
    /// Возвращает данные для конструкции пути R651840GBS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R651840GBS.
    /// </returns>
    public static WayParameters R651840GBS { get; } = new(WayParametersKey.R651840GBS, "Р65(6)1840(ЖБ)Щ",
                                                             1000, 0.01338, 55, 417, 0.403, 518, 3092, 27.6, 0.7, 55, 0.45, 0.02007);

    /// <summary>
    /// Возвращает данные для конструкции пути R652000IIS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R652000IIS.
    /// </returns>
    public static WayParameters R652000IIS { get; } = new(WayParametersKey.R652000IIS, "Р65(6)2000(II)Щ",
                                                             295, 0.01023, 51, 417, 0.433, 612, 2853, 25, 0.8, 50, 0.375, 0.0155496);

    /// <summary>
    /// Возвращает данные для конструкции пути R651840IIS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R651840IIS.
    /// </returns>
    public static WayParameters R651840IIS { get; } = new(WayParametersKey.R651840IIS, "Р65(6)1840(II)Щ",
                                                             270, 0.01, 55, 417, 0.433, 612, 2853, 25, 0.8, 50, 0.375, 0.015);

    /// <summary>
    /// Возвращает данные для конструкции пути R502000IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R502000IIGR.
    /// </returns>
    public static WayParameters R502000IIGR { get; } = new(WayParametersKey.R502000IIGR, "Р50(6)2000(II)Гр",
                                                             230, 0.0111, 51, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.017316);

    /// <summary>
    /// Возвращает данные для конструкции пути R501840IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501840IIGR.
    /// </returns>
    public static WayParameters R501840IIGR { get; } = new(WayParametersKey.R501840IIGR, "Р50(6)1840(II)Гр",
                                                             210, 0.01085, 55, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.016492);

    /// <summary>
    /// Возвращает данные для конструкции пути R501600IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501600IIGR.
    /// </returns>
    public static WayParameters R501600IIGR { get; } = new(WayParametersKey.R501600IIGR, "Р50(6)1600(II)Гр",
                                                             180, 0.01044, 63, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.01566);

    /// <summary>
    /// Возвращает данные для конструкции пути R502000IIP.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R502000IIP.
    /// </returns>
    public static WayParameters R502000IIP { get; } = new(WayParametersKey.R502000IIP, "Р50(6)2000(II)П",
                                                             230, 0.0111, 51, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.017316);

    /// <summary>
    /// Возвращает данные для конструкции пути R501840IIP.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501840IIP.
    /// </returns>
    public static WayParameters R501840IIP { get; } = new(WayParametersKey.R501840IIP, "Р50(6)1840(II)П",
                                                             210, 0.01085, 55, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.016492);

    /// <summary>
    /// Возвращает данные для конструкции пути R501600IIP.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501600IIP.
    /// </returns>
    public static WayParameters R501600IIP { get; } = new(WayParametersKey.R501600IIP, "Р50(6)1600(II)П",
                                                             180, 0.01044, 63, 273, 0.433, 527, 2561, 23, 0.8, 45, 0.375, 0.01566);

    /// <summary>
    /// Возвращает данные для конструкции пути R652000IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R652000IIGR.
    /// </returns>
    public static WayParameters R652000IIGR { get; } = new(WayParametersKey.R652000IIGR, "Р65(6)2000(II)Гр",
                                                             230, 0.00961, 51, 417, 0.433, 612, 2561, 23, 0.8, 50, 0.375, 0.0154721);

    /// <summary>
    /// Возвращает данные для конструкции пути R651840IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R651840IIGR.
    /// </returns>
    public static WayParameters R651840IIGR { get; } = new(WayParametersKey.R651840IIGR, "Р65(6)1840(II)Гр",
                                                             210, 0.00939, 55, 417, 0.433, 612, 2561, 23, 0.8, 50, 0.375, 0.0147423);

    /// <summary>
    /// Возвращает данные для конструкции пути R651600IIGR.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R651600IIGR.
    /// </returns>
    public static WayParameters R651600IIGR { get; } = new(WayParametersKey.R651600IIGR, "Р65(6)1600(II)Гр",
                                                             180, 0.00904, 63, 417, 0.433, 612, 2561, 23, 0.8, 50, 0.375, 0.014012);

    /// <summary>
    /// Возвращает данные для конструкции пути R502000IIS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R502000IIS.
    /// </returns>
    public static WayParameters R502000IIS { get; } = new(WayParametersKey.R502000IIS, "Р50(6)2000(II)Щ",
                                                             290, 0.01176, 51, 273, 0.433, 527, 2466, 23, 0.8, 45, 0.375, 0.0172872);

    /// <summary>
    /// Возвращает данные для конструкции пути R501840IIS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501840IIS.
    /// </returns>
    public static WayParameters R501840IIS { get; } = new(WayParametersKey.R501840IIS, "Р50(6)1840(II)Щ",
                                                             260, 0.01145, 55, 273, 0.433, 527, 2466, 23, 0.8, 45, 0.375, 0.016488);

    /// <summary>
    /// Возвращает данные для конструкции пути R501600IIS.
    /// </summary>
    /// <returns>
    /// Данные для конструкции пути R501600IIS.
    /// </returns>
    public static WayParameters R501600IIS { get; } = new(WayParametersKey.R501600IIS, "Р50(6)1600(II)Щ",
                                                             230, 0.0111, 63, 273, 0.433, 527, 2466, 23, 0.8, 45, 0.375, 0.015762);

    /// <summary>
    /// Возвращает коллекцию всех данных.
    /// </summary>
    /// <returns>
    /// Коллекция всех данных.
    /// </returns>
    public static WayParametersCollection AllData { get; } = new(
        new WayParameters[] { R652000GBS, R651840GBS, R652000IIS, R651840IIS, R502000IIGR, R501840IIGR, R501600IIGR, R502000IIP, R501840IIP, R501600IIP, R652000IIGR, R651840IIGR, R651600IIGR, R502000IIS, R501840IIS, R501600IIS });

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="key">
    /// Ключ.
    /// </param>
    /// <param name="name">
    /// Имя.
    /// </param>
    /// <param name="u">
    /// Модуль упругости рельсового основания.
    /// </param>
    /// <param name="k">
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </param>
    /// <param name="lsh">
    /// Расстояние между осями шпал.
    /// </param>
    /// <param name="w">
    /// Момент сопротивления рельса по низу подошвы при износе головки 6 мм.
    /// </param>
    /// <param name="alfa">
    /// Коэффициент, учитывающий отношение необрессоренной массы колеса и участвующий во взаимодействии массы пути.
    /// </param>
    /// <param name="sp">
    /// Площадь рельсовой подкладки.
    /// </param>
    /// <param name="omega">
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </param>
    /// <param name="b">
    /// Ширина нижней постели шпалы.
    /// </param>
    /// <param name="ae">
    /// Коэффициент, учитывающий неравномерность распределения давления вдоль шпалы и пространственность приложения нагрузки.
    /// </param>
    /// <param name="h">
    /// Толщина балластного слоя под шпалой.
    /// </param>
    /// <param name="fsb">
    /// Коэффициент трения шпалы по балласту.
    /// </param>
    /// <param name="kh">
    /// Коэффициент относительной жёсткости рельсового основания и колеса в горизонтальной плоскости.
    /// </param>
    WayParameters(WayParametersKey key, string name, double u, double k, double lsh, double w, double alfa,
        double sp, double omega, double b, double ae, double h, double fsb, double kh)
    {
        Key = key;
        Name = name;
        U = u;
        K = k;
        Lsh = lsh;
        W = w;
        Alfa = alfa;
        Sp = sp;
        Omega = omega;
        B = b;
        Ae = ae;
        H = h;
        Fsb = fsb;
        Kh = kh;
    }

    /// <summary>
    /// Возвращает ключ.
    /// </summary>
    /// <returns>
    /// Ключ.
    /// </returns>
    public WayParametersKey Key { get; }

    /// <summary>
    /// Возвращает имя.
    /// </summary>
    /// <returns>
    /// Имя.
    /// </returns>
    public string Name { get; }

    /// <summary>
    /// Возвращает модуль упругости рельсового основания.
    /// </summary>
    /// <returns>
    /// Модуль упругости рельсового основания.
    /// </returns>
    public double U { get; }

    /// <summary>
    /// Возвращает коэффициент относительной жёсткости рельсового основания.
    /// </summary>
    /// <returns>
    /// Коэффициент относительной жёсткости рельсового основания.
    /// </returns>
    public double K { get; }

    /// <summary>
    /// Возвращает расстояние между осями шпал.
    /// </summary>
    /// <returns>
    /// Расстояние между осями шпал.
    /// </returns>
    public double Lsh { get; }

    /// <summary>
    /// Возвращает момент сопротивления рельса по низу подошвы при износе головки 6 мм.
    /// </summary>
    /// <returns>
    /// Момент сопротивления рельса по низу подошвы при износе головки 6 мм.
    /// </returns>
    public double W { get; }

    /// <summary>
    /// Возвращает коэффициент, учитывающий отношение необрессоренной массы колеса и участвующий во взаимодействии массы пути.
    /// </summary>
    /// <returns>
    /// Коэффициент, учитывающий отношение необрессоренной массы колеса и участвующий во взаимодействии массы пути.
    /// </returns>
    public double Alfa { get; }

    /// <summary>
    /// Возвращает площадь рельсовой подкладки.
    /// </summary>
    /// <returns>
    /// Площадь рельсовой подкладки.
    /// </returns>
    public double Sp { get; }

    /// <summary>
    /// Возвращает площадь полушпалы с учётом поправки на изгиб.
    /// </summary>
    /// <returns>
    /// Площадь полушпалы с учётом поправки на изгиб.
    /// </returns>
    public double Omega { get; }

    /// <summary>
    /// Возвращает ширину нижней постели шпалы.
    /// </summary>
    /// <returns>
    /// Ширина нижней постели шпалы.
    /// </returns>
    public double B { get; }

    /// <summary>
    /// Возвращает коэффициент, учитывающий неравномерность распределения давления вдоль шпалы и пространственность приложения нагрузки.
    /// </summary>
    /// <returns>
    /// Коэффициент, учитывающий неравномерность распределения давления вдоль шпалы и пространственность приложения нагрузки.
    /// </returns>
    public double Ae { get; }

    /// <summary>
    /// Возвращает толщину балластного слоя под шпалой.
    /// </summary>
    /// <returns>
    /// Толщина балластного слоя под шпалой.
    /// </returns>
    public double H { get; }


    /// <summary>
    /// Возвращает коэффициент трения шпалы по балласту.
    /// </summary>
    /// <returns>
    /// Коэффициент трения шпалы по балласту.
    /// </returns>
    public double Fsb { get; }

    /// <summary>
    /// Возвращает коэффициент относительной жёсткости рельсового основания и колеса в горизонтальной плоскости.
    /// </summary>
    /// <returns>
    /// Коэффициент относительной жёсткости рельсового основания и колеса в горизонтальной плоскости.
    /// </returns>
    public double Kh { get; }
}
