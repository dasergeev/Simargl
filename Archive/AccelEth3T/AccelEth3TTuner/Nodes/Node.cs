using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет узел.
/// </summary>
/// <param name="name">
/// Имя узла.
/// </param>
public class Node(string name) :
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private string _Name = name;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Возвращает или задаёт имя узла.
    /// </summary>
    [Category("Общие")]
    [DisplayName("Имя")]
    [Description("Имя узла.")]
    [ReadOnly(true)]
    public string Name
    {
        get => _Name;
        set
        {
            //  Проверка изменения значения.
            if (_Name != value)
            {
                //  Установака нового значения.
                _Name = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Name)));
            }
        }
    }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    [Browsable(false)]
    public ObservableCollection<Node> Nodes { get; } = [];

    /// <summary>
    /// Вызывается событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        PropertyChanged?.Invoke(this, e);
    }
}
