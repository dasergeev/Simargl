using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

/// <summary>
/// Представляет коллекцию атрибутов узлов.
/// </summary>
/// <param name="project">
/// Проект.
/// </param>
public sealed class NodeAttributeCollection(Project project) :
    IEnumerable<NodeAttribute>,
    INotifyCollectionChanged
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)_Items).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)_Items).CollectionChanged -= value;
    }

    /// <summary>
    /// Поле для хранения коллекции элементов.
    /// </summary>
    private readonly ObservableCollection<NodeAttribute> _Items = [];

    /// <summary>
    /// Возвращает атрибут с указанным форматом.
    /// </summary>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    /// <returns>
    /// Атрибут с указанным форматом.
    /// </returns>
    public NodeAttribute this[NodeAttributeFormat format] => _Items.First(x => x.Format == format);

    /// <summary>
    /// Добавляет новый атрибут.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат атрибута узла.
    /// </param>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="description">
    /// Описание атрибута.
    /// </param>
    /// <returns>
    /// Новый атрибут.
    /// </returns>
    public NodeAttribute Add(NodeAttributeFormat format, string name, string description)
    {
        //  Добавление атрибута.
        return AddCore(format, name, description);
    }

    /// <summary>
    /// Добавляет новый атрибут.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат атрибута узла.
    /// </param>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="description">
    /// Описание атрибута.
    /// </param>
    /// <param name="values">
    /// Коллекция допустимых значений.
    /// </param>
    /// <returns>
    /// Новый атрибут.
    /// </returns>
    public NodeAttribute Add(NodeAttributeFormat format, string name, string description, IEnumerable<string> values)
    {
        //  Добавление атрибута.
        return AddCore(format, name, description,
            values: values);
    }

    /// <summary>
    /// Добавляет новый атрибут.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат атрибута узла.
    /// </param>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="description">
    /// Описание атрибута.
    /// </param>
    /// <param name="validator">
    /// Метод, выполняющий проверку значения.
    /// </param>
    /// <returns>
    /// Новый атрибут.
    /// </returns>
    public NodeAttribute Add(NodeAttributeFormat format, string name, string description, NodeAttributeValidator validator)
    {
        //  Добавление атрибута.
        return AddCore(format, name, description,
            validator: validator);
    }

    /// <summary>
    /// Добавляет новый атрибут.
    /// </summary>
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
    /// <returns>
    /// Новый атрибут.
    /// </returns>
    public NodeAttribute Add(
        NodeAttributeFormat format,
        string name, string description,
        string defaultValue,
        NodeAttributeValidator validator)
    {
        //  Добавление атрибута.
        return AddCore(format, name, description,
            defaultValue: defaultValue,
            validator: validator);
    }

    /// <summary>
    /// Добавляет новый атрибут.
    /// </summary>
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
    /// <returns>
    /// Новый атрибут.
    /// </returns>
    private NodeAttribute AddCore(
        NodeAttributeFormat format,
        string name, string description,
        string? defaultValue = null,
        NodeAttributeValidator? validator = null,
        IEnumerable<string>? values = null)
    {
        //  Создание атрибута.
        NodeAttribute attribute = new(project, format, name, description, defaultValue, validator, values);

        //  Проверка формата.
        if (_Items.Any(x => x.Format == format))
        {
            //  Выброс исключения.
            throw new InvalidOperationException(
                $"Произошла попытка добавить второй атрибут с форматом {format}");
        }

        //  Добавление в список элементов.
        _Items.Add(attribute);

        //  Возврат атрибута.
        return attribute;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<NodeAttribute> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<NodeAttribute>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
