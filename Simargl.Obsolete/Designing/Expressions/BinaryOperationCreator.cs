using System.Linq.Expressions;

namespace Simargl.Designing.Expressions;

/// <summary>
/// Представляет метод, создающий выражение для бинарной операции.
/// </summary>
/// <param name="left">
/// Выражение для левого операнда.
/// </param>
/// <param name="right">
/// Выражение для правого операнда.
/// </param>
/// <returns>
/// Выражение для бинарной операции.
/// </returns>
public delegate Expression BinaryOperationCreator(Expression left, Expression right);
