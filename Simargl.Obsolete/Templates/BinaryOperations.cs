using Simargl.Designing.Expressions;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using System.Runtime.CompilerServices;
using Simargl.Designing;
using Simargl.Designing.Utilities;

namespace Simargl.Templates;

/// <summary>
/// Предоставляет методы для выполнения бинарных операций.
/// </summary>
/// <typeparam name="TLeft">
/// Тип левого операнда.
/// </typeparam>
/// <typeparam name="TRight">
/// Тип правого операнда.
/// </typeparam>
public static class BinaryOperations<TLeft, TRight>
{
    /// <summary>
    /// Поле для хранения метода, выполняющего поточечную бинарную операцию проверки на ревенство.
    /// </summary>
    private static readonly BinaryArrayOperationHandler _ArrayEqual =
        CreateBinaryArrayOperation(Expression.Equal);

    /// <summary>
    /// Выполняет операцию поточечную проверку на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static bool Equal(TLeft[] left, TRight[] right)
    {
        //  Выполнение операции.
        return Invoke(left, right, _ArrayEqual);
    }

    /// <summary>
    /// Выполняет бинарную поточечную операцию.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <param name="handler">
    /// Метод, выполняющий операцию.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Invoke(TLeft[] left, TRight[] right,
        [NoVerify] BinaryArrayOperationHandler handler)
    {
        //  Проверка операндов.
        CheckOperands(left, right);

        //  Возврат результата.
        return handler(left, right);
    }

    /// <summary>
    /// Выполняет проверку операндов.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void CheckOperands(TLeft[] left, TRight[] right)
    {
        //  Проверка ссылки на левый операнд.
        IsNotNull(left, nameof(left));

        //  Проверка ссылки на правый операнд.
        IsNotNull(right, nameof(right));

        //  Проверяет равенство длин двух векторов.
        if (right.Length != left.Length)
        {
            throw new InvalidOperationException("Размеры данных не совпадают.");
        }
    }

    /// <summary>
    /// Представляет метод, выполняющий поточечную бинарную операцию.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    private delegate bool BinaryArrayOperationHandler(
        [NoVerify] TLeft[] left, [NoVerify] TRight[] right);

    /// <summary>
    /// Метод, генерирующий исключение, сообщающее о том, что операция не поддерживается.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    private static bool BinaryArrayInvalidOperation(
        [NoVerify] TLeft[] left, [NoVerify] TRight[] right)
    {
        //  Для анализатора.
        _ = left;
        _ = right;

        //  Генерация исключения.
        throw new InvalidOperationException("Операция не поддерживается.");
    }

    /// <summary>
    /// Создаёт метод, выполняющий поточечную бинарную операцию.
    /// </summary>
    /// <param name="operation">
    /// Выражение поточечной бинарной операции.
    /// </param>
    /// <returns>
    /// Метод, выполняющий поточечную бинарную операцию.
    /// </returns>
    private static BinaryArrayOperationHandler CreateBinaryArrayOperation(
        [NoVerify] BinaryOperationCreator operation)
    {
        //  Параетры метода.
        ParameterExpression left = Parameter(typeof(TLeft[]));
        ParameterExpression right = Parameter(typeof(TRight[]));

        try
        {
            //  Переменные для цикла.
            ParameterExpression index = Parameter(typeof(int));
            ParameterExpression length = Parameter(typeof(int));
            ParameterExpression result = Parameter(typeof(bool));

            //  Метка выхода из цикла.
            LabelTarget label = Label(typeof(bool));

            //  Основной блок метода.
            BlockExpression block = Block(
                new[] { index, length, result },
                Assign(result, Constant(true)),
                Assign(index, Constant(0)),
                Assign(length, Property(left, nameof(Array.Length))),
                Loop(
                    IfThenElse(
                        And(LessThan(index, length), result),
                        Block(
                            Assign(
                                result,
                                operation(
                                    ArrayAccess(left, index),
                                    ArrayAccess(right, index))),
                            PostIncrementAssign(index)),
                        Break(label, result)),
                label));

            //  Компиляция метода.
            return Lambda<BinaryArrayOperationHandler>(
                block, left, right).Compile();
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Операция не поддерживается.
            return BinaryArrayInvalidOperation;
        }
    }
}

