using Apeiron.Platform.Demo.AdxlDemo.Adxl;
using Apeiron.Platform.Demo.AdxlDemo.Channels;
using Apeiron.Platform.Demo.AdxlDemo.Net;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет корневой узел приложения.
/// </summary>
public sealed class Root :
    Node<INode>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Root(Engine engine) :
        base(engine, nameof(Root), NodeFormat.Root)
    {
        //  Создание узлов.
        Networks = new(engine);
        Devices = new(engine);
        Channels = new(engine);
    }

    /// <summary>
    /// Возвращает узел, содержащий коллекцию сетей.
    /// </summary>
    public NetworkCollection Networks { get; }

    /// <summary>
    /// Возвращает узел, содержащий коллекцию датчиков.
    /// </summary>
    public AdxlDeviceCollection Devices { get; }

    /// <summary>
    /// Возвращает узел, содержащий коллекцию организаторов каналов.
    /// </summary>
    public ChannelOrganizerCollection Channels { get; }

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

        //  Выполнение в основном потоке.
        await PrimaryInvokeAsync(nodes =>
        {
            //  Добавление узлов.
            nodes.Add(Networks);
            nodes.Add(Devices);
            nodes.Add(Channels);
        }, cancellationToken).ConfigureAwait(false);
    }
}
