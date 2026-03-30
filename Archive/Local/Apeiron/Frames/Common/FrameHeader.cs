using Apeiron.Frames.Catman;
using Apeiron.Frames.Simple;
using Apeiron.Frames.TestLab;

namespace Apeiron.Frames;

/// <summary>
/// Представляет базовый класс для всех заголовков кадров.
/// </summary>
public abstract class FrameHeader :
    ICloneable
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Формат кадра.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="StorageFormat"/>.
    /// </exception>
    internal FrameHeader(StorageFormat format)
    {
        //  Проверка и установка формата кадра.
        Format = IsDefined(format, nameof(format));
    }

    /// <summary>
    /// Возвращает формат кадра.
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
        IsNotNull(source, nameof(source));

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
            _ => throw Exceptions.ArgumentNotContainedInEnumeration<StorageFormat>(nameof(format)),
        };
    }
}
