using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий свойства.
/// </summary>
public partial class PropertiesView :
    UserControl
{
    /// <summary>
    /// Поле для хранения сетки свойств.
    /// </summary>
    private readonly System.Windows.Forms.PropertyGrid _PropertyGrid;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PropertiesView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание сетки свойств.
        _PropertyGrid = new()
        {
            ToolbarVisible = false,
            PropertySort = System.Windows.Forms.PropertySort.Categorized,
        };

        //  Установка сетки свойств.
        _Host.Child = _PropertyGrid;
    }

    /// <summary>
    /// Возвращает или задаёт выбранный объект.
    /// </summary>
    public object? SelectedObject
    {
        get => _PropertyGrid.SelectedObject;
        set
        {
            //  Проверка необходимости изменения объекта.
            if (!ReferenceEquals(_PropertyGrid.SelectedObject, value))
            {
                //  Проверка предыдущего значения.
                if (_PropertyGrid.SelectedObject is INotifyPropertyChanged oldValue)
                {
                    //  Удаление обработчика события.
                    oldValue.PropertyChanged -= node_PropertyChanged;
                }

                //  Установка нового значения.
                _PropertyGrid.SelectedObject = value;

                //  Проверка нового значения.
                if (_PropertyGrid.SelectedObject is INotifyPropertyChanged newValue)
                {
                    //  Добавление обработчика события.
                    newValue.PropertyChanged += node_PropertyChanged;
                }
            }

            //  Обработчик события изменения значения свойства..
            void node_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                //  Обновление сетки свойств.
                _PropertyGrid.Refresh();
            }
        }
    }
}
