using Apeiron.Platform.Management.Controls;
using System.ComponentModel;

namespace Apeiron.Platform.Management.Models.Nodes;

/// <summary>
/// Представляет узел модели.
/// </summary>
public abstract class ModelNode :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения аргументов события изменения значения свойства <see cref="Name"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _NamePropertyChangedEventArgs = new(nameof(Name));

    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения значения, определяющего загружен ли узел.
    /// </summary>
    private volatile bool _IsLoaded;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public ModelNode(string name)
    {
        //  Установка имени узла.
        _Name = Check.IsNotNull(name, nameof(name));

        //  Создание коллекции дочерних узлов.
        Nodes = new();

        //  Установка значения, определяющего загружен ли узел.
        _IsLoaded = this is EmptyModelNode;

        //  Проверка необходимости добавление пустого узла.
        if (!_IsLoaded)
        {
            //  Добавление пустого узла.
            Nodes.Add(new EmptyModelNode());
        }
    }

    /// <summary>
    /// Возвращает или задаёт имя узла.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Name
    {
        get => _Name;
        set
        {
            //  Проверка нового значения.
            value = Check.IsNotNull(value, nameof(Name));

            //  Проверка изменения значения.
            if (_Name != value)
            {
                //  Установка нового значения.
                _Name = value;

                //  Вызов события изменения значения свойства.
                OnPropertyChanged(_NamePropertyChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    public ModelNodeCollection Nodes { get; }

    /// <summary>
    /// Возвращает значение, определяющее был ли загружен узел.
    /// </summary>
    public bool IsLoaded => _IsLoaded;

    /// <summary>
    /// Возвращает формат рабочего элемента управления, отображающего узел.
    /// </summary>
    internal virtual WorkControlFormat ControlFormat => WorkControlFormat.Default;

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    internal void Load()
    {
        //  Проверка необходимости загрузки.
        if (!_IsLoaded)
        {
            //  Получение первого элемента.
            if (Nodes.Count != 0 && Nodes[0] is EmptyModelNode)
            {
                //  Удаление первого элемента.
                Nodes.RemoveAt(0);
            }

            //  Загрузка узла.
            LoadCore();

            //  Установка значения, определяющего был ли загружен узел.
            _IsLoaded = true;

            //  Вызов события об изменении свойства.
            OnPropertyChanged(new(nameof(IsLoaded)));
        }
    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected abstract void LoadCore();

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Получение делегата для обеспечения безопасности потоков.
        PropertyChangedEventHandler? handler = Volatile.Read(ref PropertyChanged);

        //  Вызов события.
        handler?.Invoke(this, e);
    }
}
