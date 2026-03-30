using Simargl.Border.Processing;
using Simargl.Net;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет коллекцию устройств.
/// </summary>
/// <param name="processor">
/// Устройство обработки.
/// </param>
public sealed class DeviceCollection(Processor processor) :
    ProcessorUnit(processor),
    IEnumerable<Device>
{
    /// <summary>
    /// Поле для хранения карты устройств.
    /// </summary>
    private readonly ConcurrentDictionary<IPv4Address, Device> _Map = [];

    /// <summary>
    /// Возвращает устройство с указанным адресом.
    /// </summary>
    /// <param name="address">
    /// Адрес устройства.
    /// </param>
    /// <returns>
    /// Устройство с указанным адресом.
    /// </returns>
    public Device this[IPv4Address address]
    {
        get
        {
            //  Возврат устройства из карты.
            return _Map.GetOrAdd(address, delegate(IPv4Address address)
            {
                //  Создание нового устройства.
                return new(Processor, address);
            });
        }
    }

    /// <summary>
    /// Асинхронно возвращает текущий синхромаркер.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая текущий синхромаркер.
    /// </returns>
    public async Task<Synchromarker?> GetSynchromarkerAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Извлечение синхромаркеров.
        List<Synchromarker> source = [.. this.Select(
            x => new
            {
                x.Packages.LastSynchromarker,
                x.Packages.SynchromarkerContinuous,
            })
            .Where(x => x.SynchromarkerContinuous >= BasisConstants.SynchromarkerContinuousMin)
            .Select(x => x.LastSynchromarker)];

        //  Основной цикл расчёта.
        while (source.Count > BasisConstants.DeviceCountMin && !cancellationToken.IsCancellationRequested)
        {
            //  Определение
        }

        //  Не удалось определить синхромаркер.
        return null;


        ////  Извлечение данных.
        //var a = this.Select(
        //    x => new
        //    {
        //        x.Packages.LastSynchromarker,
        //        x.Packages.SynchromarkerContinuous,
        //    })
        //    .Where(x => x.SynchromarkerContinuous >= BasisConstants.SynchromarkerContinuousMin)
        //    .Select(x => x.LastSynchromarker)
        //    .GroupBy(x => x)
        //    .Select(x => new
        //    {
        //        Synchromarker = x.Key,
        //        Count = x.Count()
        //    });

    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Device> GetEnumerator()
    {
        //  Возврат перечислителя значений карты.
        return _Map.Values.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя.
        return GetEnumerator();
    }
}
