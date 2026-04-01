using Simargl.Designing.Utilities;
using Simargl.Concurrent;
using Simargl.Performing;

namespace Simargl.Web.Proxies;

/// <summary>
/// Представляет коллекцию информации о прокси-серверах.
/// </summary>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
public sealed class WebProxyInfoCollection(CancellationToken cancellationToken) :
    Performer(cancellationToken),
    IEnumerable<WebProxyInfo>
{
    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private volatile AsyncLock? _Lock = new();

    /// <summary>
    /// Поле для хранения хэш-набора элементов.
    /// </summary>
    private volatile HashSet<WebProxyInfo>? _HashSet = [];

    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private volatile List<WebProxyInfo>? _Items = [];

    /// <summary>
    /// Асинхронно добавляет коллекцию элементов.
    /// </summary>
    /// <param name="collection">
    /// Коллекция добавляемых элементов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая коллекцию элементов.
    /// </returns>
    public async Task AddRangeAsync(IEnumerable<WebProxyInfo> collection, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на коллекцию.
        IsNotNull(collection);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника связанного токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            GetCancellationToken(), cancellationToken);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Флаг изменения коллекции.
        bool isChanged = false;

        //  Блокировка критического объекта.
        using (await check(_Lock).LockAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Перебор элементов коллекции.
            foreach (WebProxyInfo item in collection)
            {
                //  Проверка ссылки на элемент.
                if (item is null) continue;

                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Добавление элемента в хэш-набор.
                if (check(_HashSet).Add(item)) isChanged = true;
            }

            //  Проверка флага изменения.
            if (isChanged)
            {
                //  Создание списка элементов.
                List<WebProxyInfo> items = [.. check(_HashSet)];

                //  Замена списка элементов.
                List<WebProxyInfo>? oldItems = Interlocked.Exchange(ref _Items, items);

                //  Проверка старого списка.
                if (oldItems is not null)
                {
                    //  Очистка старого списка.
                    oldItems.Clear();
                }
                else
                {
                    //  Получение списка элементов.
                    if (Interlocked.Exchange(ref _Items, null) is List<WebProxyInfo> actualItems)
                    {
                        //  Очистка списка.
                        actualItems.Clear();
                    }
                }
            }
        }

        //  Выполяет проверку ссылки на объект.
        static T check<T>(T? @ref)
        {
            //  Проверка ссылки.
            if (@ref is null) throw ExceptionsCreator.OperationObjectDisposed(nameof(WebProxyInfoCollection));

            //  Возврат проверенного объекта.
            return @ref;
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли освободить управляемое состояние.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        //  Проверка необходимости освобождения управляемого состояния.
        if (disposing)
        {
            //  Подавление всех некритических исключений.
            DefyCritical(delegate
            {
                //  Получение критического объекта.
                if (Interlocked.Exchange(ref _Lock, null) is AsyncLock @lock)
                {
                    //  Разрушение критического объекта.
                    DefyCritical(@lock.Dispose);
                }
            });

            //  Подавление всех некритических исключений.
            DefyCritical(delegate
            {
                //  Получение хэш-набора элементов.
                if (Interlocked.Exchange(ref _HashSet, null) is HashSet<WebProxyInfo> hashSet)
                {
                    //  Очистка набора.
                    DefyCritical(hashSet.Clear);
                }
            });

            //  Подавление всех некритических исключений.
            DefyCritical(delegate
            {
                //  Получение списка элементов.
                if (Interlocked.Exchange(ref _Items, null) is List<WebProxyInfo> items)
                {
                    //  Очистка списка.
                    DefyCritical(items.Clear);
                }
            });
        }

        //  Вызов метода базового класса.
        base.Dispose(disposing);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<WebProxyInfo> GetEnumerator()
    {
        //  Проверка списка элементов.
        if (_Items is List<WebProxyInfo> items)
        {
            //  Возврат перечислителя списка.
            return items.GetEnumerator();
        }
        else
        {
            //  Объект разрушен.
            throw ExceptionsCreator.OperationObjectDisposed(nameof(WebProxyInfoCollection));
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат универсального перечислителя.
        return GetEnumerator();
    }
}
