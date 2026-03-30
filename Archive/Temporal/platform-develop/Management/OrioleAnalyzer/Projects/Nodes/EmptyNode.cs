using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет пустой узел.
/// </summary>
public class EmptyNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    public EmptyNode(Dispatcher dispatcher) :
        base(dispatcher, "Загрузка...")
    {

    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }
}
