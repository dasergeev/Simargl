using Simargl.Analysis;
using Simargl.Border.Geometry;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет группу каналов в одном сечении.
/// </summary>
public class SectionGroup :
    ProcessorUnit
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    /// <param name="section">
    /// Номер сечения.
    /// </param>
    internal SectionGroup(Processor processor, int section) :
        base(processor)
    {
        //  Установка номера сечения.
        Section = section;

        ////  Создание источника канала счётчика осей.
        //Counter = new(processor, $"Counter{section:00}", string.Empty, true);

        //  Создание групп каналов.
        Left = new RailGroup(processor, section, Rail.Left);
        Right = new RailGroup(processor, section, Rail.Right);

        //  Создание списка источников каналов.
        List<ChannelSource> sources =
        [
            //Counter,
        ];
        sources.AddRange(Left.Sources);
        sources.AddRange(Right.Sources);

        //  Установка коллекции всех источников каналов.
        Sources = sources.AsReadOnly();

        //  Определение положения сечения.
        Position = processor.Scheme.Sections.First(x => x.Number == section).Position;
    }

    /// <summary>
    /// Возвращает номер сечения.
    /// </summary>
    public int Section { get; }

    ///// <summary>
    ///// Возвращает источника канала счётчика осей.
    ///// </summary>
    //public ChannelSource Counter { get; }

    /// <summary>
    /// Возвращает группу каналов на левом рельсе.
    /// </summary>
    public RailGroup Left { get; }

    /// <summary>
    /// Возвращает группу каналов на правом рельсе.
    /// </summary>
    public RailGroup Right { get; }

    /// <summary>
    /// Возвращает коллекцию всех источников каналов.
    /// </summary>
    public IReadOnlyList<ChannelSource> Sources { get; }

    /// <summary>
    /// Возвращает положение сечения.
    /// </summary>
    public double Position { get; }

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

        //  Построение групп.
        await Left.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);
        await Right.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);

        ////  Получение исходных сигналов.
        //if (Left.Continuous[synchromarker] is Signal left &&
        //    Right.Continuous[synchromarker] is Signal right)
        //{
        //    //  Создание расчётного сигнала.
        //    Signal counter = new(2000) { Length = BasisConstants.SignalFragmentLength };

        //    //  Проверка среднего значения.
        //    if (Math.Abs(left.Average() + right.Average()) > BasisConstants.CounterThreshold)
        //    {
        //        //  Установка значения счётчика.
        //        Array.Fill(counter.Items, 1);
        //    }

        //    //  Регистрация сигнала.
        //    Counter.Register(synchromarker, counter);
        //}
    }

    /// <summary>
    /// Асинхронно выполняет построение.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение.
    /// </returns>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        ////  Проверка счётчика.
        //if (Counter.IsLoaded && Math.Abs(Counter.Deviation) < BasisConstants.CounterDeviationZero)
        //{
        //    //  Перебор каналов.
        //    foreach (ChannelSource source in Sources)
        //    {
        //        //  Установка нуля.
        //        await source.ZeroAsync(cancellationToken).ConfigureAwait(false);
        //    }
        //}
    }

    //        /// <summary>
    //        /// Устанавливает ноль.
    //        /// </summary>
    //        internal void Zero()
    //        {
    //            Left.Zero();
    //            Right.Zero();
    //        }
}
