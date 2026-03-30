using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Представляет коллекцию узлов.
/// </summary>
public interface INodeCollection :
    INotifyPropertyChanged,
    INotifyCollectionChanged,
    IEnumerable
{

}
