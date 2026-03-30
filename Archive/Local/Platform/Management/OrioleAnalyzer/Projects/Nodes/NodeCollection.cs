namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет коллекцию узлов проекта.
/// </summary>
public sealed class NodeCollection :
    Collections.NotableCollection<Node>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="provider">
    /// Поставщик коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    public NodeCollection(Node parent, out Collections.NotableCollectionProvider<Node> provider) :
        base(new()
        {
            IsConcurrent = true,
            IsCheckNull = true,
            IsCheckDuplicate = true,
            Invoker = Check.IsNotNull(parent, nameof(parent)).Dispatcher.Invoke
        })
    {
        //  Установка поставщика коллекции.
        provider = Provider;
    }

    /// <summary>
    /// Возвращает или задаёт элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное количеству элементов в коллекции.
    /// </exception>
    public Node this[int index] => Provider[index];
}
