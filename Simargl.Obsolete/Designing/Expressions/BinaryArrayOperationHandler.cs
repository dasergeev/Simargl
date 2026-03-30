namespace Simargl.Designing.Expressions;

/// <summary>
/// Представляет метод, выполняющий поточечную бинарную операцию.
/// </summary>
/// <param name="left">
/// Левый операнд.
/// </param>
/// <param name="right">
/// Правый операнд.
/// </param>
/// <param name="result">
/// Результат операции.
/// </param>
public delegate void BinaryPointwiseOperationHandler<TLeft, TRight, TResult>(TLeft[] left, TRight[] right, TResult[] result);
