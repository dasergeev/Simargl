using Apeiron.Platform.Demo.AdxlDemo.Nodes;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет обозревателя.
/// </summary>
public partial class Explorer :
    UserControl
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="SelectedNode"/>.
    /// </summary>
    public event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Поле для хранения выбранного узла.
    /// </summary>
    private INode? _SelectedNode;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Explorer()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка источника данных.
        _TreeView.ItemsSource = ((App)Application.Current).Engine.Root.Nodes;
    }

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public INode? SelectedNode
    {
        get => _SelectedNode;
        private set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_SelectedNode, value))
            {
                //  Установка нового значения свойства.
                _SelectedNode = value;

                //  Вызов события об изменении значения свойства.
                OnSelectedNodeChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="SelectedNodeChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnSelectedNodeChanged(EventArgs e)
    {
        //  Вызов события.
        SelectedNodeChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Происходит при изменении выбора элемента в дереве.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Получение выбранного узла.
        SelectedNode = e.NewValue as INode;
    }
}
