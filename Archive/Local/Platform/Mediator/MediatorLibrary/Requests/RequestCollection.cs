using System.Collections;

namespace Apeiron.Platform.MediatorLibrary.Requests;

/// <summary>
/// Представляет класс коллекцию запросов.
/// </summary>
public class RequestCollection :
    IEnumerable<Request>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<Request> _Items;

    /// <summary>
    /// Объект блокировки.
    /// </summary>
    private readonly object _Lock = new ();

    /// <summary>
    /// Возвращает размер коллекции.
    /// </summary>
    public int Size
    {
        get
        {
            int allItemSize = 0;

            foreach (var item in _Items)
            {
                allItemSize += item.Size;
            }
            return allItemSize;
        }
    }

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public RequestCollection()
    {
        _Items = new List<Request>();
    }


    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция элементов.
    /// </param>
    public RequestCollection([ParameterNoChecks] IEnumerable<Request> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new(collection);
    }


    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Request> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<Request>)_Items).GetEnumerator();
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

    /// <summary>
    /// Добавляет элемент в коллекцию.
    /// </summary>
    /// <param name="request">Новый элемент коллекции для добавления.</param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="request"/> передана пустая ссылка.
    /// </exception>
    public void Add(Request request)
    {
        Check.IsNotNull(request, nameof(request));

        lock (_Lock)
        {
            _Items.Add(request);
        }
    }

    /// <summary>
    /// Удаляет элемент из коллекции.
    /// </summary>
    /// <param name="request">Элемент, необходимый удалить из коллекции.</param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="request"/> передана пустая ссылка.
    /// </exception>
    public void Remove(Request request)
    {
        Check.IsNotNull(request, nameof(request));

        lock ((_Lock))
        {
            _Items.Remove(request);
        }
    }

    /// <summary>
    /// Очищает коллекцию.
    /// </summary> 
    public void Clear()
    {
        lock (_Lock)
        {
            _Items.Clear();
        }
    }

    ///// <summary>
    ///// Асинхронно загружает данные объекта из потока.
    ///// </summary>
    ///// <param name="stream">
    ///// Поток, из которого необходимо загрузить данные.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен для отслеживания запросов отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, представляющая асинхронную операцию чтения.
    ///// </returns>
    ///// <remarks>
    ///// Задача, должна возвращать объект, к которому обращаются.
    ///// </remarks>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="stream"/> передана пустая ссылка.
    ///// </exception>
    ///// <exception cref="ArgumentException">
    ///// Поток не поддерживает чтение.
    ///// </exception>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //public async Task<RequestCollection> LoadAsync(Stream stream, CancellationToken cancellationToken)
    //{
    //    //  Проверка потока.
    //    Check.IsReadable(stream, nameof(stream));

    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Создание средства чтения из потока.
    //    Spreader spreader = new(stream, Encoding.UTF8);

    //    //  Получение количества элементов коллекции.
    //    int collectionCount = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

    //    for (int i = 0; i < collectionCount; i++)
    //    {
    //        _Items.Add(await reader.ReadObjectAsync<Request>(cancellationToken).ConfigureAwait(false));
    //    }

    //    //  Возврат прочитанного объекта.
    //    return this;
    //}

    ///// <summary>
    ///// Асинхронно сохраняет данные объекта в поток.
    ///// </summary>
    ///// <param name="stream">
    ///// Поток, в который необходимо сохранить данные.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен для отслеживания запросов отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, представляющая асинхронную операцию записи.
    ///// </returns>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="stream"/> передана пустая ссылка.
    ///// </exception>
    ///// <exception cref="ArgumentException">
    ///// Поток не поддерживает запись.
    ///// </exception>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //public async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    //{
    //    //  Проверка потока.
    //    Check.IsWritable(stream, nameof(stream));

    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Создание средства записи в поток.
    //    using AsyncBinaryWriter writer = new(stream, Encoding.UTF8, true);

    //    //  Запись количества элементов.
    //    await writer.WriteAsync(_Items.Count, cancellationToken).ConfigureAwait(false);

    //    //  Запись элементов коллекции.
    //    foreach (var item in _Items)
    //    {
    //        await writer.WriteAsync(item, cancellationToken).ConfigureAwait(false);
    //    }
    //}

    ///// <summary>
    ///// Проверяет содержит ли коллекция переданный элемент.
    ///// </summary>
    ///// <param name="hostServiceInfo">Проверяемый элемент.</param>
    ///// <returns>True если элемет содержится в коллекции, иначе False</returns>
    //public bool Contains(HostServiceInfo hostServiceInfo)
    //{
    //    if (_Items.FirstOrDefault(x => x.Hostname == hostServiceInfo.Hostname) != default)
    //        return true;
    //    return false;
    //}

    ///// <summary>
    ///// Обновляет данные элемента.
    ///// </summary>
    ///// <param name="hostServiceInfo">Элемент списка, который необходимо обновить.</param>
    //public void Update(HostServiceInfo hostServiceInfo)
    //{
    //    lock(_Lock)
    //    {
    //        int index = 0;
    //        if (_Items.Contains(hostServiceInfo))
    //        {
    //            index = _Items.IndexOf(hostServiceInfo);
    //            _Items[index] = hostServiceInfo;
    //        }
    //    }
    //}
}
