using Apeiron.Platform.Demo.AdxlDemo.Adxl;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет коллекцию организаторов каналов.
/// </summary>
public sealed class ChannelOrganizerCollection :
    DictionaryNode<string, ChannelOrganizer>
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
    /// Поле для хранения длительности отображаемого фрагмента в секундах.
    /// </summary>
    private readonly float _Duration;

    /// <summary>
    /// Поле для хранения значения, определяющего, задаётся ли время отображения.
    /// </summary>
    private bool _IsCustomTime;

    /// <summary>
    /// Поле для хранения времени начала отображаемого фрагмента.
    /// </summary>
    private DateTime _BeginTime;

    /// <summary>
    /// Поле для хранения коллекции групп каналов.
    /// </summary>
    private ChannelGroupCollection? _Groups;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public ChannelOrganizerCollection(Engine engine) :
        base(engine, "Каналы", NodeFormat.ChannelOrganizerCollection, organizer => organizer.Name)
    {
        //  Создание очереди сообщений.
        _Packages = new();

        //  Установка параметров отображения.
        _Duration = 60;
        _IsCustomTime = false;
        _BeginTime = DateTime.Now;
    }

    /// <summary>
    /// Возвращает количество каналов.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Количество")]
    [Description("Количество каналов.")]
    public int Count => Nodes.Count;

    /// <summary>
    /// Возвращает длительности отображаемого фрагмента в секундах.
    /// </summary>
    [Browsable(false)]
    public double Duration => _Duration;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее, задаётся ли время отображения.
    /// </summary>
    [Browsable(false)]
    public bool IsCustomTime
    {
        get => _IsCustomTime;
        set
        {
            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_IsCustomTime != value)
                {
                    //  Установка значения.
                    _IsCustomTime = value;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(IsCustomTime)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает минимальное время данных.
    /// </summary>
    [Browsable(false)]
    public DateTime MinTime => Nodes.Select(organizer => organizer.MinTime).Min();

    /// <summary>
    /// Возвращает или задаёт время начала отображаемого фрагмента.
    /// </summary>
    [Browsable(false)]
    public DateTime BeginTime
    {
        get
        {
            //  Проверка ручного режима.
            if (!_IsCustomTime)
            {
                //  Корректировка текущего значения.
                _BeginTime = DateTime.Now - TimeSpan.FromSeconds(Duration);
            }

            //  Возврат текущего значения.
            return _BeginTime;
        }
        set
        {
            //  Проверка необходимости изменения времени.
            if (_BeginTime != value)
            {
                //  Установка ручного режима.
                IsCustomTime = Math.Abs((_BeginTime - value).TotalMilliseconds) > 100;

                //  Установка текущего значения времени.
                _BeginTime = value;
            }
        }
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="package"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task RegistrationAsync(AdxlExtendedPackage package, CancellationToken cancellationToken)
    {
        //  Проверка пакета.
        IsNotNull(package, nameof(package));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление пакета в очередь.
        _Packages.Enqueue(package);
    }

    /// <summary>
    /// Возвращает коллекцию групп каналов.
    /// </summary>
    /// <returns>
    /// Коллекция групп каналов.
    /// </returns>
    public ChannelGroupCollection GetGroups()
    {
        //  Выполнение в основном потоке.
        return Invoker.Primary(delegate
        {
            //  Проверка текущей коллекции.
            if (_Groups is null || _Groups.Count != 3)
            {
                //  Проверка количество узлов.
                if (Nodes.Count == 21)
                {
                    //  Создание коллекции.
                    _Groups = new(new ChannelGroup[]
                    {
                        new(Engine, 0, Nodes[0].GetSerialNumber(), new ChannelOrganizer[]
                        {
                            Nodes[0], Nodes[1], Nodes[2], Nodes[3], Nodes[4], Nodes[5], Nodes[6]
                        }),
                        new(Engine, 1, Nodes[7].GetSerialNumber(), new ChannelOrganizer[]
                        {
                            Nodes[7], Nodes[8], Nodes[9], Nodes[10], Nodes[11], Nodes[12], Nodes[13]
                        }),
                        new(Engine, 2, Nodes[14].GetSerialNumber(), new ChannelOrganizer[]
                        {
                            Nodes[14], Nodes[15], Nodes[16], Nodes[17], Nodes[18], Nodes[19], Nodes[20]
                        }),
                    });
                }
                else
                {
                    //  Создание пустой коллекции.
                    _Groups ??= new(Array.Empty<ChannelGroup>());
                }
            }

            //  Возврат коллекции.
            return _Groups;
        });
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

        //  Значение, определяющее, проверена ли база данных.
        bool isValidation = false;

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасное выполнение.
            await Invoker.SystemAsync(async (cancellationToken) =>
            {
                //  Проверка необходимости проверки базы данных.
                if (!isValidation)
                {
                    //  Проверка базы данных.
                    await ValidationDatabaseAsync(cancellationToken).ConfigureAwait(false);

                    //  Установка флага.
                    isValidation = true;
                }

                //  Перебор всех полученных пакетов.
                while (!cancellationToken.IsCancellationRequested &&
                    _Packages.TryDequeue(out AdxlExtendedPackage? package))
                {
                    //  Уведомление всех каналов.
                    await Parallel.ForEachAsync(
                        Nodes,
                        cancellationToken,
                        async (node, cancellationToken) =>
                        {
                            //  Уведомление узла.
                            await node.NotificationAsync(package, cancellationToken).ConfigureAwait(false);
                        }).ConfigureAwait(false);
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед повторным сканированием.
            await Task.Delay(_InvokeDelay, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет проверку базы данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проыерку базы данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task ValidationDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор номеров датчиков.
        for (int deviceNumber = 0; deviceNumber < 3; deviceNumber++)
        {
            //  Определение серийного номера датчика.
            long serialNumber = deviceNumber switch
            {
                0 => 0x6A00BEFF,
                1 => 0x6B008A0F,
                _ => 0x6B00282E,
            };

            //  Синхронные каналы.
            ChannelType channelType = ChannelType.Sync;
            double sampling = 1000;
            double cutoff = 250;

            await considerAsync("UX", 0, cancellationToken).ConfigureAwait(false);
            await considerAsync("UY", 1, cancellationToken).ConfigureAwait(false);
            await considerAsync("UZ", 2, cancellationToken).ConfigureAwait(false);

            //  Асинхронные каналы.
            channelType = ChannelType.Async;
            sampling = 20;
            cutoff = 5;

            await considerAsync("CPUTemp", 0, cancellationToken).ConfigureAwait(false);
            await considerAsync("DeviceTemp", 1, cancellationToken).ConfigureAwait(false);
            await considerAsync("CPUVoltage", 2, cancellationToken).ConfigureAwait(false);

            //  Информационные каналы.
            channelType = ChannelType.Info;
            sampling = 20;
            cutoff = 5;

            await considerAsync("Diagnostic", 0, cancellationToken).ConfigureAwait(false);

            //  Проверяет канал.
            async Task considerAsync(string channelName, int signalNumber, CancellationToken cancellationToken)
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Корректировка имени канала.
                channelName = $"{channelName}{deviceNumber}";

                //  Запрос канала.
                ChannelInfo? channelInfo = await Engine.ContextManager.RequestAsync(
                    async (context, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Поиск в базе данных.
                        return await context.ChannelInfos
                            .Where(channel => channel.Name == channelName)
                            .Include(channel => channel.ChannelFragmentInfos)
                            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                    }, cancellationToken).ConfigureAwait(false);

                //  Проверка канала.
                if (channelInfo is null)
                {
                    //  Создание канала.
                    channelInfo = new()
                    {
                        Name = channelName,
                        SerialNumber = serialNumber,
                        ChannelType = channelType,
                        SignalNumber = signalNumber,
                        Sampling = sampling,
                        Cutoff = cutoff,
                    };

                    //  Выполнение транзакции в базу данных.
                    await Engine.ContextManager.TransactionAsync(
                        async (context, cancellationToken) =>
                        {
                            //  Проверка токена отмены.
                            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                            //  Добавление информации в базу данных.
                            await context.ChannelInfos.AddAsync(channelInfo, cancellationToken).ConfigureAwait(false);
                        }, cancellationToken).ConfigureAwait(false);
                }
                //else
                //{
                //    //  Обновление канала.
                //    await Engine.ContextManager.TransactionAsync(
                //        async (context, cancellationToken) =>
                //        {
                //            //  Проверка токена отмены.
                //            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //            //  Корректировка значений.
                //            channelInfo.SerialNumber = serialNumber;
                //            channelInfo.Sampling = sampling;
                //            channelInfo.Cutoff = cutoff;
                //        }, cancellationToken).ConfigureAwait(false);
                //}

                //  Обновление узла.
                await AddOrUpdateAsync(channelName,
                    () => new ChannelOrganizer(Engine, channelInfo),
                    organizer => { }, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
