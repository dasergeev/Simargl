using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет сущность.
/// </summary>
public abstract class Entity :
    INotifyPropertyChanged
{
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
    internal Entity()
    {

    }

    /// <summary>
    /// Возвращает идентификатор сущности.
    /// </summary>
    public long Id
    {
        get => _Id;
        internal set
        {
            //  Проверка изменения значения.
            if (_Id != value)
            {
                //  Установка нового значения.
                _Id = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Id)));
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

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <param name="actualValue">
    /// Текущее значение свойства.
    /// </param>
    /// <param name="newValue">
    /// Новое значение свойства.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetProperty(
        [ParameterNoChecks] string propertyName,
        [ParameterNoChecks] ref int actualValue,
        [ParameterNoChecks] int newValue)
    {
        //  Проверка изменения значения свойства.
        if (actualValue != newValue)
        {
            //  Установка нового значения.
            actualValue = newValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(propertyName));
        }
    }

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <param name="actualValue">
    /// Текущее значение свойства.
    /// </param>
    /// <param name="newValue">
    /// Новое значение свойства.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetProperty(
        [ParameterNoChecks] string propertyName,
        [ParameterNoChecks] ref long actualValue,
        [ParameterNoChecks] long newValue)
    {
        //  Проверка изменения значения свойства.
        if (actualValue != newValue)
        {
            //  Установка нового значения.
            actualValue = newValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(propertyName));
        }
    }

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <param name="actualValue">
    /// Текущее значение свойства.
    /// </param>
    /// <param name="newValue">
    /// Новое значение свойства.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetProperty(
        [ParameterNoChecks] string propertyName,
        [ParameterNoChecks] ref double actualValue,
        [ParameterNoChecks] double newValue)
    {
        //  Проверка изменения значения свойства.
        if (actualValue != newValue)
        {
            //  Установка нового значения.
            actualValue = newValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(propertyName));
        }
    }

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <param name="actualValue">
    /// Текущее значение свойства.
    /// </param>
    /// <param name="newValue">
    /// Новое значение свойства.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetProperty(
        [ParameterNoChecks] string propertyName,
        [ParameterNoChecks] ref object actualValue,
        [ParameterNoChecks] object newValue)
    {
        //  Проверка изменения значения свойства.
        if (!ReferenceEquals(actualValue, newValue))
        {
            //  Установка нового значения.
            actualValue = newValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(propertyName));
        }
    }

    ///// <summary>
    ///// Устанавливает значение свойства.
    ///// </summary>
    ///// <typeparam name="TValue">
    ///// Тип значения свойства.
    ///// </typeparam>
    ///// <param name="propertyName">
    ///// Имя свойства.
    ///// </param>
    ///// <param name="actualValue">
    ///// Текущее значение.
    ///// </param>
    ///// <param name="newValue">
    ///// Новое значение.
    ///// </param>
    //protected void SetProperty<TValue>(string propertyName, ref TValue actualValue, TValue newValue)
    //{
    //    //  Получение объекта, выполняющего сравнение значений.
    //    EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;

    //    //  Проверка изменения значения.
    //    if (!comparer.Equals(actualValue, newValue))
    //    {
    //        //  Установка нового значения.
    //        actualValue = newValue;

    //        //  Вызов события об изменении значения свойства.
    //        OnPropertyChanged(propertyName);
    //    }
    //}



    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Тип сущности.
    /// </typeparam>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction<TEntity>(EntityTypeBuilder<TEntity> typeBuilder)
        where TEntity : Entity
    {
        ////  Указание того, как контекст обнаруживает
        ////  изменения свойств для экземпляра типа сущности.
        //typeBuilder.HasChangeTrackingStrategy(
        //    //  Свойства помечаются как измененные,
        //    //  когда сущность вызывает событие PropertyChanged.
        //    ChangeTrackingStrategy.ChangedNotifications);

        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);
    }
}
