using Simargl.Designing;

namespace Simargl.Frames;

/// <summary>
/// Представляет заголовок кадра регистрации.
/// </summary>
public abstract class FrameHeader :
    ICloneable
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат хранения элементов кадра регистрации.
    /// </param>
    internal FrameHeader([NoVerify] StorageFormat format)
    {
        //  Установка значения, определяющего формат хранения элементов кадра регистрации.
        Format = format;
    }

    /// <summary>
    /// Возвращает значение, определяющее формат хранения элементов кадра регистрации.
    /// </summary>
    public StorageFormat Format { get; }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public abstract FrameHeader Clone();

    /// <summary>
    /// Возвращает копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    object ICloneable.Clone() => Clone();
}
