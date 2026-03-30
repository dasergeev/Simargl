using Simargl.Analysis;
using Simargl.Border.Geometry;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;
using Simargl.Border.Schematic;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет группу каналов на одной стороне, на одном рельсе, в одном сечении.
/// </summary>
public sealed class SideGroup :
    ProcessorUnit
{
    /// <summary>
    /// Поле для хранения устройства.
    /// </summary>
    private readonly Device _Device;

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
    /// <param name="side">
    /// Значение, определяющее сторону.
    /// </param>
    internal SideGroup(Processor processor, int section, Rail rail, Side side) :
        base(processor)
    {
        //  Установка значений основных свойств.
        Section = section;
        Rail = rail;
        Side = side;

        //  Имя канала.
        string name = "S";
        if (rail == Rail.Right)
        {
            name += "R";
        }
        else
        {
            name += "L";
        }
        if (side == Side.External)
        {
            name += "e";
        }
        else
        {
            name += "i";
        }
        name += section.ToString("00");

        //  Создание источников каналов.
        Source0 = new (processor, name + "_0", string.Empty/*, false*/);
        Source1 = new (processor, name + "_1", string.Empty/*, false*/);
        Source2 = new (processor, name + "_2", string.Empty/*, false*/);

        //  Создание коллекции всех источников каналов.
        Sources = [Source0, Source1, Source2];

        //  Получение данных из схемы.
        ModuleScheme scheme = processor.Scheme.Modules.First(x => x.Section == section && x.Rail == rail);

        //  Определение устройства.
        _Device = processor.Devices[side == Side.Internal ? scheme.IPAddress1 : scheme.IPAddress2];
    }

    /// <summary>
    /// Возвращает номер сечения.
    /// </summary>
    public int Section { get; }

    /// <summary>
    /// Возвращает значение, определяющее рельс.
    /// </summary>
    public Rail Rail { get; }

    /// <summary>
    /// Возвращает сторону рельса.
    /// </summary>
    public Side Side { get; }

    /// <summary>
    /// Возвращает источник канала нулевого датчика.
    /// </summary>
    public ChannelSource Source0 { get; }

    /// <summary>
    /// Возвращает источник канала первого датчика.
    /// </summary>
    public ChannelSource Source1 { get; }

    /// <summary>
    /// Возвращает источник канала второго датчика.
    /// </summary>
    public ChannelSource Source2 { get; }

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

        //  Получение пакета.
        if (_Device.Packages[synchromarker] is DevicePackage package)
        {
            //  Создание сигналов.
            Signal signal0 = new(2000, new([.. package.Data0.Select(x => (double)x)]));
            Signal signal1 = new(2000, new([.. package.Data1.Select(x => (double)x)]));
            Signal signal2 = new(2000, new([.. package.Data2.Select(x => (double)x)]));

            //  Регистрация сигналов.
            Source0.Register(synchromarker, signal0);
            Source1.Register(synchromarker, signal1);
            Source2.Register(synchromarker, signal2);
        }
    }

    /// <summary>
    /// Возвращает коллекцию всех источников каналов.
    /// </summary>
    public IReadOnlyList<ChannelSource> Sources { get; }

    //        /// <summary>
    //        /// Устанавливает ноль.
    //        /// </summary>
    //        internal void Zero()
    //        {
    //            Signal0.Zero();
    //            Signal1.Zero();
    //            Signal2.Zero();
    //        }

    //        /// <summary>
    //        /// Обновляет данные.
    //        /// </summary>
    //        /// <param name="blockIndex">
    //        /// Индекс чтения.
    //        /// </param>
    //        internal void Update(int blockIndex)
    //        {

    //        }
}
