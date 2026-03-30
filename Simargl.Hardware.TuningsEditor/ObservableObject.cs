using System.ComponentModel;

namespace Simargl.Hardware.TuningsEditor;

/// <summary>
/// Представляет наблюдаемый объект.
/// </summary>
public abstract class ObservableObject :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }
}
