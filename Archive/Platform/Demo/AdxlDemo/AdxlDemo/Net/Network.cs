using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Demo.AdxlDemo.Net;

/// <summary>
/// Представляет сеть.
/// </summary>
public sealed class Network :
    Node<INode>
{
    /// <summary>
    /// Поле для хранения маски сети.
    /// </summary>
    private IPv4Address _Mask;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="host">
    /// Адрес текущего ПК в сети.
    /// </param>
    /// <param name="mask">
    /// Маска сети.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Network(Engine engine, IPv4Address host, IPv4Address mask) :
        base(engine, host.ToString(), NodeFormat.Network)
    {
        //  Установка адреса текущего ПК в сети.
        Host = host;

        //  Установка маски сети.
        _Mask = mask;

        //  Установка адреса сети.
        Address = host & mask;
    }

    /// <summary>
    /// Возвращает адрес текущего ПК в сети.
    /// </summary>
    public IPv4Address Host { get; }

    /// <summary>
    /// Возвращает маску сети.
    /// </summary>
    public IPv4Address Mask
    {
        get => _Mask;
        set
        {
            //  Сохранение старых значений.
            IPv4Address oldMask = Mask;
            IPv4Address oldAddress = Address;

            //  Установка текущих значений.
            _Mask = value;
            Address = Host & value;

            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Проверка изменения значения маски сети.
                if (_Mask != oldMask)
                {
                    //  Вызов события об изменении значения.
                    OnPropertyChanged(new(nameof(Mask)));
                }

                //  Проверка изменения значения адреса сети.
                if (Address != oldAddress)
                {
                    //  Вызов события об изменении значения.
                    OnPropertyChanged(new(nameof(Address)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает адрес сети.
    /// </summary>
    public IPv4Address Address { get; private set; }

    /// <summary>
    /// Асинхронно проверяет принадлежит ли адрес данной сети.
    /// </summary>
    /// <param name="address">
    /// Адрес для проверки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проверку.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<bool> ContainsAsync(IPv4Address address, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение проверки.
        return await Task.Run(delegate
        {
            //  Проверка.
            return (address & Mask) == Address;
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно отправляет эхо-запросы всем внешним адресам сети.
    /// </summary>
    /// <param name="timeout"> 
    /// Максимальное время (после отправки сообщения проверки связи)
    /// ожидания сообщения ответа проверки связи ICMP в миллисекундах.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отправляющая эхо-запросы.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async IAsyncEnumerable<PingReply> PingAsync(int timeout, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        //  Проверка времени ожидания.
        IsPositive(timeout, nameof(timeout));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение значения адреса.
        uint addressValue = (uint)Address.Value;

        //  Получение количества адресов в сети.
        int count = (int)((~(uint)Mask.Value) - 2);

        //  Проверка количества адресов в сети.
        if (count < 2)
        {
            //  Переход к следующему адресу.
            yield break;
        }

        //  Создание списка адресов.
        List<IPv4Address> addresses = new(count);

        //  Заполнение списка адресов.
        for (uint i = 0; i < count; i++)
        {
            //  Получение очередного адреса.
            IPv4Address item = new(addressValue + i + 1);

            //  Проверка адреса.
            if (item != Host)
            {
                //  Добавление в список пдресов.
                addresses.Add(item);
            }
        }

        //  Создание очереди ответов.
        ConcurrentQueue<PingReply> replies = new();

        //  Запуск задачи, выполняющей отправку эхо-запросов.
        Task sender = Task.Run(async delegate
        {
            //  Параллельная работа с адресами.
            await Parallel.ForEachAsync(addresses, cancellationToken, async (address, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Безопасное выполнение.
                await Invoker.CriticalAsync(async (cancellationToken) =>
                {
                    //  Создание средства отправки эхо-запросов.
                    Ping ping = new();

                    //  Отправка запроса.
                    PingReply reply = await ping.SendPingAsync(address.ToString(), timeout).ConfigureAwait(false);

                    //  Проверка ответа.
                    if (reply.Status == IPStatus.Success)
                    {
                        //  Добавление ответа в очередь.
                        replies.Enqueue(reply);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }, cancellationToken);

        //  Цикл ожидания ответов.
        while (!sender.IsCompleted)
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Извлечение сообщений из очереди.
            while (replies.TryDequeue(out PingReply? reply))
            {
                //  Возврат ответа.
                yield return reply;
            }

            //  Ожидание до следующего цикла.
            await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
