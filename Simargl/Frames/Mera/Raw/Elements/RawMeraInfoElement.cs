using Simargl.Designing;
using System.Runtime.CompilerServices;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет элемент сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public abstract class RawMeraInfoElement
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат элемента сырого информационного файла МЕРА.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RawMeraInfoElement([NoVerify] RawMeraInfoElementFormat format)
    {
        //  Установка значения, определяющего формат элемента сырого информационного файла МЕРА.
        Format = format;
    }

    /// <summary>
    /// Возвращает значение, определяющее формат элемента сырого информационного файла МЕРА.
    /// </summary>
    public RawMeraInfoElementFormat Format { get; }
}
