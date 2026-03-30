using Simargl.Engine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Simargl.AccelEth3T.AccelEth3TViewer.Nodes;

/// <summary>
/// Представляет узел.
/// </summary>
public class Node :
    Something,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private volatile string _Name;

    /// <summary>
    /// Поле для хранения значения, определяющего выбран ли узел.
    /// </summary>
    private volatile bool _IsSelected;

    /// <summary>
    /// Поле для хранения значения, определяющего развёрнут ли узел.
    /// </summary>
    private volatile bool _IsExpanded;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    public Node(Node? parent, string name)
    {
        //  Установка родительского узла.
        Parent = parent;

        //  Установка имени.
        _Name = IsNotNull(name);

        //  Создание поставщика коллекции дочерних узлов.
        Provider = [];

        //  Создание коллекции дочерних узлов.
        Nodes = new(Provider);
    }

    /// <summary>
    /// Возвращает родительский узел.
    /// </summary>
    public Node? Parent { get; }

    /// <summary>
    /// Возвращает поставщика коллекции дочерних узлов.
    /// </summary>
    protected ObservableCollection<Node> Provider { get; }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    public NodeCollection Nodes { get; }

    /// <summary>
    /// Возвращает или задаёт имя узла.
    /// </summary>
    public string Name
    {
        get => _Name;
        set
        {
            //  Проверка нового значения.
            IsNotNull(value, nameof(Name));

            //  Проверка изменения значения.
            if (_Name != value)
            {
                //  Установка нового значения.
                _Name = value;

                //  Вызов события об изменении значения.
                OnPropertyChanged(new(nameof(Name)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее выбран ли узел.
    /// </summary>
    public bool IsSelected
    {
        get => _IsSelected;
        set
        {
            //  Проверка изменения значения.
            if (_IsSelected != value)
            {
                //  Установка нового значения.
                _IsSelected = value;

                //  Проверка необходимости развернуть все узлы до текущего.
                if (_IsSelected)
                {
                    //  Разворачивание всех узлов до текущего.
                    ExpandParents();
                }

                //  Вызов события об изменении значения.
                OnPropertyChanged(new(nameof(IsSelected)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее развёрнут ли узел.
    /// </summary>
    public bool IsExpanded
    {
        get => _IsExpanded;
        set
        {
            //  Проверка изменения значения.
            if (_IsExpanded != value)
            {
                //  Установка нового значения.
                _IsExpanded = value;

                //  Вызов события об изменении значения.
                OnPropertyChanged(new(nameof(IsExpanded)));
            }
        }
    }

    /// <summary>
    /// Разворачивает все узлы до текущего.
    /// </summary>
    public void ExpandParents()
    {
        //  Стек родительских узлов.
        Stack<Node> parents = [];

        //  Получение родительского узла.
        Node? parent = Parent;

        //  Проход по дереву узлов вверх.
        while (parent is not null)
        {
            //  Добавление в стек.
            parents.Push(parent);

            //  Переход к следующему узлу.
            parent = parent.Parent;
        }

        //  Раскручивание стека.
        while (parents.TryPop(out parent))
        {
            //  Проверка узла.
            if (parent is not null)
            {
                //  Разворачивание узла.
                parent.IsExpanded = true;
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
}
