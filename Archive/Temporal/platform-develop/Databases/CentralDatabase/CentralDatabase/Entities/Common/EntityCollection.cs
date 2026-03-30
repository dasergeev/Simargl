using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

/// <summary>
/// Представляет коллекцию сущностей.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности.
/// </typeparam>
public sealed class EntityCollection<TEntity> :
    ICollection<TEntity>,
    INotifyCollectionChanged
    where TEntity : Entity
{
    /// <summary>
    /// ПРоисходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)_Items).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)_Items).CollectionChanged -= value;
    }

    /// <summary>
    /// Поле для хранения коллекции, содержащей элементы.
    /// </summary>
    private readonly ObservableCollection<TEntity> _Items;

    /// <summary>
    /// Поле для хранения загрузчика элементов.
    /// </summary>
    private readonly Func<Session, IQueryable<TEntity>> _Loader;

    /// <summary>
    /// Поле для хранения значения, определяющего загружены ли данные.
    /// </summary>
    private bool _IsLoad;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="loader">
    /// Загрузчик элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="loader"/> передана пустая ссылка.
    /// </exception>
    internal EntityCollection(Func<Session, IQueryable<TEntity>> loader)
    {
        //  Создание коллекции, содержащей элементы.
        _Items = new();

        //  Установка загрузчика элементов.
        _Loader = Check.IsNotNull(loader, nameof(loader));

        //  Установка значения, определяющего загружены ли данные.
        _IsLoad = false;
    }

    /// <summary>
    /// Выполняет загрузку коллекции из базы данных.
    /// </summary>
    private void Load()
    {
        //  Проверка необходимости загрузки данных.
        if (!_IsLoad)
        {
            //  Проверка предварительной загрузки.
            if (_Items.Count == 0)
            {
                //  Выполнение запроса в базу данных.
                TEntity[] items = CentralDatabaseAgent.Request(context => _Loader(context).AsNoTracking().ToArray());

                //  Перебор полученных элементов.
                foreach (var item in items)
                {
                    //  Добавление элемента в коллекцию.
                    _Items.Add(item);
                }
            }

            //  Установка флага загрузки.
            _IsLoad = true;
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count
    {
        get
        {
            //  Загрузка элементов.
            Load();

            //  Возврат количества элементов.
            return _Items.Count;
        }
    }

    ///// <summary>
    ///// Прикрепляет коллекцию сущностей к контексту.
    ///// </summary>
    ///// <param name="attachment">
    ///// Объект, для прикрепления сущностей к контексту.
    ///// </param>
    //internal void AttachToContext([ParameterNoChecks] Attachment attachment)
    //{
    //    //  Перебор сущностей коллекции.
    //    foreach (TEntity entity in _Items)
    //    {
    //        //  Прикрепление сущности к контексту.
    //        entity.AttachToContext(attachment);
    //    }
    //}

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<TEntity> GetEnumerator()
    {
        //  Загрузка элементов.
        Load();

        //  Возврат перечислителя коллекции.
        return _Items.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    bool ICollection<TEntity>.IsReadOnly => false;
    void ICollection<TEntity>.Add(TEntity item) => _Items.Add(item);
    void ICollection<TEntity>.Clear() => _Items.Clear();
    bool ICollection<TEntity>.Contains(TEntity item) => _Items.Contains(item);
    void ICollection<TEntity>.CopyTo(TEntity[] array, int arrayIndex) => _Items.CopyTo(array, arrayIndex);
    bool ICollection<TEntity>.Remove(TEntity item) => _Items.Remove(item);
}
