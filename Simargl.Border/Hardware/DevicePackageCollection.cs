using Simargl.Border.Processing;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет коллекцию пакетов.
/// </summary>
public sealed class DevicePackageCollection :
    IEnumerable<DevicePackage?>
{
    /// <summary>
    /// Поле для хранения циклического буфера.
    /// </summary>
    private readonly CircularBuffer<DevicePackage> _Buffer = new(BasisConstants.DevicePackageCollectionSize);

    /// <summary>
    /// Поле для хранения времени начала поступления данных.
    /// </summary>
    private DateTime _LastBeginTime = DateTime.Now;

    /// <summary>
    /// Возвращает последний синхромаркер.
    /// </summary>
    public Synchromarker LastSynchromarker => _Buffer.Synchromarker;

    /// <summary>
    /// Возвращает количество непрерывных синхромаркеров.
    /// </summary>
    public long SynchromarkerContinuous => _Buffer.Count;

    /// <summary>
    /// Возвращает время непрерывной работы.
    /// </summary>
    public TimeSpan TimeContinuous { get; private set; }

    /// <summary>
    /// Возвращает размер непрерывно переданных данных в МБ.
    /// </summary>
    public double DataContinuous { get; private set; }

    /// <summary>
    /// Возвращает элемент с указанным синхромаркером.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <returns>
    /// Элемент с указанным синхромаркером.
    /// </returns>
    public DevicePackage? this[Synchromarker synchromarker] => _Buffer[synchromarker];

    /// <summary>
    /// Асинхронно регистрирует полученные данные.
    /// </summary>
    /// <param name="package">
    /// Пакет данных от модуля Т.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая данные.
    /// </returns>
    public async Task RegisterAsync(DevicePackage package, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Регистрация в буфере.
        _Buffer.Register(package.Synchromarker, package);

        //  Проверка последовательности синхромаркеров.
        if (_Buffer.Count == 1)
        {
            //  Сброс времени.
            _LastBeginTime = package.ReceiptTime;
        }

        //  Обновление непреывных значений.
        TimeContinuous = package.ReceiptTime - _LastBeginTime;
        DataContinuous = 611.0 * SynchromarkerContinuous / 1048576.0;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<DevicePackage?> GetEnumerator()
    {
        return _Buffer.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Buffer.GetEnumerator();
    }
}
