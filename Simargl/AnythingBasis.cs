namespace Simargl;

/// <summary>
/// Представляет базу объекта.
/// </summary>
public class AnythingBasis :
    Anything
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="anything">
    /// Объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="anything"/> передана пустая ссылка.
    /// </exception>
    public AnythingBasis(IAnything anything)
    {
        //  Внутреннее обращение к объекту.
        Lay();

        //  Проверка объекта.
        IsNotNull(anything);
    }
}
