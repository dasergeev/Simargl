using System.Collections.Concurrent;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет коллекцию буферов датчиков.
/// </summary>
public sealed class AdxlPackageBufferCollection :
    Active
{
    /// <summary>
    /// Поле для хранения карты буферов датчиков.
    /// </summary>
    private readonly ConcurrentDictionary<long, AdxlPackageBuffer> _Map;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public AdxlPackageBufferCollection(Engine engine) :
        base(engine)
    {
        //  Создание карты буферов.
        _Map = new();
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

        //  Получение серийного номера датчика.
        long serialNumber = package.DeviceProperties.SerialNumber;

        //  Получение буфера.
        AdxlPackageBuffer buffer = _Map.GetOrAdd(serialNumber, serialNumber => new(Engine));

        //  Регистрация пакета в буфере.
        await buffer.RegistrationAsync(package, cancellationToken).ConfigureAwait(false);
    }
}
