namespace Simargl;

/// <summary>
/// Предоставляет методы расширения для типа <see cref="IAnything"/>.
/// </summary>
public static class AnythingExtensions
{
    /// <summary>
    /// Возвращает исходную базу объекта.
    /// </summary>
    /// <param name="anything">
    /// Объект.
    /// </param>
    /// <returns>
    /// Исходная база объекта.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="anything"/> передана пустая ссылка.
    /// </exception>
    public static AnythingBasis GetOriginalBasis(this IAnything anything)
    {
        //  Возврат исходной базы.
        return new(anything);
    }
}
