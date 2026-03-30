using Apeiron.Platform.Demo.AdxlDemo.Net;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет коллекцию датчиков ADXL357.
/// </summary>
public sealed class AdxlDeviceCollection :
    DictionaryNode<long, AdxlDevice>
{
    /// <summary>
    /// Постоянная, определяющая задержку перед следующим шагом основного цикла выполнения.
    /// </summary>
    private const int _InvokeDelay = 1000;

    /// <summary>
    /// Время ожидания ответа на эхозапрос.
    /// </summary>
    private const int _PingTimeout = 1;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public AdxlDeviceCollection(Engine engine) :
        base(engine, "Датчики", NodeFormat.AdxlDeviceCollection, device => device.SerialNumber)
    {

    }

    /// <summary>
    /// Возвращает количество каналов.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Количество")]
    [Description("Количество датчиков.")]
    public int Count => Nodes.Count;

    /// <summary>
    /// Асинхронно выполняет поиск датчика, имеющего указанный адрес.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск датчика.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<AdxlDevice?> FindAsync(IPv4Address address, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Поиск соедиения с датчиком.
        AdxlConnect? connect = await AdxlConnect.FindAsync(Engine, address, cancellationToken).ConfigureAwait(false);

        //  Проверка соединения с датчиком.
        if (connect is null)
        {
            //  Датчик не найден.
            return null;
        }

        //  Получение серийного номера датчика.
        uint serialNumber = connect.ReadSerialNumber();

        //  Добавление или обновление датчика.
        return await AddOrUpdateAsync(serialNumber,
            () => new AdxlDevice(Engine, serialNumber, connect),
            device => device.Connect = connect,
            cancellationToken).ConfigureAwait(false);
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
            //  Безопасное выполнние действия.
            await Invoker.SystemAsync(async (cancellationToken) =>
            {
                //  Поиск датчиков в сети.
                await FindInNetworkAsync(cancellationToken).ConfigureAwait(false);

                //  Проверка соединений с датчиками.
                await CheckConnectsAsync(cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед повторным сканированием.
            await Task.Delay(_InvokeDelay, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет поиск датчиков в сети.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск датчиков в сети.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task FindInNetworkAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасное выполнение.
        await Invoker.SystemAsync(async (cancellationToken) =>
        {
            //  Вывод в журнал.
            await Logger.LogAsync("Поиск датчиков.", cancellationToken).ConfigureAwait(false);

            //  Получение массива сетей.
            Network[] networks = await Engine.Root.Networks.ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Асинхронный перебор сетей.
            await Parallel.ForEachAsync(
                networks,
                cancellationToken,
                async (network, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Выполнение эхо-запроса в сети.
                    await foreach (PingReply reply in network.PingAsync(_PingTimeout, cancellationToken))
                    {
                        //  Проверка статуса ответа.
                        if (reply.Status == IPStatus.Success &&
                            reply.Options is PingOptions pingOptions &&
                            pingOptions.Ttl <= 10
                            )
                        {
                            //  Запуск задачи, обрабатывающий новый узел.
                            _ = Task.Run(async delegate
                            {
                                //  Проверка токена отмены.
                                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Вывод информации в журнал.
                                await Logger.LogAsync($"Получен ответ от активного узла: {reply.Address}", cancellationToken).ConfigureAwait(false);

                                //  Установка соединения с датчиком.
                                AdxlConnect? connect = await AdxlConnect.FindAsync(
                                    Engine, new(reply.Address), cancellationToken).ConfigureAwait(false);

                                //  Проверка соединения.
                                if (connect is null)
                                {
                                    //  Завершение работы с узлом.
                                    return;
                                }

                                //  Проверка токена отмены.
                                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Получение серийного номера датчика.
                                uint serialNumber = connect.ReadSerialNumber();

                                //  Вывод информации о датчике.
                                await Logger.LogAsync($"Активный датчик в сети: {serialNumber:X8}", cancellationToken).ConfigureAwait(false);

                                //  Добавление или обновление датчика.
                                AdxlDevice device = await AddOrUpdateAsync(serialNumber,
                                    () => new AdxlDevice(Engine, serialNumber, connect),
                                    device => device.Connect = connect,
                                    cancellationToken).ConfigureAwait(false);

                                //  Обновление датчика.
                                await device.UpdateAsync(cancellationToken).ConfigureAwait(false);
                            }, cancellationToken);
                        }
                    }
                }).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно проверяет соединения с датчиками.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление информации о соединении с датчиком.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task CheckConnectsAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасное выполнение.
        await Invoker.SystemAsync(async (cancellationToken) =>
        {
            //  Вывод в журнал.
            await Logger.LogAsync("Проверка датчиков.", cancellationToken).ConfigureAwait(false);

            //  Получение массива текущих датчиков.
            AdxlDevice[] devices = await ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Асинхронный перебор подключений.
            await Parallel.ForEachAsync(
                devices,
                cancellationToken,
                async (device, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Безопасное выполнение.
                    await Invoker.SystemAsync(async (cancellationToken) =>
                    {
                        //  Проверка адреса сервера.
                        IPv4Address server = device.Connect.ReadServer();
                        if (server != device.Connect.Network.Host && Engine.Application.Settings.IsDeviceCapture)
                        {
                            //  Запись адреса сервера.
                            device.Connect.WriteServer(device.Connect.Network.Host);

                            //  Перезагрузка контроллера.
                            //device.Connect.WriteState(0xA0A0);
                            await device.RebootAsync(cancellationToken).ConfigureAwait(false);

                            //  Выброс исключения для прекращения работы с датчиком до повторного появления его в сети.
                            throw Exceptions.OperationInvalid();
                        }

                        //  Обновление информации о датчике.
                        await device.UpdateAsync(cancellationToken).ConfigureAwait(false);

                        //  Вывод в журнал.
                        await Logger.LogAsync($"Датчик {device.SerialNumber:X8} проверен.", cancellationToken).ConfigureAwait(false);
                    }, async (ex, cancellationToken) =>
                    {
                        //  Удаление узла из коллекции.
                        await TryRemoveAsync(device.SerialNumber, cancellationToken).ConfigureAwait(false);
                    }, cancellationToken).ConfigureAwait(false);
                });
        }, cancellationToken).ConfigureAwait(false);
    }
}
