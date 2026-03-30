using Simargl.Designing;
using System.Runtime.CompilerServices;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет поле сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public sealed class RawMeraInfoField :
    RawMeraInfoElement
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="name">
    /// Имя поля.
    /// </param>
    /// <param name="value">
    /// Значение поля.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RawMeraInfoField([NoVerify] string name, [NoVerify] string value) :
        base(RawMeraInfoElementFormat.Field)
    {
        //  Установка имени поля.
        Name = name;

        //  Установка значения поля.
        Value = value;
    }

    /// <summary>
    /// Возвращает имя поля.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает значение поля.
    /// </summary>
    public string Value { get; }
}
