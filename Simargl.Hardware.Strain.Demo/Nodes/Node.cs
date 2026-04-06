using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет узел.
/// </summary>
public abstract class Node :
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения аргументов для события изменения значения свойства <see cref="Name"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _NamePropertyChangedEventArgs = new(nameof(Name));

    /// <summary>
    /// Поле для хранения аргументов для события изменения значения свойства <see cref="Parent"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _ParentPropertyChangedEventArgs = new(nameof(Parent));

    /// <summary>
    /// Поле для хранения карты изображений.
    /// </summary>
    private static readonly ConcurrentDictionary<string, BitmapImage> _ImageMap = [];

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Name"/>.
    /// </summary>
    public event EventHandler? NameChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Parent"/>.
    /// </summary>
    public event EventHandler? ParentChanged;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения родительского узла.
    /// </summary>
    private Node? _Parent;

    /// <summary>
    /// Поле для хранения изображения.
    /// </summary>
    private BitmapImage _Image;

    /// <summary>
    /// Поле для хранения имени изображения.
    /// </summary>
    private string _ImageName;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public Node(Heart heart)
    {
        //  Установка имени узла.
        _Name = string.Empty;

        //  Установка сердца приложения.
        Heart = heart;

        //  Установка родительского узла.
        _Parent = null;

        //  Создание поставщика коллекции дочерних узлов.
        Provider = new(this);

        //  Создание коллекции дочерних узлов.
        Nodes = new(Provider);

        //  Установка имени изображения.
        _ImageName = "Node.png";

        //  Установка изображения.
        _Image = GetImage(_ImageName);
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
            if (_Name != value)
            {
                //  Изменение значения.
                _Name = IsNotNull(value, nameof(Name));

                //  Вызов событий об изменении значения.
                OnNameChanged(EventArgs.Empty);
                OnPropertyChanged(_NamePropertyChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Возвращает родительский узел.
    /// </summary>
    public Node? Parent
    {
        get => _Parent;
        internal set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_Parent, value))
            {
                //  Изменение значения.
                _Parent = value;

                //  Вызов событий об изменении значения.
                OnParentChanged(EventArgs.Empty);
                OnPropertyChanged(_ParentPropertyChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    public NodeCollection Nodes { get; }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    public BitmapImage Image => _Image;

    /// <summary>
    /// Устанавливает изображение.
    /// </summary>
    /// <param name="name">
    /// Имя изображения.
    /// </param>
    public void SetImage(string name)
    {
        //  Проверка изменения имени.
        if (_ImageName != name)
        {
            //  Установка нового имени.
            _ImageName = name;

            //  Установка изображения.
            _Image = GetImage(_ImageName);

            //  Вызов события об изменении значения свойства.
            Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(Image)));
        }
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    /// <param name="name">
    /// Имя изображения.
    /// </param>
    /// <returns>
    /// Изображение.
    /// </returns>
    private static BitmapImage GetImage(string name)
    {
        //  Возврат изображения из карты.
        return _ImageMap.GetOrAdd(name, delegate (string name)
        {
            //  Получение изображения.
            return new(new Uri($"pack://application:,,,/Images/{name}", UriKind.Absolute));
        });
    }

    /// <summary>
    /// Возвращает поставщика коллекции дочерних узлов.
    /// </summary>
    protected NodeCollectionProvider Provider { get; }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    protected Heart Heart { get; }

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    protected App Application => Heart.Application;

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    protected Journal Journal => Application.Journal;

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    protected Invoker Invoker => Application.Invoker;

    /// <summary>
    /// Возвращает механизм поддержки.
    /// </summary>
    protected Keeper Keeper => Application.Keeper;

    /// <summary>
    /// Вызывает событие <see cref="NameChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnNameChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref NameChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="ParentChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnParentChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref ParentChanged)?.Invoke(this, e);
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
