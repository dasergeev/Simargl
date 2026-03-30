using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;

namespace Simargl.UI.Nodes.Core;

/// <summary>
/// Представляет панель управления.
/// </summary>
internal class ControlPanel :
    Control
{
    /// <summary>
    /// Поле для хранения ленты.
    /// </summary>
    private readonly Ribbon _Ribbon;

    /// <summary>
    /// Поле для хранения карты элементов, отображающих вкладки.
    /// </summary>
    private readonly Dictionary<Type, NodeTab> _MapTabs;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public ControlPanel()
    {
        //  Создание ленты.
        _Ribbon = new();

        //  Создание элементов, отображающих вкладки.
        _MapTabs = [];

        //  Установка ленты в качестве содержимого панели управления.
        Content = _Ribbon;

        //  Добавление обработчика события загрузки ленты.
        _Ribbon.Loaded += delegate (object sender, RoutedEventArgs e)
        {
            //  Поиск сетки летны.
            if (VisualTreeHelper.GetChild((DependencyObject)sender, 0) is Grid child)
            {
                //  Скрытие первой строки в сетке.
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        };

        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Завершение инициализации.
            return;
        }

        //  Добавление обработчика события.
        Root.SelectedChanged += delegate (object? sender, EventArgs e)
        {
            //  Обновление активного элемента.
            UpdateActiveView();
        };
    }

    /// <summary>
    /// Обновляет активный элемент.
    /// </summary>
    private void UpdateActiveView()
    {
        //  Активный элемент.
        NodeTab? activeTab = null;

        //  Получение активного узла.
        if (Root.Selected is Node node)
        {
            //  Получение типа элемента.
            Type tabType = node.TabSource.GetControlType();

            //  Поиск элемента в карте.
            if (!_MapTabs.TryGetValue(tabType, out activeTab))
            {
                //  Создание активного элемента.
                activeTab = Activator.CreateInstance(tabType) as NodeTab;

                //  Проверка активного элемента.
                if (activeTab is null) throw new InvalidOperationException(
                    $"Не удалось создать элемент, отображающий вкладку панели управления, типа \"{tabType.Name}\".");

                //  Добавление элемента на ленту.
                _Ribbon.Items.Add(activeTab);

                //  Добавление элемента в карту.
                _MapTabs.Add(tabType, activeTab);
            }

            //  Настройка активного элемента.
            activeTab.SetActiveNode(node);
        }

        //  Перебор элементов.
        foreach (NodeTab tab in _MapTabs.Values)
        {
            //  Проверка активного узла.
            if (ReferenceEquals(activeTab, tab))
            {
                //  Показ элемента.
                tab.Visibility = Visibility.Visible;
            }
            else
            {
                //  Скрытие элемента.
                tab.Visibility = Visibility.Collapsed;

                //  Сброс активного узла.
                tab.SetActiveNode(null);
            }
        }
    }
}
