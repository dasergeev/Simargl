using System.ComponentModel;

namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

/// <summary>
/// Представляет сущность базы данных.
/// </summary>
public abstract class Entity :
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения аргументов события изменения идентификатора.
    /// </summary>
    private static readonly PropertyChangedEventArgs _IdChangedEventArgs = new(nameof(Id));

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Id"/>.
    /// </summary>
    public event EventHandler? IdChanged;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения идентификатора сущности.
    /// </summary>
    private long _Id;

    /// <summary>
    /// Поле для хранения объекта, с помощью которого может быть синхронизирован доступ к внутренним данным сущности.
    /// </summary>
    private object? _SyncRoot;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности.
    /// </param>
    internal Entity(long id)
    {
        //  Установка идентификатора.
        _Id = id;
    }

    /// <summary>
    /// Возвращает идентификатор сущности.
    /// </summary>
    public long Id
    {
        get => _Id;
        internal set
        {
            //  Проверка изменения значения свойства.
            if (_Id != value)
            {
                //  Установка нового значения.
                _Id = value;

                //  Вызов общего события об изменении значения свойства.
                OnPropertyChanged(_IdChangedEventArgs);

                //  Вызов события об изменении идентификатора.
                OnIdChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает объект, с помощью которого может быть синхронизирован доступ к внутренним данным сущности.
    /// </summary>
    protected object SyncRoot
    {
        get
        {
            //  Проверка ссылки на объект.
            if (_SyncRoot is null)
            {
                //  Создание ссылки на объект.
                Interlocked.CompareExchange(ref _SyncRoot, new(), null);
            }

            //  Возврат ссылки на объект.
            return _SyncRoot;
        }
    }

    ///// <summary>
    ///// Прикрепляет сущность к контексту.
    ///// </summary>
    ///// <param name="attachment">
    ///// Объект, для прикрепления сущностей к контексту.
    ///// </param>
    //internal abstract void AttachToContext([ParameterNoChecks] Attachment attachment);

    /// <summary>
    /// Вызывает событие <see cref="IdChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnIdChanged(EventArgs e)
    {
        //  Получение делегата для обеспечения безопасности потоков.
        EventHandler? handler = Volatile.Read(ref IdChanged);

        //  Вызов события.
        handler?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Получение делегата для обеспечения безопасности потоков.
        PropertyChangedEventHandler? handler = Volatile.Read(ref PropertyChanged);

        //  Вызов события.
        handler?.Invoke(this, e);
    }
}
