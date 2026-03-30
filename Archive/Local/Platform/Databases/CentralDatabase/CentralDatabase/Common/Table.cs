using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Linq.Expressions;

namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет таблицу базы данных.
/// </summary>
public abstract class Table :
    IQueryable,
    IEnumerable
{
    /// <summary>
    /// Поле для хранения объекта для оценки запросов.
    /// </summary>
    private readonly IQueryable _Queryable;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя таблицы.
    /// </param>
    /// <param name="category">
    /// Имя категории таблицы.
    /// </param>
    /// <param name="queryable">
    /// Объект для оценки запросов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="category"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="queryable"/> передана пустая ссылка.
    /// </exception>
    internal Table(string name, string category, IQueryable queryable)
    {
        //  Установка имени таблицы.
        Name = Check.IsNotNull(name, nameof(name));

        //  Установка имени категории таблицы.
        Category = Check.IsNotNull(category, nameof(category));

        //  Установка объекта для оценки запросов.
        _Queryable = Check.IsNotNull(queryable, nameof(queryable));
    }

    /// <summary>
    /// Возвращает имя таблицы.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает имя категории таблицы.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Асинхронно возвращает количество элементов в таблице.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающкая количество элементов в таблице.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public abstract Task<long> GetCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает тип элементов,
    /// которые возвращаются при выполнении дерева выражения,
    /// связанного с экземпляром класса
    /// </summary>
    Type IQueryable.ElementType => _Queryable.ElementType;

    /// <summary>
    /// Возвращает выражение, связанное с экземпляром класса.
    /// </summary>
    Expression IQueryable.Expression => _Queryable.Expression;

    /// <summary>
    /// Возвращает объект поставщика запросов, связанного с указанным источником данных.
    /// </summary>
    IQueryProvider IQueryable.Provider => _Queryable.Provider;

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя объекта для оценки запросов.
        return _Queryable.GetEnumerator();
    }
}

/// <summary>
/// Представляет таблицу базы данных.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности базы данных.
/// </typeparam>
public sealed class Table<TEntity> :
    Table,
    IQueryable<TEntity>,
    IEnumerable<TEntity>,
    IAsyncEnumerable<TEntity>,
    IQueryable,
    IEnumerable
    where TEntity : Entity
{
    /// <summary>
    /// Поле для хранения ядра множества значений.
    /// </summary>
    private readonly DbSet<TEntity> _CoreSet;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя таблицы.
    /// </param>
    /// <param name="category">
    /// Имя категории таблицы.
    /// </param>
    /// <param name="coreSet">
    /// Ядро множества значений.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="category"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="coreSet"/> передана пустая ссылка.
    /// </exception>
    internal Table(string name, string category, DbSet<TEntity> coreSet) :
        base(name, category, Check.IsNotNull(coreSet, nameof(coreSet)))
    {
        //  Установка ядра множества значений.
        _CoreSet = coreSet;
    }

    /// <summary>
    /// Асинхронно возвращает количество элементов в таблице.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающкая количество элементов в таблице.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task<long> GetCountAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат количества элементов.
        return await _CoreSet.LongCountAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
    {
        //  Возврат перечислителя ядра множества значений.
        return ((IEnumerable<TEntity>)_CoreSet).GetEnumerator();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [CLSCompliant(false)]
    public EntityEntry<TEntity> Attach(TEntity entity)
    {
        return _CoreSet.Attach(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [CLSCompliant(false)]
    public EntityEntry<TEntity> Update(TEntity entity)
    {
        return _CoreSet.Update(entity);
    }

    /// <summary>
    /// Удаляет сущность из таблицы.
    /// </summary>
    /// <param name="entity">
    /// Сущность, которую необходимо удалить из таблицы.
    /// </param>
    public void Remove(TEntity entity)
    {
        _CoreSet.Remove(entity);
    }

    /// <summary>
    /// Асинхронно добавляет сущность в базу данных.
    /// </summary>
    /// <param name="entity">
    /// Сущнос, которую необходимо добавить в базу данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая сущность в базу данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление сущности в базу данных.
        await _CoreSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает асинхронный перечислитель коллекции.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Асинхронный перечислитель коллекции.
    /// </returns>
    IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
    {
        //  Возврат асинхронного перечислителя ядра множества значений.
        return _CoreSet.GetAsyncEnumerator(cancellationToken);
    }
}
