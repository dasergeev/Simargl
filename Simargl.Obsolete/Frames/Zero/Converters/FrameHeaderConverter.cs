using Simargl.Designing;
using Simargl.Frames.Catman;
using Simargl.Frames.Mera;
using Simargl.Frames.Simple;
using Simargl.Frames.TestLab;

namespace Simargl.Frames.Zero;

/// <summary>
/// Предоставляет методы для преобразования заголовков кадров.
/// </summary>
internal static class FrameHeaderConverter
{
    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static SimpleFrameHeader ToSimple([NoVerify] FrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static TestLabFrameHeader ToTestLab([NoVerify] FrameHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToTestLab((SimpleFrameHeader)header),
        StorageFormat.TestLab => ToTestLab((TestLabFrameHeader)header),
        StorageFormat.Catman => ToTestLab((CatmanFrameHeader)header),
        StorageFormat.Mera => ToTestLab((MeraFrameHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static TestLabFrameHeader ToTestLab([NoVerify] SimpleFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static TestLabFrameHeader ToTestLab([NoVerify] TestLabFrameHeader header) => (TestLabFrameHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static TestLabFrameHeader ToTestLab([NoVerify] CatmanFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static TestLabFrameHeader ToTestLab([NoVerify] MeraFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static CatmanFrameHeader ToCatman([NoVerify] FrameHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToCatman((SimpleFrameHeader)header),
        StorageFormat.TestLab => ToCatman((TestLabFrameHeader)header),
        StorageFormat.Catman => ToCatman((CatmanFrameHeader)header),
        StorageFormat.Mera => ToCatman((MeraFrameHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static CatmanFrameHeader ToCatman([NoVerify] SimpleFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static CatmanFrameHeader ToCatman([NoVerify] TestLabFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static CatmanFrameHeader ToCatman([NoVerify] CatmanFrameHeader header) => (CatmanFrameHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static CatmanFrameHeader ToCatman([NoVerify] MeraFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static MeraFrameHeader ToMera([NoVerify] FrameHeader header) => header.Format switch
    {
        StorageFormat.Simple => ToMera((SimpleFrameHeader)header),
        StorageFormat.TestLab => ToMera((TestLabFrameHeader)header),
        StorageFormat.Catman => ToMera((CatmanFrameHeader)header),
        StorageFormat.Mera => ToMera((MeraFrameHeader)header),
        _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
    };

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static MeraFrameHeader ToMera([NoVerify] SimpleFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static MeraFrameHeader ToMera([NoVerify] TestLabFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static MeraFrameHeader ToMera([NoVerify] CatmanFrameHeader header) => new();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static MeraFrameHeader ToMera([NoVerify] MeraFrameHeader header) => (MeraFrameHeader)header.Clone();

    /// <summary>
    /// Выполняет преобразование заголовка кадра в другой формат.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра, который необходимо преобразовать.
    /// </param>
    /// <param name="format">
    /// Новое значение, определяющее формат хранения элементов кадра регистрации.
    /// </param>
    /// <returns>
    /// Преобразованный заголовок кадра.
    /// </returns>
    public static FrameHeader Convert([NoVerify] FrameHeader header, [NoVerify] StorageFormat format) =>
        format switch
        {
            StorageFormat.Simple => ToSimple(header),
            StorageFormat.TestLab => ToTestLab(header),
            StorageFormat.Catman => ToCatman(header),
            StorageFormat.Mera => ToMera(header),
            _ => throw new InvalidOperationException($"Преобразование не поддерживается."),
        };




    /*




    Из FrameHeader

/// <summary>
/// Возвращает заголовок кадра в другом формате.
/// </summary>
/// <param name="source">
/// Исходный заголовок кадра.
/// </param>
/// <param name="format">
/// Формат заголовка кадра, в который необходимо преобразовать текущий объект.
/// </param>
/// <returns>
/// Преобразованный заголовок кадра.
/// </returns>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="source"/> передана пустая ссылка.
/// </exception>
internal static FrameHeader Convert(FrameHeader source, StorageFormat format)
{
    //  Проверка ссылки на заголовок.
    Validation.IsNotNull(source, nameof(source));

    //  Проверка необходимости преобразования.
    if (source.Format == format)
    {
        //  Возврат копии заголовка.
        return source.Clone();
    }

    //  Проверка целевого формата.
    return format switch
    {
        StorageFormat.Simple => new SimpleFrameHeader(),
        StorageFormat.TestLab => new TestLabFrameHeader(),
        StorageFormat.Catman => new CatmanFrameHeader(),
        _ => throw ExceptionCreator.ArgumentNotContainedInEnumeration<StorageFormat>(nameof(format)),
    };
}



    */
}
