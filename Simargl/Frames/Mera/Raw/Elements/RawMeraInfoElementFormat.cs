namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет значение, определяющее формат элемента сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public enum RawMeraInfoElementFormat
{
    /// <summary>
    /// Раздел сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </summary>
    Section,

    /// <summary>
    /// Поле сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </summary>
    Field,
}
