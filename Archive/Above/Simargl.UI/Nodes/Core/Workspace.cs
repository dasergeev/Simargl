using System.Collections.Generic;
using System.Windows.Controls;

namespace Simargl.UI.Nodes.Core;

/// <summary>
/// Представляет рабочее пространство.
/// </summary>
internal partial class Workspace :
    Control
{
    /// <summary>
    /// Поле для хранения сетки для размещения элементов управления, отображающих данные узлов.
    /// </summary>
    private readonly Grid _Grid;

    /// <summary>
    /// Поле для хранения карты элементов, отображающих узлы.
    /// </summary>
    private readonly Dictionary<Type, NodeView> _MapViews;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Workspace()
    {
        //  Создание сетки.
        _Grid = new();

        //  Установка сетки, как содержимого рабочего пространства.
        Content = _Grid;

        //  Создание элементов, отображающих узлы.
        _MapViews = [];

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
        NodeView? activeView = null;

        //  Получение активного узла.
        if (Root.Selected is Node node)
        {
            //  Получение типа элемента.
            Type viewType = node.ViewSource.GetControlType();

            //  Поиск элемента в карте.
            if (!_MapViews.TryGetValue(viewType, out activeView))
            {
                //  Создание активного элемента.
                activeView = Activator.CreateInstance(viewType) as NodeView;

                //  Проверка активного элемента.
                if (activeView is null) throw new InvalidOperationException(
                    $"Не удалось создать элемент, отображающий узел, типа \"{viewType.Name}\".");

                //  Добавление узла в сетку.
                _Grid.Children.Add(activeView);

                //  Добавление элемента в карту.
                _MapViews.Add(viewType, activeView);
            }

            //  Настройка активного элемента.
            activeView.SetActiveNode(node);
        }

        //  Перебор элементов.
        foreach (NodeView view in _MapViews.Values)
        {
            //  Проверка активного узла.
            if (ReferenceEquals(activeView, view))
            {
                //  Показ элемента.
                view.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //  Скрытие элемента.
                view.Visibility = System.Windows.Visibility.Collapsed;

                //  Сброс активного узла.
                view.SetActiveNode(null);
            }
        }
    }
}
