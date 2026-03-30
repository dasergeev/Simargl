namespace Apeiron.Frames.Simple;

/// <summary>
/// Представляет заголовок кадра в формате <see cref="StorageFormat.Simple"/>.
/// </summary>
public sealed class SimpleFrameHeader :
    FrameHeader
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal SimpleFrameHeader() :
        base(StorageFormat.Simple)
    {

    }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override FrameHeader Clone()
    {
        //  Создание нового заголовка кадра.
        return new SimpleFrameHeader();
    }
}
