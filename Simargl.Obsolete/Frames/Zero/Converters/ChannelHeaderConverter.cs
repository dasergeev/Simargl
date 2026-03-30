using Simargl.Designing;
using Simargl.Frames.Catman;
using Simargl.Frames.Mera;
using Simargl.Frames.Simple;
using Simargl.Frames.TestLab;

namespace Simargl.Frames.Zero;

/// <summary>
/// Предоставляет методы для преобразования заголовков каналов.
/// </summary>
internal static class ChannelHeaderConverter
{
    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static SimpleChannelHeader ToSimple([NoVerify] ChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {

        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static TestLabChannelHeader ToTestLab([NoVerify] ChannelHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToTestLab((SimpleChannelHeader)header),
        StorageFormat.TestLab => ToTestLab((TestLabChannelHeader)header),
        StorageFormat.Catman => ToTestLab((CatmanChannelHeader)header),
        StorageFormat.Mera => ToTestLab((MeraChannelHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static TestLabChannelHeader ToTestLab([NoVerify] SimpleChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static TestLabChannelHeader ToTestLab([NoVerify] TestLabChannelHeader header) => (TestLabChannelHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static TestLabChannelHeader ToTestLab([NoVerify] CatmanChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Description = header.Comment,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static TestLabChannelHeader ToTestLab([NoVerify] MeraChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Description = header.Description,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static CatmanChannelHeader ToCatman([NoVerify] ChannelHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToCatman((SimpleChannelHeader)header),
        StorageFormat.TestLab => ToCatman((TestLabChannelHeader)header),
        StorageFormat.Catman => ToCatman((CatmanChannelHeader)header),
        StorageFormat.Mera => ToCatman((MeraChannelHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static CatmanChannelHeader ToCatman([NoVerify] SimpleChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {

        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static CatmanChannelHeader ToCatman([NoVerify] TestLabChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Comment = header.Description,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static CatmanChannelHeader ToCatman([NoVerify] CatmanChannelHeader header) => (CatmanChannelHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static CatmanChannelHeader ToCatman([NoVerify] MeraChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Comment = header.Description,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static MeraChannelHeader ToMera([NoVerify] ChannelHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToMera((SimpleChannelHeader)header),
        StorageFormat.TestLab => ToMera((TestLabChannelHeader)header),
        StorageFormat.Catman => ToMera((CatmanChannelHeader)header),
        StorageFormat.Mera => ToMera((MeraChannelHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static MeraChannelHeader ToMera([NoVerify] SimpleChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {

        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static MeraChannelHeader ToMera([NoVerify] TestLabChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Description = header.Description,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static MeraChannelHeader ToMera([NoVerify] CatmanChannelHeader header) =>
        new(header.Name, header.Unit, header.Cutoff)
        {
            Description = header.Comment,
        };

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static MeraChannelHeader ToMera([NoVerify] MeraChannelHeader header) => (MeraChannelHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка канала в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала, который необходимо преобразовать.
    /// </param>
    /// <param name="format">
    /// Новое значение, определяющее формат хранения элементов кадра регистрации.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок канала.
    /// </returns>
    public static ChannelHeader Convert([NoVerify] ChannelHeader header, [NoVerify] StorageFormat format) =>
        format switch
        {
            StorageFormat.Simple => ToSimple(header),
            StorageFormat.TestLab => ToTestLab(header),
            StorageFormat.Catman => ToCatman(header),
            StorageFormat.Mera => ToMera(header),
            _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
        };
}
