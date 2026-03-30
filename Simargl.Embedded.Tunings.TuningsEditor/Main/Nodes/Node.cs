using Simargl.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Images;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет узел.
/// </summary>
public abstract class Node :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения родительского узла.
    /// </summary>
    private Node? _Parent;

    /// <summary>
    /// Поле для хранения изображения.
    /// </summary>
    private ImageSource _Image;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Формат узла.
    /// </param>
    /// <param name="project">
    /// Проект.
    /// </param>
    public Node(NodeFormat format, Project project)
    {
        //  Установка формата узла.
        Format = format;

        //  Установка проекта.
        Project = project;

        //  Создание коллекции атрибутов.
        Attributes = new(project);

        //  Создание атрибутов.
        Name = Attributes.Add(
            NodeAttributeFormat.Name,
            "Имя",
            "Отображаемое имя узла в проекте.");
        Description = Attributes.Add(
            NodeAttributeFormat.Description,
            "Описание",
            "Описание узла, которое используется в спецификации и в генерируемом коде.");
        Identifier = Attributes.Add(
            NodeAttributeFormat.Identifier,
            "Идентификатор",
            "Идентификатор узла, который используется в спецификации и в генерируемом коде.",
            "Identifier",
            SyntaxValidator.IsIdentifier);

        //  Создание команд.
        RemoveSectionCommand = new(IsMovable, delegate
        {
            //  Вызов диалогового окна.
            if (MessageBox.Show(
                "Вы уверены, что хотите удалить узел?",
                "Узел",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //  Удаление узла.
                Remove();
            }
        });

        //  Создание коллекции дочерниху узлов.
        Nodes = new(this);

        //  Добавление обработчика события изменения коллекции.
        Nodes.CollectionChanged += delegate (object? sender, NotifyCollectionChangedEventArgs e)
        {
            //  Установка значения, определяющего требуется ли сохранить проект.
            Project.IsNeedSaving = true;
        };

        //  Установка изображения.
        _Image = ImageMap.GetImage(format);
    }

    /// <summary>
    /// Возвращает формат узла.
    /// </summary>
    public NodeFormat Format { get; }

    /// <summary>
    /// Возвращает проект.
    /// </summary>
    public Project Project { get; }

    /// <summary>
    /// Возвращает коллекцию атрибутов.
    /// </summary>
    public NodeAttributeCollection Attributes { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий имя.
    /// </summary>
    public NodeAttribute Name { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий описание.
    /// </summary>
    public NodeAttribute Description { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий идентификатор.
    /// </summary>
    public NodeAttribute Identifier { get; }

    /// <summary>
    /// Возвращает или задаёт родительской узел.
    /// </summary>
    public Node? Parent
    {
        get => _Parent;
        set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_Parent, value))
            {
                //  Установка нового значения.
                _Parent = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Parent)));
            }
        }
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    public ImageSource Image
    {
        get => _Image;
        set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_Image, value))
            {
                //  Установка нового значения.
                _Image = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Image)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли узел перемещаемым.
    /// </summary>
    public bool IsMovable => NodeHelper.IsMovable(Format);

    /// <summary>
    /// Возвращает значение, определяющее, может ли узел содержать данный.
    /// </summary>
    /// <param name="node">
    /// Узел для проверки.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public bool CanContain(Node node)
    {
        return NodeHelper.CanContain(Format, node.Format);
    }

    /// <summary>
    /// Сохраняет данные в файл.
    /// </summary>
    /// <param name="writer">
    /// Средство записи данных.
    /// </param>
    public void Save(Utf8JsonWriter writer)
    {
        //  Начало массива атрибутов.
        writer.WriteStartArray("attributes");

        //  Перебор атрибутов.
        foreach (NodeAttribute attribute in Attributes)
        {
            //  Начало объекта.
            writer.WriteStartObject();

            //  Запись параметров атрибута.
            writer.WriteNumber("format", (int)attribute.Format);
            writer.WriteString("value", attribute.Value);

            //  Завершение объекта.
            writer.WriteEndObject();
        }

        //  Завершение массива атрибутов.
        writer.WriteEndArray();

        //  Начало массива узлов.
        writer.WriteStartArray("nodes");

        //  Перебор дочерних узлов.
        foreach (Node node in Nodes)
        {
            //  Начало объекта.
            writer.WriteStartObject();

            //  Запись формата узла.
            writer.WriteNumber("__node_format", (int)node.Format);

            //  Сохранение узла.
            node.Save(writer);

            //  Завершение объекта.
            writer.WriteEndObject();
        }

        //  Завершение массива узлов.
        writer.WriteEndArray();
    }

    /// <summary>
    /// Загружает данные.
    /// </summary>
    /// <param name="element">
    /// Элемент, из которого необходимо загрузить данные.
    /// </param>
    public void Load(JsonElement element)
    {
        //  Перебор атрибутов.
        foreach (JsonElement item in element.GetProperty("attributes").EnumerateArray())
        {
            //  Чтение значений.
            NodeAttributeFormat format = (NodeAttributeFormat)item.GetProperty("format").GetInt32();
            string value = item.GetProperty("value").GetString() ?? throw new InvalidDataException("Неверный формат файла.");

            //  Блок перехвата всех исключений.
            try
            {
                //  Установка значения атрибута.
                Attributes[format].Value = value;
            }
            catch (Exception ex)
            {
                //  Повторный выброс исключения.
                throw new InvalidDataException("Неверный формат файла.", ex);
            }
        }

        //  Перебор узлов.
        foreach (JsonElement item in element.GetProperty("nodes").EnumerateArray())
        {
            //  Чтение формата узла.
            NodeFormat format = (NodeFormat)item.GetProperty("__node_format").GetInt32();

            //  Создание узла.
            Node node = NodeHelper.Create(format, Project);

            //  Заргузка узла.
            node.Load(item);

            //  Добавление узла.
            Nodes.Add(node);
        }
    }

    /// <summary>
    /// Возвращает команду удаления раздела информации.
    /// </summary>
    public Command RemoveSectionCommand { get; }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    public NodeCollection Nodes { get; }

    /// <summary>
    /// Удаляет узел.
    /// </summary>
    public void Remove()
    {
        //  Проверка родительского узла.
        if (Parent is Node node)
        {
            //  Удаление узла.
            node.Nodes.Remove(this);
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

        //  Установка значения, определяющего требуется ли сохранить проект.
        Project.IsNeedSaving = true;
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public abstract int GetDataSize();
}
