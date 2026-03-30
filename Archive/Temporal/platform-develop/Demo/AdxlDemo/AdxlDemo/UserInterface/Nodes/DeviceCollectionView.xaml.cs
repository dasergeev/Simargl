using Apeiron.Platform.Demo.AdxlDemo.Channels;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, управляющий коллекцией устройств.
/// </summary>
partial class DeviceCollectionView
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public DeviceCollectionView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка источника устройств.
        _DeviceListView.ItemsSource = Engine.Root.Devices.Nodes;

        //  Установка источника каналов.
        _ChannelListView.ItemsSource = Engine.Root.Channels.GetGroups();

        //  Подписка на событие изменения коллекции каналов.
        Engine.Root.Channels.Nodes.CollectionChanged += (sender, e) =>
        {
            //  Получение коллекции груп.
            ChannelGroupCollection channelSource = Engine.Root.Channels.GetGroups();

            //  Проверка необходимости изменения источника каналов.
            if (!ReferenceEquals(_ChannelListView.ItemsSource, channelSource))
            {
                //  Установка нового источника.
                _ChannelListView.ItemsSource = channelSource;
            }
        };
    }

    /// <summary>
    /// Асинхронно выполняет новый отсчёт.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполнящая новый отсчёт.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task TickAsync(INode node, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        ////  Источники.
        ////IEnumerable deviceSource = null!;
        //IEnumerable channelSource = null!;

        ////  Получение коллекции датчиков.
        //if (node is AdxlDeviceCollection devices)
        //{
        //    //  Установка источников.
        //    //deviceSource = devices.Nodes;
        //    channelSource = Engine.Root.Channels.GetGroups();
        //}

        ////  Выполнение в основном потоке.
        //Invoker.Primary(delegate
        //{
        //    ////  Проверка необходимости изменения источника датчиков.
        //    //if (!ReferenceEquals(_DeviceListView.ItemsSource, deviceSource))
        //    //{
        //    //    //  Установка нового источника.
        //    //    _DeviceListView.ItemsSource = deviceSource;
        //    //}

        //});
    }

    /// <summary>
    /// Происходит при нажатии клавиши мыши.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //  Проверка данных.
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed &&
            sender is FrameworkElement element &&
            element.Tag is long serialNumber)
        {
            //  Начало перетаскивания.
            DragDrop.DoDragDrop(element, serialNumber, DragDropEffects.Copy);
        }
    }

    /// <summary>
    /// Происходит при перетаскивании элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Number_Drop(object sender, DragEventArgs e)
    {
        //  Безопасный вызов.
        Invoker.Critical(delegate
        {
            //  Проверка данных.
            if (sender is FrameworkElement element &&
                element.Tag is int groupNumber &&
                e.Data.GetData(typeof(long)) is long serialNumber)
            {
                //  Запрос группы каналов.
                ChannelGroup? group = Engine.Root.Channels.GetGroups()
                .Where(group => group.Number == groupNumber)
                .FirstOrDefault();

                //  Проверка полученной группы каналов.
                if (group is not null)
                {
                    //  Установка серийного номера датчика.
                    group.SerialNumber = serialNumber;
                }
            }
        });
    }
}
