namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет рабочее пространство.
/// </summary>
public sealed class WorkSpace :
    UserControl
{
    /// <summary>
    /// Поле для хранения коллекции рабочих элементов управления.
    /// </summary>
    private readonly WorkControlCollection _Controls;

    /// <summary>
    /// Поле для хранения текущего рабочего элемента управления.
    /// </summary>
    private WorkControl _CurrentControl;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public WorkSpace()
    {
        //  Создание коллекции рабочих элементов управления.
        _Controls = new(this);

        //  Установка текущего рабочего элемента управления.
        _CurrentControl = _Controls[WorkControlFormat.Default];

        //  Настройка текущего рабочего элемента управления.
        _CurrentControl.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Устанавливает текущий узел.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    internal void SetNode(ModelNode? node)
    {
        //  Определение формата для рабочего элемента управления.
        WorkControlFormat format = node is not null ? node.ControlFormat : WorkControlFormat.Default;

        //  Определение рабочего элемента управления.
        WorkControl control = _Controls[format];

        //  Проверка изменения рабочего элемента управления.
        if (!ReferenceEquals(_CurrentControl, control))
        {
            //  Выгрузка текущего рабочего элемента управления.
            _CurrentControl.Unload();

            //  Установка нового рабочего элемента управления.
            _CurrentControl = control;

            //  Загрузка нового рабочего элемента управления.
            _CurrentControl.Load(node);
        }
        else
        {
            //  Изменение узла.
            _CurrentControl.SetNode(node);
        }
    }
}
