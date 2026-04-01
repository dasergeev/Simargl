namespace Simargl.AdxlRecorder.Designing.Utilities;

/// <summary>
/// Предоставляет методы расширений для исключений.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Возвращает значение, определяющее является ли исключение системным.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо проверить.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если исключение <paramref name="exception"/> является системным;
    /// <c>false</c> - в противном случае.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSystem(this Exception exception)
    {
        //  Поиск системного исключения.
        return exception.ExistsCore(SystemMatchCore);
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли исключение критическим.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо проверить.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если исключение <paramref name="exception"/> является критическим;
    /// <c>false</c> - в противном случае.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="exception"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCritical(this Exception exception)
    {
        //  Поиск критического исключения.
        return exception.ExistsCore(CriticalMatchCore);
    }

    /// <summary>
    /// Проверяет, является ли исключение системным.
    /// </summary>
    /// <param name="exception">
    /// Проверяемое исключения.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если исключение <paramref name="exception"/> является системным;
    /// <c>false</c> - в противном случае.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool SystemMatchCore(Exception exception)
    {
        //  Проверка типа исключения.
        return CriticalMatchCore(exception) ||
            exception is DllNotFoundException ||
            exception is ThreadAbortException ||
            exception is OperationCanceledException;
    }

    /// <summary>
    /// Проверяет, является ли исключение критическим.
    /// </summary>
    /// <param name="exception">
    /// Проверяемое исключения.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если исключение <paramref name="exception"/> является критическим;
    /// <c>false</c> - в противном случае.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CriticalMatchCore(Exception exception)
    {
        //  Проверка типа исключения.
        return exception is OutOfMemoryException ||
            exception is StackOverflowException;
    }

    /// <summary>
    /// Проверяет удовлетворяет данное исключение или связанные с ним исключения условиям указанного предиката.
    /// </summary>
    /// <param name="exception">
    /// Проверяемое исключение.
    /// </param>
    /// <param name="match">
    /// Предикат, определяющий условия поиска.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если найдено исключение, удовлетворяющее условиям указанного предиката;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="exception"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="match"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ExistsCore(this Exception exception, Predicate<Exception> match)
    {
        //  Проверка исключения.
        exception = IsNotNull(exception, nameof(exception));

        //  Проверка ссылки на предикат.
        match = IsNotNull(match, nameof(match));

        //  Проверка исключения.
        if (match(exception))
        {
            //  Найдено исключение удовлетворяющее условиям поиска.
            return true;
        }

        //  Проверка типа исключения.
        if (exception is AggregateException aggregateException)
        {
            //  Проверка вложенных исключений.
            if (aggregateException.InnerExceptions.Any((item) => item.ExistsCore(match)))
            {
                //  Найдено исключение удовлетворяющее условиям поиска.
                return true;
            }
        }

        //  Проверка вложенного исключения.
        if (exception.InnerException is Exception innerException)
        {
            //  Поиск во вложенном исключении.
            return innerException.ExistsCore(match);
        }

        //  Исключений удовлетворяющих условиям поиска не найдено.
        return false;
    }
}
