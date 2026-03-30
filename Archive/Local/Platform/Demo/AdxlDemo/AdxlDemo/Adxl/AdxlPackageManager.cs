using Apeiron.Recording.Adxl357;
using System.Collections.Concurrent;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет диспетчер пакетов.
/// </summary>
public sealed class AdxlPackageManager :
    Active
{
    /// <summary>
    /// Постоянная, определяющая задержку перед следующим шагом основного цикла выполнения.
    /// </summary>
    private const int _InvokeDelay = 25;

    /// <summary>
    /// Поле для хранения очереди сообщений.
    /// </summary>
    private readonly ConcurrentQueue<AdxlExtendedPackage> _Packages;

    /// <summary>
    /// Поле для хранения коллекции буферов датчиков.
    /// </summary>
    private readonly AdxlPackageBufferCollection _Buffers;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public AdxlPackageManager(Engine engine) :
        base(engine)
    {
        //  Создание очереди сообщений.
        _Packages = new();

        //  Создание коллекции буферов датчиков.
        _Buffers = new(engine);
    }

    /// <summary>
    /// Асинхронно регистрирует пакет.
    /// </summary>
    /// <param name="device">
    /// Устройство, с которого получен пакет.
    /// </param>
    /// <param name="dataPackage">
    /// Пакет данных.
    /// </param>
    /// <param name="receiptTime">
    /// Время получения пакета.
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
    /// В параметре <paramref name="device"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dataPackage"/> передана пустая ссылка.
    /// </exception>
    public async Task RegistrationAsync(AdxlDevice device, Adxl357DataPackage dataPackage,
        DateTime receiptTime, CancellationToken cancellationToken)
    {
        //  Проверка входных параметров.
        IsNotNull(device, nameof(device));
        IsNotNull(dataPackage, nameof(dataPackage));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение свойств устройства.
        AdxlDeviceProperties properties = await device.GetPropertiesAsync(cancellationToken).ConfigureAwait(false);

        //  Создание расширенного пакета датчика ADXL357.
        AdxlExtendedPackage extendedPackage = new(receiptTime, properties, dataPackage);

        //  Добавление пакета в очередь.
        _Packages.Enqueue(extendedPackage);
    }

    /// <summary>
    /// Асинхронно выполняет основную задачу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Основная задача.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Выполнение метода базового класса.
        await base.InvokeAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасное выполнение.
            await Invoker.SystemAsync(async (cancellationToken) =>
            {
                //  Перебор всех полученных пакетов.
                while (!cancellationToken.IsCancellationRequested &&
                    _Packages.TryDequeue(out AdxlExtendedPackage? package))
                {
                    //  Добавление пакета в буфер.
                    await _Buffers.RegistrationAsync(package, cancellationToken).ConfigureAwait(false);
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед повтором.
            await Task.Delay(_InvokeDelay, cancellationToken).ConfigureAwait(false);
        }
    }
}
