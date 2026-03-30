using Simargl.Analysis;
using Simargl.Border.Geometry;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет группу сигналов на одном рельсе, в одном сечении.
/// </summary>
public sealed class RailGroup :
    ProcessorUnit
{
    //private readonly double[] _PFactors;
    //private readonly double[] _FFactors;
    //private readonly double[] _MFactors;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    /// <param name="section">
    /// Номер сечения.
    /// </param>
    /// <param name="rail">
    /// Значение, определяющее рельс.
    /// </param>
    internal RailGroup(Processor processor, int section, Rail rail) :
        base(processor)
    {
        //  Установка значений основных свойств.
        Section = section;
        Rail = rail;

        ////  Создание источников каналов.
        //Continuous = new(processor, "PC" + Rail.ToString()[0] + section.ToString("00"), "kN", true);
        //Vertical = new(processor, "P" + Rail.ToString()[0] + section.ToString("00"), "kN", true);
        //Lateral = new(processor, "F" + Rail.ToString()[0] + section.ToString("00"), "kN", true);
        //Moment = new(processor, "M" + Rail.ToString()[0] + section.ToString("00"), "N*m", true);

        //  Создание групп каналов.
        External = new(processor, section, rail, Side.External);
        Internal = new(processor, section, rail, Side.Internal);

        //  Создание списка источников каналов.
        List<ChannelSource> sources =
        [
            //Continuous,
            //Vertical,
            //Lateral,
            //Moment,
        ];
        sources.AddRange(External.Sources);
        sources.AddRange(Internal.Sources);

        //  Установка коллекции всех источников каналов.
        Sources = sources.AsReadOnly();

        //_PFactors = new double[4];
        //_FFactors = new double[4];
        //_MFactors = new double[4];

        //if (rail == Rail.Right)
        //{
        //    _PFactors[0] = -0.007756567;
        //    _PFactors[1] = -0.006143675;
        //    _PFactors[2] = -0.008389439;
        //    _PFactors[3] = -0.004532694;

        //    _FFactors[0] = -0.000999496;
        //    _FFactors[1] = 0.003178214;
        //    _FFactors[2] = 0.001283404;
        //    _FFactors[3] = -0.00309948;

        //    _MFactors[0] = 0.152464116;
        //    _MFactors[1] = 0.106417953;
        //    _MFactors[2] = -0.17670708;
        //    _MFactors[3] = -0.119681737;
        //}
        //else
        //{
        //    _PFactors[0] = -0.008503084;
        //    _PFactors[1] = -0.005796226;
        //    _PFactors[2] = -0.008051301;
        //    _PFactors[3] = -0.004032223;

        //    _FFactors[0] = -0.001116288;
        //    _FFactors[1] = 0.003273377;
        //    _FFactors[2] = 0.000966514;
        //    _FFactors[3] = -0.003198094;

        //    _MFactors[0] = 0.184084533;
        //    _MFactors[1] = 0.118443871;
        //    _MFactors[2] = -0.162429982;
        //    _MFactors[3] = -0.103054066;
        //}

        //  Получение масштаба.
        ContinuousConstFactor = Processor.Scheme.Modules.First(x => x.Rail == rail && x.Section == section).Scale;
    }

    ///// <summary>
    ///// Возвращает источник канала вертикальной силы по методу двух сечений.
    ///// </summary>
    //public ChannelSource Continuous { get; }

    ///// <summary>
    ///// Возвращает источник канала вертикальной силы.
    ///// </summary>
    //public ChannelSource Vertical { get; }

    ///// <summary>
    ///// Возвращает источник канала боковой силы.
    ///// </summary>
    //public ChannelSource Lateral { get; }

    ///// <summary>
    ///// Возвращает источник канала момента.
    ///// </summary>
    //public ChannelSource Moment { get; }

    /// <summary>
    /// Возвращает номер сечения.
    /// </summary>
    public int Section { get; }

    /// <summary>
    /// Возвращает значение, определяющее рельс.
    /// </summary>
    public Rail Rail { get; }

    /// <summary>
    /// Возвращает группу каналов на внешней стороне.
    /// </summary>
    public SideGroup External { get; }

    /// <summary>
    /// Возвращает группу каналов на внутренней стороне.
    /// </summary>
    public SideGroup Internal { get; }

    /// <summary>
    /// Возвращает коллекцию всех источников каналов.
    /// </summary>
    public IReadOnlyList<ChannelSource> Sources { get; }

    /// <summary>
    /// Возвращает масштаб сигнала вертикальной силы по методу двух сечений.
    /// </summary>
    public double ContinuousConstFactor { get; }

    /// <summary>
    /// Асинхронно выполняет построение.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение.
    /// </returns>
    public async Task BuildAsync(Synchromarker synchromarker, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Построение групп каналов.
        await External.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);
        await Internal.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);

        //  Получение исходных сигналов.
        if (External.Sources[0][synchromarker] is Signal external &&
            Internal.Sources[0][synchromarker] is Signal @internal)
        {
            //  Создание сигнала вертикальной силы по методу двух сечений.
            Signal continuous = new(2000) { Length = BasisConstants.SignalFragmentLength };

            //  Проверка рельса.
            if (Rail == Rail.Right)
            {
                //  Перебор значений.
                for (int i = 0; i != BasisConstants.SignalFragmentLength; ++i)
                {
                    //  Определение значения.
                    continuous.Items[i] = ContinuousConstFactor * (@internal.Items[i] - external.Items[i]);
                }
            }
            else
            {
                //  Перебор значений.
                for (int i = 0; i != BasisConstants.SignalFragmentLength; ++i)
                {
                    //  Определение значения.
                    continuous.Items[i] = ContinuousConstFactor * (external.Items[i] - @internal.Items[i]);
                }
            }

            ////  Регистрация сигнала.
            //Continuous.Register(synchromarker, continuous);
        }

        ////  Получение исходных сигналов.
        //if (Internal.Sources[1][synchromarker] is Signal signal0 &&
        //    Internal.Sources[2][synchromarker] is Signal signal1 &&
        //    External.Sources[1][synchromarker] is Signal signal2 &&
        //    External.Sources[2][synchromarker] is Signal signal3)
        //{
        //    //  Создание расчётных сигналов.
        //    Signal vertical = new(2000) { Length = BasisConstants.SignalFragmentLength };
        //    Signal lateral = new(2000) { Length = BasisConstants.SignalFragmentLength };
        //    Signal moment = new(2000) { Length = BasisConstants.SignalFragmentLength };

        //    //  Перебор значений.
        //    for (int i = 0; i != BasisConstants.SignalFragmentLength; ++i)
        //    {
        //        //  Расчёт значений.
        //        vertical.Items[i] = _PFactors[0] * signal0.Items[i] + _PFactors[1] * signal1.Items[i]
        //            + _PFactors[2] * signal2.Items[i] + _PFactors[3] * signal3.Items[i];
        //        lateral.Items[i] = _FFactors[0] * signal0.Items[i] + _FFactors[1] * signal1.Items[i]
        //            + _FFactors[2] * signal2.Items[i] + _FFactors[3] * signal3.Items[i];
        //        moment.Items[i] = _MFactors[0] * signal0.Items[i] + _MFactors[1] * signal1.Items[i]
        //            + _MFactors[2] * signal2.Items[i] + _MFactors[3] * signal3.Items[i];
        //    }

        //    //  Регистрация сигналов.
        //    Vertical.Register(synchromarker, vertical);
        //    Lateral.Register(synchromarker, lateral);
        //    Moment.Register(synchromarker, moment);
        //}
    }

    //        /// <summary>
    //        /// Устанавливает ноль.
    //        /// </summary>
    //        internal void Zero()
    //        {
    //            External.Zero();
    //            Internal.Zero();
    //        }
}
