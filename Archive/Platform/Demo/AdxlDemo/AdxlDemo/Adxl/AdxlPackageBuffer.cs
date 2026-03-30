namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет буфер пакетов датчиков.
/// </summary>
public sealed class AdxlPackageBuffer :
    Active
{
    /// <summary>
    /// Поле для хранения предыдущего пакета.
    /// </summary>
    private AdxlExtendedPackage? _PreviousPackage;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public AdxlPackageBuffer(Engine engine) :
        base(engine)
    {
        //  Установка предыдущего пакета.
        _PreviousPackage = null;
    }

    /// <summary>
    /// Асинхронно регистрирует пакет.
    /// </summary>
    /// <param name="package">
    /// Пакет.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая пакет.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="package"/> передана пустая ссылка.
    /// </exception>
    public async Task RegistrationAsync(AdxlExtendedPackage package, CancellationToken cancellationToken)
    {
        //  Проверка пакета.
        IsNotNull(package, nameof(package));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Предварительный расчёт времени начала и длительности записи.
        TimeSpan duration = TimeSpan.FromSeconds(package.DataPackage.Length / package.DeviceProperties.Sampling);
        DateTime beginTime = package.ReceiptTime - duration;

        //  Проверка предыдущего пакета.
        if (_PreviousPackage is not null && _PreviousPackage.EndTime is DateTime endTime)
        {
            //  Получение расхождения по времени.
            TimeSpan deviation = beginTime - endTime;

            //  Получение расхождения по синхромаркеру.
            double count = package.DeviceProperties.Sampling * (package.DataPackage.Synchromarker.Ticks - _PreviousPackage.DataPackage.Synchromarker.Ticks) / TimeSpan.TicksPerMillisecond / 1000.0;

            //  Проверка необходимости корректировки значения.
            if ((25 < count && count < 75) || deviation < /*3 **/ duration)
            {
                //  Корректировка времени начала и длительности записи.
                beginTime = endTime;
                duration += 0.1 * deviation;

                //  Проверка длительности.
                if (duration <= TimeSpan.Zero)
                {
                    //  Установка длительности без корректировки.
                    duration = TimeSpan.FromSeconds(package.DataPackage.Length / package.DeviceProperties.Sampling);
                }
            }
        }

        //  Установка времени начала и длительности записи.
        package.Duration = duration;
        package.BeginTime = beginTime;

        //  Установка предыдущего пакета.
        _PreviousPackage = package;

        //  Передача пакета в организатор каналов.
        await Engine.Root.Channels.RegistrationAsync(package, cancellationToken).ConfigureAwait(false);
    }
}
