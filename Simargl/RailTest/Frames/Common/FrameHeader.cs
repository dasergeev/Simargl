using System;

namespace RailTest.Frames;

/// <summary>
/// Представляет базовый класс для всех заголовков кадров.
/// </summary>
public class FrameHeader
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public FrameHeader() : this(StorageFormat.Simple)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Формат кадра.
    /// </param>
    internal FrameHeader(StorageFormat format)
    {
        Format = format;
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
    internal virtual FrameHeader Clone()
    {
        if (Format != StorageFormat.Simple)
        {
            throw new NotSupportedException("При попытке создать копию заголовка канала не была найдена функция, реализующая копирование.");
        }
        return new FrameHeader();
    }

    /// <summary>
    /// Возвращает заголовок кадра в другом формате.
    /// </summary>
    /// <param name="format">
    /// Формат заголовка канала, в который необходимо преобразовать текущий объект.
    /// </param>
    /// <returns>
    /// Преобразованный объект.
    /// </returns>
    internal virtual FrameHeader Convert(StorageFormat format)
    {
        if (format == Format)
        {
            return Clone();
        }
        return format switch
        {
            StorageFormat.Simple => new FrameHeader(),
            StorageFormat.TestLab => new TestLabFrameHeader(),
            StorageFormat.Catman => new CatmanFrameHeader(),
            StorageFormat.Mera => throw new Exception(),
            _ => throw new ArgumentOutOfRangeException(nameof(format), "Произошла попытка преобразовать заголовок кадра в неизвестный формат."),
        };
    }
}
