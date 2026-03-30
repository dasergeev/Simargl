using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace Simargl.Designing.Expressions;

/// <summary>
/// Предоставляет методы для создания бинарных операций.
/// </summary>
public static class BinaryOperations
{
    /// <summary>
    /// Создаёт выражение для метода, выполняющего поточечную бинарную операцию.
    /// </summary>
    /// <typeparam name="TLeft">
    /// Тип элементов левого операнда.
    /// </typeparam>
    /// <typeparam name="TRight">
    /// Тип элементов правого операнда.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Тип элементов результата операции.
    /// </typeparam>
    /// <param name="operation">
    /// Выражение поточечной бинарной операции.
    /// </param>
    /// <returns>
    /// Выражение для метода, выполняющего поточечную бинарную операцию.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="operation"/> передана пустая ссылка.
    /// </exception>
    public static Expression<BinaryPointwiseOperationHandler<TLeft, TRight, TResult>>
        Pointwise<TLeft, TRight, TResult>(
        Func<Expression, Expression, Expression> operation)
    {
        //  Проверка ссылки на операцию.
        IsNotNull(operation, nameof(operation));

        //  Параетры метода.
        ParameterExpression left = Parameter(typeof(TLeft[]));
        ParameterExpression right = Parameter(typeof(TRight[]));
        ParameterExpression result = Parameter(typeof(TResult[]));

        //  Переменные для цикла.
        ParameterExpression index = Parameter(typeof(int));
        ParameterExpression length = Parameter(typeof(int));

        //  Метка выхода из цикла.
        LabelTarget label = Label();

        //  Основной блок метода.
        BlockExpression block = Block(
            new[] { index, length },
            Assign(index, Constant(0)),
            Assign(length, Property(result, nameof(Array.Length))),
            Loop(
                IfThenElse(
                    LessThan(index, length),
                    Block(
                        Assign(
                            ArrayAccess(result, index),
                            operation(
                                ArrayAccess(left, index),
                                ArrayAccess(right, index))),
                        PostIncrementAssign(index)),
                    Break(label)),
            label));

        //  Создание выражения для метода.
        return Lambda<BinaryPointwiseOperationHandler<TLeft, TRight, TResult>>(
            block, left, right, result);
    }
}

/// <summary>
/// Предоставляет методы для создания бинарных операций.
/// </summary>
/// <typeparam name="TLeft">
/// Тип левого операнда.
/// </typeparam>
/// <typeparam name="TRight">
/// Тип правого операнда.
/// </typeparam>
/// <typeparam name="TResult">
/// Тип результата операции.
/// </typeparam>
public static class BinaryOperations<TLeft, TRight, TResult>
{

}
