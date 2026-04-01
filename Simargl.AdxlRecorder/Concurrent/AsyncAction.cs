namespace Simargl.AdxlRecorder.Concurrent;

/// <summary>
/// Представляет асинхронное действие.
/// </summary>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <returns>
/// Задача, выполняющая асинхронное действие.
/// </returns>
/// <exception cref="OperationCanceledException">
/// Операция отменена.
/// </exception>
public delegate Task AsyncAction(CancellationToken cancellationToken);