/// <summary>
/// Предоставляет методы для выполнения бинарных операций.
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
    /// <summary>
    /// Поле для хранения метода, выполняющего поточечную бинарную операцию сложения.
    /// </summary>
    private static readonly BinaryArrayOperationHandler _ArrayAdd =
        CreateBinaryArrayOperation(Expression.Add);

    /// <summary>
    /// Поле для хранения метода, выполняющего поточечную бинарную операцию вычитания.
    /// </summary>
    private static readonly BinaryArrayOperationHandler _ArraySubtract =
        CreateBinaryArrayOperation(Expression.Subtract);

    /// <summary>
    /// Поле для хранения метода, выполняющего поточечную бинарную операцию умножения.
    /// </summary>
    private static readonly BinaryArrayOperationHandler _ArrayMultiply =
        CreateBinaryArrayOperation(Expression.Multiply);

    /// <summary>
    /// Поле для хранения метода, выполняющего поточечную бинарную операцию деления.
    /// </summary>
    private static readonly BinaryArrayOperationHandler _ArrayDivide =
        CreateBinaryArrayOperation(Expression.Divide);

    /// <summary>
    /// Выполняет операцию поточечного сложения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static TResult[] Add(TLeft[] left, TRight[] right)
    {
        //  Выполнение операции.
        return Invoke(left, right, _ArrayAdd);
    }

    /// <summary>
    /// Выполняет операцию поточечного сложения.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины массивов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static void Add(TLeft[] left, TRight[] right, TResult[] result)
    {
        //  Выполнение операции.
        Invoke(left, right, result, _ArrayAdd);
    }

    /// <summary>
    /// Выполняет операцию поточечного вычитания.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static TResult[] Subtract(TLeft[] left, TRight[] right)
    {
        //  Выполнение операции.
        return Invoke(left, right, _ArraySubtract);
    }

    /// <summary>
    /// Выполняет операцию поточечного вычитания.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины массивов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static void Subtract(TLeft[] left, TRight[] right, TResult[] result)
    {
        //  Выполнение операции.
        Invoke(left, right, result, _ArraySubtract);
    }

    /// <summary>
    /// Выполняет операцию поточечного умножения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static TResult[] Multiply(TLeft[] left, TRight[] right)
    {
        //  Выполнение операции.
        return Invoke(left, right, _ArrayMultiply);
    }

    /// <summary>
    /// Выполняет операцию поточечного умножения.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины массивов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static void Multiply(TLeft[] left, TRight[] right, TResult[] result)
    {
        //  Выполнение операции.
        Invoke(left, right, result, _ArrayMultiply);
    }

    /// <summary>
    /// Выполняет операцию поточечного деления.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static TResult[] Divide(TLeft[] left, TRight[] right)
    {
        //  Выполнение операции.
        return Invoke(left, right, _ArrayDivide);
    }

    /// <summary>
    /// Выполняет операцию поточечного деления.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины массивов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static void Divide(TLeft[] left, TRight[] right, TResult[] result)
    {
        //  Выполнение операции.
        Invoke(left, right, result, _ArrayDivide);
    }

    /// <summary>
    /// Выполняет бинарную поточечную операцию.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <param name="handler">
    /// Метод, выполняющий операцию.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TResult[] Invoke(TLeft[] left, TRight[] right,
        [NoVerify] BinaryArrayOperationHandler handler)
    {
        //  Проверка операндов.
        int length = CheckOperands(left, right);

        //  Создание массива-результата.
        TResult[] result = new TResult[length];

        //  Выполнение операции.
        handler(left, right, result);

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Выполняет бинарную поточечную операцию.
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
    /// <param name="handler">
    /// Метод, выполняющий операцию.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины массивов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Invoke(TLeft[] left, TRight[] right, TResult[] result,
        [NoVerify] BinaryArrayOperationHandler handler)
    {
        //  Проверка операндов.
        CheckOperands(left, right, result);

        //  Выполнение операции.
        handler(left, right, result);
    }

    /// <summary>
    /// Выполняет проверку операндов.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Возвращает длину векторов.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CheckOperands(TLeft[] left, TRight[] right)
    {
        //  Проверка ссылки на левый операнд.
        IsNotNull(left, nameof(left));

        //  Проверка ссылки на правый операнд.
        IsNotNull(right, nameof(right));

        //  Определение длины векторов.
        int length = left.Length;

        //  Проверяет равенство длин двух векторов.
        if (right.Length != length)
        {
            throw new InvalidOperationException("Размеры данных не совпадают.");
        }

        //  Возврат длины операндов.
        return length;
    }

    /// <summary>
    /// Выполняет проверку операндов.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="result"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/>, <paramref name="right"/>
    /// и  <paramref name="result"/> не совпадают.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void CheckOperands(TLeft[] left, TRight[] right, TResult[] result)
    {
        //  Проверка ссылок.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));
        IsNotNull(result, nameof(result));

        //  Определение длины массива-результата.
        int length = result.Length;

        //  Проверка длин массивов-операндов.
        if (left.Length != length || right.Length != length)
        {
            throw new InvalidOperationException("Размеры данных не совпадают.");
        }
    }

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
    private delegate void BinaryArrayOperationHandler(
        [NoVerify] TLeft[] left, [NoVerify] TRight[] right,
        [NoVerify] TResult[] result);

    /// <summary>
    /// Метод, генерирующий исключение, сообщающее о том, что операция не поддерживается.
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
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    private static void BinaryArrayInvalidOperation(
        [NoVerify] TLeft[] left, [NoVerify] TRight[] right,
        [NoVerify] TResult[] result)
    {
        //  Для анализатора.
        _ = left;
        _ = right;
        _ = result;

        //  Генерация исключения.
        throw new InvalidOperationException("Операция не поддерживается.");
    }

    /// <summary>
    /// Создаёт метод, выполняющий поточечную бинарную операцию.
    /// </summary>
    /// <param name="operation">
    /// Выражение поточечной бинарной операции.
    /// </param>
    /// <returns>
    /// Метод, выполняющий поточечную бинарную операцию.
    /// </returns>
    private static BinaryArrayOperationHandler CreateBinaryArrayOperation(
        [NoVerify] Func<Expression, Expression, Expression> operation)
    {
        //  Параетры метода.
        ParameterExpression left = Parameter(typeof(TLeft[]));
        ParameterExpression right = Parameter(typeof(TRight[]));
        ParameterExpression result = Parameter(typeof(TResult[]));

        try
        {
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

            //  Компиляция метода.
            return Lambda<BinaryArrayOperationHandler>(
                block, left, right, result).Compile();
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Операция не поддерживается.
            return BinaryArrayInvalidOperation;
        }
    }
}
