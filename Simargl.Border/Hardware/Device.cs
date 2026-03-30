using Simargl.Border.Processing;
using Simargl.Net;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет устройство.
/// </summary>
/// <param name="processor">
/// Устройство обработки.
/// </param>
/// <param name="address">
/// IP-адрес устройства.
/// </param>
public sealed class Device(Processor processor, IPv4Address address) :
    ProcessorUnit(processor)
{
    /// <summary>
    /// Возвращает адрес устройства.
    /// </summary>
    public IPv4Address Address { get; } = address;

    /// <summary>
    /// Возвращает коллекцию пакетов.
    /// </summary>
    public DevicePackageCollection Packages { get; } = [];

    /// <summary>
    /// Возвращает предварительный просмотр данных устройства.
    /// </summary>
    public DeviceDataPreview Preview { get; } = new(processor);

    /// <summary>
    /// Возвращает статистику.
    /// </summary>
    public DeviceStatisticalParameterCollection Statistics { get; } = [];

    /// <summary>
    /// Асинхронно регистрирует полученные данные.
    /// </summary>
    /// <param name="package">
    /// Пакет данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая данные.
    /// </returns>
    public async Task RegisterAsync(DevicePackage package, CancellationToken cancellationToken)
    {
        //  Регистрация пакета.
        await Packages.RegisterAsync(package, cancellationToken).ConfigureAwait(false);

        //  Обновление статистики.
        await UpdateStatisticsAsync(package, cancellationToken).ConfigureAwait(false);

        //  Регистрация данных в предварительном просмотре.
        await Preview.RegisterAsync(package, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно обновляет статистику.
    /// </summary>
    /// <param name="package">
    /// Пакет данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, обновляющая статистику.
    /// </returns>
    private async Task UpdateStatisticsAsync(DevicePackage package, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка количества непрерывных синхромаркеров.
        if (Packages.SynchromarkerContinuous >= BasisConstants.SynchromarkerContinuousMin)
        {
            //  Регистрация в синхронизаторе.
            Processor.Synchronizer.Register(package.ReceiptTime, package.Synchromarker);
        }

        //  Установка значений.
        Statistics.TimeContinuous.Value = Packages.TimeContinuous.ToString();
        Statistics.DataContinuous.Value = Packages.DataContinuous.ToString("0.0000");
        Statistics.LastSynchromarker.Value = Packages.LastSynchromarker.ToString();
        Statistics.SynchromarkerContinuous.Value = Packages.SynchromarkerContinuous.ToString();
    }
}
