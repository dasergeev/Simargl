using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Simargl.Projects.Oriole.Oriole01Viewer;

class Node(string name) :
    INotifyPropertyChanged
{
    private string _Name = name;
    public string Name
    {
        get => _Name;
        set
        {
            if (_Name != value)
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new(nameof(Name)));
            }
        }
    }
    public object? Tag { get; set; }
    public ObservableCollection<Node> Nodes { get; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;
}
