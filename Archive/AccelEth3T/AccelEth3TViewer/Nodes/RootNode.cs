using Simargl.AccelEth3T.AccelEth3TViewer.Targets.Directory;

namespace Simargl.AccelEth3T.AccelEth3TViewer.Nodes;

/// <summary>
/// Представляет корневой узел.
/// </summary>
public sealed class RootNode :
    DirectoryNode
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Selected"/>.
    /// </summary>
    public event EventHandler? SelectedChanged;

    /// <summary>
    /// Поле для хранения выбранного узла.
    /// </summary>
    private volatile Node? _Selected;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal RootNode() :
        base(null, string.Empty)
    {

    }

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public Node? Selected
    {
        get => _Selected;
        set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_Selected, value))
            {
                //  Установка значения.
                Node? oldNode = Interlocked.Exchange(ref _Selected, value);

                //  Проверка нового узла.
                if (_Selected is not null)
                {
                    //  Установка значения, определяющего выбран ли узел.
                    _Selected.IsSelected = true;
                }

                //  Проверка старого узла.
                if (oldNode is not null)
                {
                    //  Установка значения, определяющего выбран ли узел.
                    oldNode.IsSelected = false;
                }

                //  Вызов событий об изменении значения.
                OnPropertyChanged(new(nameof(Selected)));
                Volatile.Read(ref SelectedChanged)?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
