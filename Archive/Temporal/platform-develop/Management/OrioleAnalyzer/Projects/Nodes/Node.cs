using Apeiron.Collections;
using Apeiron.Events;
using System.ComponentModel;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет узел проекта.
/// </summary>
public abstract class Node :
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения значения, определяющего загружен ли узел.
    /// </summary>
    private volatile bool _IsLoaded;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => _PropertyChangedProvider.AddHandler(value);
        remove => _PropertyChangedProvider.RemoveHandler(value);
    }

    /// <summary>
    /// Поле для хранения метода, который выполняет делегат в базовом потоке.
    /// </summary>
    private readonly Action<Action> _Invoker;

    /// <summary>
    /// Поле для хранения поставщика события <see cref="PropertyChanged"/>.
    /// </summary>
    private readonly EventProvider
        <PropertyChangedEventArgs, PropertyChangedEventHandler>
        _PropertyChangedProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public Node(Dispatcher dispatcher, string name)
    {
        //  Установка диспетчера.
        Dispatcher = Check.IsNotNull(dispatcher, nameof(dispatcher));

        //  Установка имени узла.
        _Name = Check.IsNotNull(name, nameof(name));

        //  Создание коллекции дочерних узлов.
        Nodes = new(this, out NotableCollectionProvider<Node> provider);

        //  Установка постащика коллекции дочерних узлов.
        NodesProvider = provider;

        //  Установка метода, который выполняет делегат в базовом потоке.
        _Invoker = Dispatcher.Invoke;

        //  Создание поставщика события.
        _PropertyChangedProvider = new(this);

        //  Установка значения, определяющего загружен ли узел.
        _IsLoaded = this is EmptyNode;

        //  Проверка необходимости добавление пустого узла.
        if (!_IsLoaded)
        {
            //  Добавление пустого узла.
            NodesProvider.Add(new EmptyNode(dispatcher));
        }
    }

    /// <summary>
    /// Возвращает имя узла.
    /// </summary>
    public string Name
    {
        get => _Name;
        protected set
        {
            //  Проверка изменения значения.
            if (_Name != Check.IsNotNull(value, nameof(Name)))
            {
                //  Установка нового значения.
                _Name = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Name)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее был ли загружен узел.
    /// </summary>
    public bool IsLoaded => _IsLoaded;

    /// <summary>
    /// Возвращает диспетчера.
    /// </summary>
    public Dispatcher Dispatcher { get; }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    public NodeCollection Nodes { get; }

    /// <summary>
    /// Возвращает поставщика коллекции <see cref="Nodes"/>.
    /// </summary>
    protected NotableCollectionProvider<Node> NodesProvider { get; }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    public void Load()
    {
        //  Проверка необходимости загрузки.
        if (!_IsLoaded)
        {
            //  Блокировка критического объекта.
            lock (NodesProvider.SyncRoot)
            {
                //  Получение первого элемента.
                if (NodesProvider.Count != 0 && NodesProvider[0] is EmptyNode emptyNode)
                {
                    //  Удаление первого элемента.
                    NodesProvider.Remove(emptyNode);
                }
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
        //  Вызов события.
        _Invoker(() => _PropertyChangedProvider.RaiseEvent(e));
    }
}
