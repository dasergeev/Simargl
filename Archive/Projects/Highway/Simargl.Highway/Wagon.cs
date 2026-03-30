namespace Simargl.Highway;

/// <summary>
/// Представляет вагон.
/// </summary>
public sealed class Wagon
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="wagonType">
    /// Значение, определяющее тип вагона.
    /// </param>
    public Wagon(WagonType wagonType)
    {
        //  Установка значения, определяющего тип вагона.

    }

    /// <summary>
    /// Возвращает значение, определяющее тип вагона.
    /// </summary>
    public WagonType WagonType { get; }
}
