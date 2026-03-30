using Simargl.Hardware.Strain.Demo.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет обозревателя.
/// </summary>
partial class Explorer
{
    /// <summary>
    /// Поле для хранения таймера инициализации.
    /// </summary>
    private readonly DispatcherTimer _InitializeTimer;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Explorer()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание таймера инициализации.
        _InitializeTimer = new()
        {
            Interval = TimeSpan.FromMilliseconds(100),
        };

        //  Добавление обработчика события таймера.
        _InitializeTimer.Tick += InitializeTimer_Tick;

        //  Запуск таймера.
        _InitializeTimer.Start();
    }

    /// <summary>
    /// Обрабатывает событие таймера.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void InitializeTimer_Tick(object? sender, EventArgs e)
    {
        //  Проверка количества элементов.
        if (_TreeView.Items.Count > 0)
        {
            //  Получение первого элемента.
            object firstItem = _TreeView.Items[0];

            //  Поиск контейнера.
            if (_TreeView.ItemContainerGenerator.ContainerFromItem(firstItem) is TreeViewItem container)
            {
                //  Выбор элемента.
                container.IsSelected = true;

                //  Остановка таймера.
                _InitializeTimer.Stop();
            }
        }
    }

    /// <summary>
    /// Происходит при изменении выбранного элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Установка узла.
            Heart.SetSelectedNode(e.NewValue as Node);
        }
        catch { }
    }
}
