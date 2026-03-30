namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет метод, выполняющий действие в другом контексте.
/// </summary>
/// <param name="action">
/// Действие, которое необходимо выполнить в другом контексте.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="action"/> передана пустая ссылка.
/// </exception>
public delegate void DirectInvoker(Action action);

/// <summary>
/// Представляет поставщика первичных вызовов.
/// </summary>
public interface IPrimaryInvokeProvider
{
    /// <summary>
    /// Возвращает метод вызывающий действие в основном потоке.
    /// </summary>
    DirectInvoker? PrimaryInvoker { get; }
}
