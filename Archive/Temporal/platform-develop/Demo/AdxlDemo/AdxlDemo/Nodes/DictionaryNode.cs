namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Представляет узел, содержащий словарь.
/// </summary>
/// <typeparam name="TKey">
/// Ключ.
/// </typeparam>
/// <typeparam name="TChildNode">
/// Тип дочернего узла.
/// </typeparam>
public abstract class DictionaryNode<TKey, TChildNode> :
    Node<TChildNode>
    where TChildNode : INode
{
    /// <summary>
    /// Поле для хранения селектора ключа.
    /// </summary>
    private readonly Func<TChildNode, TKey> _KeySelector;

    /// <summary>
    /// Поле для хранения объекта, выполняющего сравнение ключей.
    /// </summary>
    private readonly EqualityComparer<TKey> _KeyComparer;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="nodeFormat">
    /// Формат узла.
    /// </param>
    /// <param name="keySelector">
    /// Селектор ключа.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="nodeFormat"/> передано значение,
    /// которое не содержится в перечислении <see cref="NodeFormat"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="keySelector"/> передана пустая ссылка.
    /// </exception>
    public DictionaryNode(Engine engine, string name, NodeFormat nodeFormat, Func<TChildNode, TKey> keySelector) :
        base(engine, name, nodeFormat)
    {
        //  Установка селектора ключа.
        _KeySelector = IsNotNull(keySelector, nameof(keySelector));

        //  Получение объекта, выполняющего сравнение ключей.
        _KeyComparer = EqualityComparer<TKey>.Default;
    }

    /// <summary>
    /// Асинхронно удаляет дочерний узел с указанным ключом.
    /// </summary>
    /// <param name="key">
    /// Ключ узла, который необходимо удалить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удаление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<bool> TryRemoveAsync(TKey key, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение в основном потоке.
        return await PrimaryInvokeAsync(nodes =>
        {
            //  Перебор дочерних узлов.
            foreach (TChildNode node in nodes)
            {
                //  Проверка узла.
                if (_KeyComparer.Equals(key, _KeySelector(node)))
                {
                    //  Удаление узла из коллекции.
                    nodes.Remove(node);

                    //  Узел успешно удалён.
                    return true;
                }
            }

            //  Узел не найден.
            return false;
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно добавляет или обновляет дочерний узел.
    /// </summary>
    /// <param name="key">
    /// Ключ узла.
    /// </param>
    /// <param name="creator">
    /// Метод, создающий узел.
    /// </param>
    /// <param name="updater">
    /// Метод, обновляющий узел.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая добавленный или обновлённый узел.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="creator"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="updater"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<TChildNode> AddOrUpdateAsync(
        TKey key, Func<TChildNode> creator, Action<TChildNode> updater, CancellationToken cancellationToken)
    {
        //  Проверка входных параметров.
        IsNotNull(creator, nameof(creator));
        IsNotNull(updater, nameof(updater));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение в основном потоке.
        return await PrimaryInvokeAsync(nodes =>
        {
            //  Перебор дочерних узлов.
            foreach (TChildNode node in nodes)
            {
                //  Проверка узла.
                if (_KeyComparer.Equals(key, _KeySelector(node)))
                {
                    //  Обновление узла.
                    updater(node);

                    //  Узел обновлён.
                    return node;
                }
            }

            {
                //  Создание узла.
                TChildNode node = creator();

                //  Добавление нового узла.
                nodes.Add(node);

                //  Возврат нового узла.
                return node;
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно возвращает массив дочерних узлов.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая массив дочерних узлов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<TChildNode[]> ToArrayAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение в основном потоке.
        return await PrimaryInvokeAsync(nodes =>
        {
            //  Возврат массива элементов.
            return nodes.ToArray();
        }, cancellationToken).ConfigureAwait(false);
    }
}
