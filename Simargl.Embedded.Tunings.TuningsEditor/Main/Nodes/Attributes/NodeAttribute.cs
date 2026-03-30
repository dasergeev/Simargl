using System.ComponentModel;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

/// <summary>
/// Представляет атрибут.
/// </summary>
public sealed class NodeAttribute :
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения аргументов для события <see cref="PropertyChanged"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _ValueChangedEventArgs = new(nameof(Value));

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Value"/>.
    /// </summary>
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Поле для хранения метода, проверяющего значение.
    /// </summary>
    private readonly NodeAttributeValidator _Validator;

    /// <summary>
    /// Поле для хранения значения.
    /// </summary>
    private string _Value;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    /// <param name="format">
    /// Значение, определяющее формат атрибута узла.
    /// </param>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="description">
    /// Описание атрибута.
    /// </param>
    /// <param name="defaultValue">
    /// Значение по умолчанию.
    /// </param>
    /// <param name="validator">
    /// Метод, выполняющий проверку значения.
    /// </param>
    /// <param name="values">
    /// Коллекция допустимых значений.
    /// </param>
    internal NodeAttribute(
        Project project,
        NodeAttributeFormat format,
        string name, string description,
        string? defaultValue = null,
        NodeAttributeValidator? validator = null,
        IEnumerable<string>? values = null)
    {
        //  Установка значения, определяющего формат атрибута узла.
        Format = format;

        //  Установка имени атрибута.
        Name = name;

        //  Установка описания атрибута.
        Description = description;

        //  Установка значения по умолчанию.
        DefaultValue = defaultValue ?? string.Empty;

        //  Установка методоа, проверяющего значение.
        _Validator = validator ?? DefaultValidator;

        //  Установка коллекции допустимых значений.
        Values = values;

        //  Установка значения по умолчанию.
        _Value = Values?.FirstOrDefault() ?? DefaultValue;

        //  Установка значения, определяющего является ли атрибут перечислением.
        IsEnum = Values is not null;

        //  Добавление обработчика события.
        ValueChanged += delegate (object? sender, EventArgs e)
        {
            //  Установка значения, определяющего требуется ли сохранить проект.
            project.IsNeedSaving = true;
        };
    }

    /// <summary>
    /// Возвращает значение, определяющее формат атрибута узла.
    /// </summary>
    public NodeAttributeFormat Format { get; }

    /// <summary>
    /// Возвращает имя атрибута.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает описание атрибута.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Возвращает значение по умолчанию.
    /// </summary>
    public string DefaultValue { get; }

    /// <summary>
    /// Возвращает или задаёт значение.
    /// </summary>
    public string Value
    {
        get => _Value;
        set
        {
            //  Проверка изменения значения.
            if (_Value != value)
            {
                //  Проверка нового значения.
                if (!IsValid(value))
                {
                    //  Выброс исключения.
                    throw new InvalidOperationException("Передано недопустимое значение свойства.");
                }

                //  Установка нового значения.
                _Value = value;

                //  Вызов события об изменении значения свойства.
                OnValueChanged(_ValueChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Возвращает коллекцию допустимых значений.
    /// </summary>
    public IEnumerable<string>? Values { get; }

    /// <summary>
    /// Возвращает значение, определяющее является ли атрибут перечислением.
    /// </summary>
    public bool IsEnum { get; }

    /// <summary>
    /// Возвращает значение, определяющее видимость текстового блока.
    /// </summary>
    public Visibility TextBoxVisibility => !IsEnum ? Visibility.Visible : Visibility.Collapsed;

    /// <summary>
    /// Возвращает значение, определяющее видимость блока выбора.
    /// </summary>
    public Visibility ComboBoxVisibility => IsEnum ? Visibility.Visible : Visibility.Collapsed;

    /// <summary>
    /// Проверяет значение.
    /// </summary>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public bool IsValid(string value)
    {
        //  Проверка значения.
        return _Validator(value);
    }

    /// <summary>
    /// Метод проверяющий значение по умолчанию.
    /// </summary>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    private static bool DefaultValidator(string value)
    {
        //  Любая строка - действительное значение.
        return true;
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="ValueChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnValueChanged(EventArgs e)
    {
        //  Вызов базового события.
        OnPropertyChanged(new(nameof(Value)));

        //  Вызов основного события.
        Volatile.Read(ref ValueChanged)?.Invoke(this, e);
    }
}
