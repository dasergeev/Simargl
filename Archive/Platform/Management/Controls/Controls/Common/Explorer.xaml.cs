using Apeiron.Platform.Management.Models;

namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет обозреватель управления.
/// </summary>
public sealed partial class Explorer :
    UserControl
{
    /// <summary>
    /// Происходит при изменении свойства <see cref="SelectedNode"/>.
    /// </summary>
    public event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Поле для хранения выбранного узла.
    /// </summary>
    private ModelNode? _SelectedNode;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Explorer()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание модели.
        Model = new();

        //  Установка источника для дерева обозревателя.
        _TreeView.ItemsSource = Model.Nodes;
    }

    /// <summary>
    /// Возвращает модель.
    /// </summary>
    public Model Model { get; }

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public ModelNode? SelectedNode
    {
        get => _SelectedNode;
        private set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_SelectedNode, value))
            {
                //  Установка нового значения.
                _SelectedNode = value;

                //  Вызов события, об изменении значения свойства.
                SelectedNodeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие разворачивания узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_Expanded(object sender, RoutedEventArgs e)
    {
        //  Проверка узла.
        if (e.OriginalSource is TreeViewItem item && item.Header is ModelNode node)
        {
            //  Загрузка узла.
            node.Load();
        }
    }

    /// <summary>
    /// Обрабатывает событие изменения выбора элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Изменение выбранного узла.
        SelectedNode = e.NewValue as ModelNode;
    }
}
