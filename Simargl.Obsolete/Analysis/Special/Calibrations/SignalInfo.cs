namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет информацию об исходном сигнале.
/// </summary>
public sealed class SignalInfo
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя исходного сигнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public SignalInfo(string name)
    {
        //  Установка имени исходного сигнала.
        Name = IsNotNull(name, nameof(name));
    }

    /// <summary>
    /// Возвращает имя сигнала.
    /// </summary>
    public string Name { get; }
}
