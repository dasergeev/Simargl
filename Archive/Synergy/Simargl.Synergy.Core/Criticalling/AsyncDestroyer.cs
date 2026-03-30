namespace Simargl.Synergy.Core.Criticalling;

/// <summary>
/// Представляет метод, асинхронно выполняющий разрушение.
/// </summary>
/// <returns>
/// Задача, выполняющая разрушение.
/// </returns>
internal delegate ValueTask AsyncDestroyer();

/// <summary>
/// Представляет метод, асинхронно выполняющий разрушение.
/// </summary>
/// <typeparam name="T">
/// Тип объекта.
/// </typeparam>
/// <param name="obj">
/// Объект.
/// </param>
/// <returns>
/// Задача, выполняющая разрушение.
/// </returns>
internal delegate ValueTask AsyncDestroyer<T>(T obj);
