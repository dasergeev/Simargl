using Apeiron.Platform.Demo.AdxlDemo.Nodes;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет основной элемент управления.
/// </summary>
public partial class MainControl :
    UserControl
{
    /// <summary>
    /// Поле для хранения элементов управления, отображающих информацию по узлу.
    /// </summary>
    private readonly List<NodeView> _NodeViews;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MainControl()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание массива элементов управления.
        _NodeViews = new()
        {
            _ChannelView,
            _ChannelCollectionView,
            _DeviceView,
            _DeviceCollectionView,
        };

        //  Настройка видимости элементов.
        _NodeViews.ForEach(view => view.Visibility = Visibility.Collapsed);
    }

    /// <summary>
    /// Устанавливает новый текущий узел.
    /// </summary>
    /// <param name="node">
    /// Новый текущий узел.
    /// </param>
    public void SetNode(INode? node)
    {
        //  Активный элемент управления.
        NodeView? active = null;

        //  Провека выбранного узла.
        if (node is not null)
        {
            //  Определение активного элемента управления.
            active = node.NodeFormat switch
            {
                NodeFormat.Root => null,
                NodeFormat.Network => null,
                NodeFormat.NetworkCollection => null,
                NodeFormat.AdxlDevice => _DeviceView,
                NodeFormat.AdxlDeviceCollection => _DeviceCollectionView,
                NodeFormat.ChannelOrganizer => _ChannelView,
                NodeFormat.ChannelOrganizerCollection => _ChannelCollectionView,
                _ => null,
            };
        }

        //  Сброс неактивных элементов управления.
        _NodeViews.ForEach(view =>
        {
            //  Проверка ссылки.
            if (!ReferenceEquals(view, active))
            {
                //  Сброс выбранного узла.
                view.Node = null;
            }
        });

        //  Настройка активного элемента.
        if (active is not null)
        {
            //  Установка выбранного узла.
            active.Node = node;
        }
    }
}
