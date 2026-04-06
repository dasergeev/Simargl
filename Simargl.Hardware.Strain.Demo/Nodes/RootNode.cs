namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет корневой узел.
/// </summary>
public sealed class RootNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public RootNode(Heart heart) :
        base(heart)
    {
        //  Установка имени узла.
        Name = "root";

        //  Создание узла регистратора.
        Recorder = new(heart);
        Provider.Add(Recorder);

        //  Создание узла аппаратуры.
        Equipment = new(heart);
        Provider.Add(Equipment);
    }

    /// <summary>
    /// Возвращает узел регистратора.
    /// </summary>
    public RecorderNode Recorder { get; }

    /// <summary>
    /// Возвращает узел аппаратуры.
    /// </summary>
    public EquipmentNode Equipment { get; }
}
