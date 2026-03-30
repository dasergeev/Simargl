using Simargl.Hardware.Strain.Demo.Nodes;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет рабочее пространство.
/// </summary>
partial class Workspace
{
    /// <summary>
    /// Поле для хранения списка элементов, отображающих узлы.
    /// </summary>
    private readonly List<NodeView> _Views;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Workspace()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание списка элементов, отображающих узлы.
        _Views =
        [
            _EquipmentView,
            _SensorView,
            _RecorderView,
        ];

        //  Настройка обработчика события.
        SelectedNodeChanged += delegate (object? sender, EventArgs e)
        {
            //  Обновление элементов, отображающих узлы.
            UpdateViews();
        };

        //  Обновление элементов, отображающих узлы.
        UpdateViews();
    }

    /// <summary>
    /// Обновляет элементы, отображающие узлы.
    /// </summary>
    private void UpdateViews()
    {
        //  Проверка целевого узла.
        if (SelectedNode is Node node)
        {
            //  Получение типа узла.
            Type type = node.GetType();

            //  Настройка элементов.
            _Views.ForEach(x => x.Visibility = x.TargetNodeType == type ? Visibility.Visible : Visibility.Hidden);
        }
        else
        {
            //  Скрытие всех элементов.
            _Views.ForEach(x => x.Visibility = Visibility.Hidden);
        }
    }
}
