using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Simargl.Embedded.Tunings.TuningsEditor.UI.Controls;

/// <summary>
/// Представляет элемент управления для редактирования атрибута.
/// </summary>
partial class NodeAttributeEditor :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="NodeAttribute"/>.
    /// </summary>
    public event EventHandler? NodeAttributeChanged;

    /// <summary>
    /// Свойство зависимости для <see cref="NodeAttribute"/>.
    /// </summary>
    public static readonly DependencyProperty NodeAttributeProperty =
            DependencyProperty.Register(
                nameof(NodeAttribute),
                typeof(NodeAttribute),
                typeof(NodeAttributeEditor),
                new PropertyMetadata(
                    null,
                    OnNodeAttributePropertyChanged
                )
            );

    /// <summary>
    /// Поле для хранения текущего значения атрибута.
    /// </summary>
    private NodeAttribute? _NodeAttribute;

    /// <summary>
    /// Поле для хранения значения атрибута.
    /// </summary>
    private string _NodeAttributeValue;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public NodeAttributeEditor()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка значения атрибута по умолчанию.
        _NodeAttributeValue = string.Empty;

        //  Добавление обработчика события изменения атрибута.
        NodeAttributeChanged += NodeAttributeEditor_NodeAttributeChanged;

        Binding textBinding = new (nameof(NodeAttributeValue))
        {
            Source = this,
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        textBinding.ValidationRules.Add(new NodeAttributeValidationRule(this));
        ValueTextBox.SetBinding(TextBox.TextProperty, textBinding);
    }

    /// <summary>
    /// Возвращает или задаёт атрибут.
    /// </summary>
    public NodeAttribute? NodeAttribute
    {
        get => (NodeAttribute?)GetValue(NodeAttributeProperty);
        set => SetValue(NodeAttributeProperty, value);
    }

    /// <summary>
    /// Возвращает или задаёт значение атрибута.
    /// </summary>
    public string NodeAttributeValue
    {
        get => _NodeAttributeValue;
        set
        {
            //  Проверка изменения значения.
            if ( _NodeAttributeValue != value )
            {
                //  Установка нового значения.
                _NodeAttributeValue = value;

                //  Проверка текущего атрибута.
                if (NodeAttribute is not null)
                {
                    //  Установка значения.
                    NodeAttribute.Value = value;
                }

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(NodeAttributeValue)));
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="NodeAttributeChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnNodeAttributeChanged(EventArgs e)
    {
        //  Вызов базового события.
        OnPropertyChanged(new(nameof(NodeAttribute)));

        //  Вызов основного события.
        Volatile.Read(ref NodeAttributeChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Обрабатывает изменение свойства зависимости <see cref="NodeAttributeProperty"/>.
    /// </summary>
    /// <param name="d">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private static void OnNodeAttributePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //  Проверка элемента управления.
        if (d is NodeAttributeEditor editor)
        {
            //  Вызов события.
            editor.OnNodeAttributeChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Обрабатывает событие <see cref="NodeAttributeChanged"/>.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void NodeAttributeEditor_NodeAttributeChanged(object? sender, EventArgs e)
    {
        //  Проверка текущего значения атрибута.
        if (_NodeAttribute is not null)
        {
            //  Удаление обработчика события.
            _NodeAttribute.ValueChanged -= NodeAttribute_ValueChanged;
        }

        //  Установка нового значения атрибута.
        _NodeAttribute = NodeAttribute;

        //  Проверка текущего значения атрибута.
        if (_NodeAttribute is not null)
        {
            //  Добавление обработчика события.
            _NodeAttribute.ValueChanged += NodeAttribute_ValueChanged;

            //  Обновление значения свойства.
            NodeAttributeValue = _NodeAttribute.Value;
        }
        else
        {
            //  Сброс значения свойства.
            NodeAttributeValue = string.Empty;
        }
    }

    /// <summary>
    /// Обрабатывает событие <see cref="NodeAttribute.ValueChanged"/>.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void NodeAttribute_ValueChanged(object? sender, EventArgs e)
    {
        //  Проверка объекта, создавшего событие.
        if (sender is NodeAttribute attribute)
        {
            //  Установка нового значения.
            NodeAttributeValue = attribute.Value;
        }
    }
}
