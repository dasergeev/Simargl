using Simargl.Designing;
using System.Runtime.CompilerServices;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет раздел сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public sealed class RawMeraInfoSection :
    RawMeraInfoElement
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="name">
    /// Имя раздела.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RawMeraInfoSection([NoVerify] string name) :
        base(RawMeraInfoElementFormat.Section)
    {
        //  Установка имени разделя.
        Name = name;
    }

    /// <summary>
    /// Возвращает имя раздела.
    /// </summary>
    public string Name { get; }
}
