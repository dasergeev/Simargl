namespace Apeiron.Platform.Utilities;

/// <summary>
/// Представляет структуру описывающую исходный файл.
/// </summary>
public readonly struct FileProp : IEquatable<FileProp>
{
    /// <summary>
    /// Путь к файлу.
    /// </summary>
    public string FilePath { get; init; }

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; init; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is FileProp prop && Equals(prop);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(FileProp other)
    {
        return FilePath == other.FilePath;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(FileProp left, FileProp right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(FileProp left, FileProp right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Возвращает хэш-код данного экземпляра.
    /// </summary>
    /// <returns>
    /// 32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(FilePath);
    }
}
