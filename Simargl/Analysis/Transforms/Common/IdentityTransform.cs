namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет тождественное преобразование.
/// </summary>
public sealed class IdentityTransform :
    Transform
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public IdentityTransform()
    {

    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <param name="target">
    /// Преобразованный объект.
    /// </param>
    internal protected override void InvokeCore(Signal source, Signal target)
    {
        //  Проверка ссылок.
        if (!ReferenceEquals(source, target))
        {
            //  Копирование полей.
            target.Sampling = source.Sampling;
            target.Items = (double[])source.Items.Clone();
        }
    }
}
