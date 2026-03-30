using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;

namespace Apeiron.Platform.Demo.AdxlDemo.Net;

/// <summary>
/// Представляет коллекцию сетей.
/// </summary>
public sealed class NetworkCollection :
    DictionaryNode<IPv4Address, Network>
{
    /// <summary>
    /// Постоянная, определяющая задержку перед следующим шагом основного цикла выполнения.
    /// </summary>
    private const int _InvokeDelay = 5000;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public NetworkCollection(Engine engine) :
        base(engine, "Сети", NodeFormat.NetworkCollection, network => network.Host)
    {

    }

    /// <summary>
    /// Асинхронно выполняет поиск сети, содержащей адрес.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск сети.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<Network?> FindAsync(IPv4Address address, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение массива текущих сетей.
        Network[] networks = await ToArrayAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор сетей.
        foreach (Network network in networks)
        {
            //  Проверка принадлежности сети.
            if (await network.ContainsAsync(address, cancellationToken).ConfigureAwait(false))
            {
                //  Возврат найденной сети.
                return network;
            }
        }

        //  Сеть не найдена.
        return null;
    }

    /// <summary>
    /// Асинхронно обновляет информацию о сетях.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление информации о сетях.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение списка хостов.
        List<IPv4Address> hosts = (await Dns.GetHostAddressesAsync(Dns.GetHostName(), cancellationToken).ConfigureAwait(false))
            .Where(address => address.AddressFamily == AddressFamily.InterNetwork)
            .Select(address => new IPv4Address(address))
            .ToList();

        //  Асинхронная работа с конфигурациями сетевых интерфейсов.
        await Parallel.ForEachAsync(
            NetworkInterface.GetAllNetworkInterfaces(),
            cancellationToken,
            async (adapter, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Асинхронная работа с адресами одноадресной рассылки, назначенные интерфейсу.
                await Parallel.ForEachAsync(
                    adapter.GetIPProperties().UnicastAddresses,
                    cancellationToken,
                    async (unicastIPAddressInformation, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Проверка сети.
                        if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            //  Получение адреса текущего ПК в сети.
                            IPv4Address host = new(unicastIPAddressInformation.Address);

                            //  Проверка вхождения в список хостов.
                            if (hosts.Contains(host))
                            {
                                //  Получение маски сети.
                                IPv4Address mask = new(unicastIPAddressInformation.IPv4Mask);

                                //  Добавление или обновление узла.
                                await AddOrUpdateAsync(host,
                                    () => new Network(Engine, host, mask),
                                    network => network.Mask = mask,
                                    cancellationToken).ConfigureAwait(false);
                            }
                        }
                    });
            }).ConfigureAwait(false);
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
                //  Вывод в журнал.
                await Logger.LogAsync("Сканирование сети.", cancellationToken).ConfigureAwait(false);

                //  Обновление информации о сетях.
                await UpdateAsync(cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед повторным сканированием.
            await Task.Delay(_InvokeDelay, cancellationToken).ConfigureAwait(false);
        }
    }
}
