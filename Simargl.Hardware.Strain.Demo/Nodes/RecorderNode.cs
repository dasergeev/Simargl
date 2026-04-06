using Simargl.Hardware.Strain.Demo.Main;
using System.Collections.ObjectModel;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет узел регистратора.
/// </summary>
public sealed class RecorderNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public RecorderNode(Heart heart) :
        base(heart)
    {
        //  Установка имени узла.
        Name = "Регистратор";

        //  Установка изображения.
        SetImage("Recorder.ico");

        //  Создание коллекции сигналов.
        Signals = [];
    }

    /// <summary>
    /// Возвращает коллекцию сигналов.
    /// </summary>
    public ObservableCollection<Signal> Signals { get; }
}
