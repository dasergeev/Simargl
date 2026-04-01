using Simargl.AdxlRecorder.Designing.Utilities;

namespace Simargl.AdxlRecorder.Designing;

/// <summary>
/// Предоставляет методы проверки.
/// </summary>
public static class Verify
{
    /// <summary>
    /// Выполенят нормализацию имени параметра.
    /// </summary>
    /// <param name="paramName">
    /// Имя параметра.
    /// </param>
    /// <returns>
    /// Нормализованное имя параметра.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string NameNormalization(string? paramName)
    {
        //  Постоянная, определяющая пустое имя параметра.
        const string emptyName = " ";

        //  Проверка имени параметра.
        if (paramName is not null)
        {
            //  Возврат норматизованного имени.
            return $" \"{paramName}\" ";
        }
        else
        {
            //  Возврат пустого имени.
            return emptyName;
        }
    }


    /// <summary>
    /// Не выполняет проверок.
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Исходный объект.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsAnything<T>(T obj, string? paramName)
    {
        //  Для анализатора.
        _ = paramName;

        //  Возврат объекта.
        return obj;
    }


    /// <summary>
    /// Выполняет проверку:
    /// значение параметра <paramref name="value"/> не должно содержать пустую ссылку.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsNotNull<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value is null)
        {
            //  Выброс исключения.
            throw new ArgumentNullException(
                paramName,
                $"В параметре{NameNormalization(paramName)}передана пустая ссылка.");
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Асинхронно проверяет токен отмены.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, проверяющая токен отмены.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async ValueTask IsNotCancelledAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        if (cancellationToken.IsCancellationRequested)
        {
            //  Выброс исключения.
            throw new OperationCanceledException("Операция отменена.");
        }

        //  Ожидание завершённой задачи.
        await ValueTask.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет проверку на принадлежность значения перечислению.
    /// </summary>
    /// <typeparam name="T">
    /// Тип перечисления.
    /// </typeparam>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое не содержится в перечислении <typeparamref name="T"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsDefined<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : Enum
    {
        //  Проверка значения.
        if (!Enum.IsDefined(typeof(T), value))
        {
            //  В параметре передано значение, которое не содержится в перечислении.
            throw ExceptionsCreator.ArgumentNotContainedInEnumeration<T>(paramName);
        }

        //  Возврат значения.
        return value;
    }



    /// <summary>
    /// Выполняет проверку значения.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="value">
    /// Обнуляемое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsHasValue<T>(T? value, string? paramName)
        where T : struct
    {
        //  Проверка ссылки на объект.
        if (!value.HasValue)
        {
            //  В параметре передана пустая ссылка.
            throw new ArgumentNullException(paramName, $"\"В параметре {paramName} передана пустая ссылка.\"");
        }

        //  Возврат значения.
        return value.Value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IsNotLess(byte value, byte minValue,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort IsNotLess(ushort value, ushort minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint IsNotLess(uint value, uint minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IsNotLess(ulong value, ulong minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsNotLess(sbyte value, sbyte minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsNotLess(short value, short minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsNotLess(int value, int minValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsNotLess(long value, long minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsNotLess(float value, float minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotLess(double value, double minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsNotLess(decimal value, decimal minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime IsNotLess(DateTime value, DateTime minValue, string? paramName)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше или равно заданному.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsNotLess(
        TimeSpan value, TimeSpan minValue,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value < minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IsNotLarger(byte value, byte maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort IsNotLarger(ushort value, ushort maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint IsNotLarger(uint value, uint maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IsNotLarger(ulong value, ulong maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsNotLarger(sbyte value, sbyte maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsNotLarger(short value, short maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsNotLarger(int value, int maxValue, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsNotLarger(long value, long maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsNotLarger(float value, float maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotLarger(double value, double maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsNotLarger(decimal value, decimal maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime IsNotLarger(DateTime value, DateTime maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsNotLarger(
        TimeSpan value, TimeSpan maxValue,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка значения.
        if (value > maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Проверяет, находится ли значение в указанном диапазоне значений:
    ///     должно быть больше или равно значению парамента <paramref name="minValue"/>
    ///     и должно быть меньше или равно значению парамента <paramref name="maxValue"/>
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>
    /// или больше значения <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsInRange(int value,
        [NoVerify] int minValue, [NoVerify] int maxValue,
        [NoVerify] string? paramName)
    {
        //  Проверка значения.
        if (value < minValue || maxValue < value)
        {
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Проверяет, находится ли значение в указанном диапазоне значений:
    ///     должно быть больше или равно значению парамента <paramref name="minValue"/>
    ///     и должно быть меньше или равно значению парамента <paramref name="maxValue"/>
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>
    /// или больше значения <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsInRange(long value,
        [NoVerify] long minValue, [NoVerify] long maxValue,
        [NoVerify] string? paramName)
    {
        //  Проверка значения.
        if (value < minValue || maxValue < value)
        {
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Проверяет, находится ли значение в указанном диапазоне значений:
    ///     должно быть больше или равно значению парамента <paramref name="minValue"/>
    ///     и должно быть меньше или равно значению парамента <paramref name="maxValue"/>
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <paramref name="minValue"/>
    /// или больше значения <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsInRange(TimeSpan value,
        [NoVerify] TimeSpan minValue, [NoVerify] TimeSpan maxValue,
        [NoVerify] string? paramName)
    {
        //  Проверка значения.
        if (value < minValue || maxValue < value)
        {
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на конечное значение.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано бесконечное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsFinite(double value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка числового значения частоты среза фильтра.
        IsNotNaN(value, paramName);

        //  Проверка конечного значения частоты среза фильтра.
        IsNotInfinity(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Проверка на равенство бесконечности.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано бесконечное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotInfinity(double value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (double.IsInfinity(value))
        {
            //  В параметре передано бесконечное значение.
            throw new ArgumentOutOfRangeException(paramName, $"В параметре {paramName} передано бесконечное значение.");
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Проверка на нечисловое значение.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нечисловое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotNaN(double value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (double.IsNaN(value))
        {
            //  В параметре передано нечисловое значение.
            throw ExceptionsCreator.ArgumentNaN(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на принадлежность объекта данному типу.
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта.
    /// </typeparam>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="value"/> передан объект недопустимого типа.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsType<T>(object? value, string? paramName)
    {
        //  Проверка ссылки.
        value = IsNotNull(value, paramName);

        //  Проверка типа.
        if (value.GetType() != typeof(T))
        {
            //  Недопустимый тип.
            throw ExceptionsCreator.ArgumentInvalidType(paramName);
        }

        //  Возврат значения.
        return (T)value;
    }

    ///// <summary>
    ///// Выполняет проверку на возможность приведения объекта к данному типу.
    ///// </summary>
    ///// <typeparam name="T">
    ///// Тип объекта.
    ///// </typeparam>
    ///// <param name="value">
    ///// Значение, которое необходимо проверить.
    ///// </param>
    ///// <param name="paramName">
    ///// Имя проверяемого параметра.
    ///// </param>
    ///// <returns>
    ///// Проверенное значение.
    ///// </returns>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="value"/> передана пустая ссылка.
    ///// </exception>
    ///// <exception cref="ArgumentException">
    ///// В параметре <paramref name="value"/> передан объект недопустимого типа.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static T IsAssignable<T>(object? value, string? paramName)
    //{
    //    //  Проверка ссылки.
    //    value = IsNotNull(value, paramName);

    //    //  Проверка типа.
    //    if (!value.GetType().IsAssignableTo(typeof(T)))
    //    {
    //        //  Недопустимый тип.
    //        throw ExceptionCreator.ArgumentInvalidType(paramName);
    //    }

    //    //  Возврат значения.
    //    return (T)value;
    //}


    /// <summary>
    /// Проверяет, является ли массив пустым.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов массива.
    /// </typeparam>
    /// <param name="array">
    /// Проверяемый массив.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный массив.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="array"/> передан пустой массив.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] IsNotEmpty<T>(T[]? array,
        [CallerArgumentExpression(nameof(array))] string? paramName = null)
    {
        //  Проверка ссылки на массив.
        array = IsNotNull(array, paramName);

        //  Проверка размера массива.
        if (array.Length == 0)
        {
            //  В параметре передан пустой массив.
            throw ExceptionsCreator.ArgumentEmptyArray(paramName);
        }

        //  Возврат проверяемого массива.
        return array;
    }

    /// <summary>
    /// Проверяет, является ли коллекция пустой.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов коллекции.
    /// </typeparam>
    /// <param name="collection">
    /// Проверяемая коллекция.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенная коллекция.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="collection"/> передана пустая коллекция.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T IsNotEmpty<T>(T? collection,
        [CallerArgumentExpression(nameof(collection))] string? paramName = null)
        where T : IEnumerable
    {
        //  Проверка ссылки на коллекцию.
        collection = IsNotNull(collection, paramName);

        //  Перебор элементов коллекции.
        foreach (var _ in collection)
        {
            //  Коллекция содержит элементы.
            return collection;
        }

        //  В параметре передана пустая коллекция.
        throw ExceptionsCreator.ArgumentEmptyCollection(paramName);
    }

    /// <summary>
    /// Проверяет, является ли строка пустой.
    /// </summary>
    /// <param name="value">
    /// Проверяемая строка.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенная строка.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передана пустая строка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string IsNotEmpty(string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка ссылки на строку.
        value = IsNotNull(value, paramName);

        //  Проверка длины строки.
        if (value.Length == 0)
        {
            //  В параметре передана пустая строка.
            throw ExceptionsCreator.ArgumentEmptyString(paramName);
        }

        //  Возврат проверенной строки.
        return value;
    }

    ///// <summary>
    ///// Проверяет, является ли вектор пустой.
    ///// </summary>
    ///// <param name="value">
    ///// Проверяемый вектор.
    ///// </param>
    ///// <param name="paramName">
    ///// Имя проверяемого параметра.
    ///// </param>
    ///// <returns>
    ///// Проверенный вектор.
    ///// </returns>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="value"/> передана пустая ссылка.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="value"/> передан пустой вектор.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector<T> IsNotEmpty<T>(Vector<T>? value, string? paramName)
    //{
    //    //  Проверка ссылки на строку.
    //    value = IsNotNull(value, paramName);

    //    //  Проверка длины строки.
    //    if (value.Length == 0)
    //    {
    //        //  В параметре передан пустой вектор.
    //        throw Exceptions.ArgumentEmptyVector(paramName);
    //    }

    //    //  Возврат проверенной строки.
    //    return value;
    //}

    /// <summary>
    /// Выполняет проверку индекса элемента.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <param name="count">
    /// Количество элементов в коллекции.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <paramref name="count"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsIndex(int index, int count, string? paramName)
    {
        //  Проверка неотрицательности индекса.
        IsNotNegative(index, paramName);

        //  Проверка превышения допустимого значения индекса.
        IsLess(index, count, paramName);

        //  Возврат проверенного значения.
        return index;
    }

    /// <summary>
    /// Проверяет, входит ли диапазон в массив.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов массива.
    /// </typeparam>
    /// <param name="array">
    /// Массив, который необходимо проверить.
    /// </param>
    /// <param name="count">
    /// Количество целевых элементов.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Длина массива <paramref name="array"/> меньше значения <paramref name="count"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsArrayRoomy<T>(T[]? array, int count, string? paramName)
    {
        //  Проверка ссылки на массив.
        array = IsNotNull(array, paramName);

        //  Проверка количества элементов.
        IsNotNegative(count, nameof(count));

        //  Получение длины массива.
        var length = array.LongLength;

        //  Проверка вхождения количества элементов в массив.
        _ = IsNotLarger(count, length, nameof(array));
    }

    /// <summary>
    /// Проверяет, входит ли диапазон в массив.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов массива.
    /// </typeparam>
    /// <param name="array">
    /// Массив, который необходимо проверить.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="array"/>,
    /// начиная с которого расположены целевые элементы.
    /// </param>
    /// <param name="count">
    /// Количество целевых элементов.
    /// </param>
    /// <param name="paramArrayName">
    /// Имя параметра, в котором передаётся массив.
    /// </param>
    /// <param name="paramOffsetName">
    /// Имя параметра, в котором передаётся смещение.
    /// </param>
    /// <param name="paramCountName">
    /// Имя параметра, в котором передаётся количество элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано значение,
    /// которое превышает длину массива <paramref name="array"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="offset"/> и <paramref name="count"/>
    /// превышает длину массива <paramref name="array"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsRange<T>(T[]? array, int offset, int count,
        string? paramArrayName, string? paramOffsetName,
        string? paramCountName)
    {
        //  Проверка ссылки на массив.
        array = IsNotNull(array, paramArrayName);

        //  Проверка смещения в массиве.
        IsNotNegative(offset, paramOffsetName);

        //  Проверка количества элементов.
        IsNotNegative(count, paramCountName);

        //  Получение длины массива.
        var length = array.LongLength;

        //  Проверка вхождения смещения в массив.
        _ = IsNotLarger(offset, length, nameof(offset));

        //  Проверка вхождения количества элементов в массив.
        _ = IsNotLarger(offset + (long)count, length, nameof(count));
    }

    ///// <summary>
    ///// Проверяет диапазон.
    ///// </summary>
    ///// <param name="range">
    ///// Диапазон, который необходимо проверить.
    ///// </param>
    ///// <param name="count">
    ///// Количество элементов в коллекции, для которой проверяется диапазон.
    ///// </param>
    ///// <param name="paramRangeName">
    ///// Имя параметра, в котором передаётся диапазон.
    ///// </param>
    ///// <param name="paramCountName">
    ///// Имя параметра, в котором передаётся количество элементов в коллекции, для которой проверяется диапазон.
    ///// </param>
    ///// <returns>
    ///// Возвращает начальный индекс, с которого начинается диапазон, и длину диапазона.
    ///// </returns>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="count"/> передано отрицательное значение.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="range"/> передан диапазон, в котором начальный индекс отрицательный.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="range"/> передан диапазон, в котором начальный индекс превышает допустимое значение.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="range"/> передан диапазон, в котором отрицательная длина.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="range"/> передан диапазон, который выходит за пределы коллекции.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static (int index, int length) IsRange(Range range, int count,
    //    string? paramRangeName = null, string? paramCountName = null)
    //{
    //    //  Проверка количества элементов в коллекции.
    //    IsNotNegative(count, paramCountName ?? nameof(count));

    //    //  Определение начального индекса диапазона.
    //    int index = range.Start.IsFromEnd ? count - range.Start.Value : range.Start.Value;

    //    //  Проверка на отрицательность начального индекса диапазона.
    //    if (index < 0)
    //    {
    //        //  В параметре передан диапазон, в котором начальный индекс отрицательный.
    //        throw ExceptionCreator.ArgumentRangeNegativeIndex(paramRangeName ?? nameof(range));
    //    }

    //    //  Проверка на превышения допустимого значения начального индекса диапазона.
    //    if (index >= count)
    //    {
    //        //  В параметре передан диапазон, в котором начальный индекс превышает допустимое значение.
    //        throw ExceptionCreator.ArgumentRangeLargeIndex(paramRangeName ?? nameof(range));
    //    }

    //    //  Определение длины диапазона.
    //    int length = (range.End.IsFromEnd ? count - range.End.Value : range.End.Value) - index;

    //    //  Проверка длины диапазона на отрицательность.
    //    if (length < 0)
    //    {
    //        //  В параметре передан диапазон, в котором отрицательная длина.
    //        throw ExceptionCreator.ArgumentRangeNegativeLength(paramRangeName ?? nameof(range));
    //    }

    //    //  Проверка вхождения диапазона в коллекцию.
    //    if (index + length >= count)
    //    {
    //        //  В параметре передан диапазон, который выходит за пределы коллекции.
    //        throw ExceptionCreator.ArgumentRangeNotBelong(paramRangeName ?? nameof(range));
    //    }

    //    //  Возврат начального индекса, с которого начинается диапазон, и длины диапазона.
    //    return (index, length);
    //}

    /// <summary>
    /// Проверяет, входит ли диапазон в массив.
    /// </summary>
    /// <param name="array">
    /// Массив, который необходимо проверить.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="array"/>,
    /// начиная с которого расположены целевые элементы.
    /// </param>
    /// <param name="count">
    /// Количество целевых элементов.
    /// </param>
    /// <param name="paramArrayName">
    /// Имя параметра, в котором передаётся массив.
    /// </param>
    /// <param name="paramOffsetName">
    /// Имя параметра, в котором передаётся смещение.
    /// </param>
    /// <param name="paramCountName">
    /// Имя параметра, в котором передаётся количество элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="RankException">
    /// В параметре <paramref name="array"/> передан многомерный массив.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="array"/> передан массив, индексация которого не начинается с нуля.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано значение,
    /// которое превышает длину массива <paramref name="array"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="offset"/> и <paramref name="count"/>
    /// превышает длину массива <paramref name="array"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsRange(Array array, int offset, int count,
        string? paramArrayName, string? paramOffsetName,
        string? paramCountName)
    {
        //  Проверка массива.
        IsOneDimensional(array, paramArrayName);

        //  Проверка смещения в массиве.
        IsNotNegative(offset, paramOffsetName);

        //  Проверка количества элементов.
        IsNotNegative(count, paramCountName);

        //  Получение длины массива.
        var length = array.LongLength;

        //  Проверка вхождения смещения в массив.
        _ = IsNotLarger(offset, length, nameof(offset));

        //  Проверка вхождения количества элементов в массив.
        _ = IsNotLarger(offset + (long)count, length, nameof(count));
    }

    ///// <summary>
    ///// Проверяет, входит ли диапазон в вектор.
    ///// </summary>
    ///// <typeparam name="T">
    ///// Тип элементов вектора.
    ///// </typeparam>
    ///// <param name="vector">
    ///// Вектор, который необходимо проверить.
    ///// </param>
    ///// <param name="offset">
    ///// Смещение в векторе <paramref name="vector"/>,
    ///// начиная с которого расположены целевые элементы.
    ///// </param>
    ///// <param name="count">
    ///// Количество целевых элементов.
    ///// </param>
    ///// <param name="paramVectorName">
    ///// Имя параметра, в котором передаётся вектор.
    ///// </param>
    ///// <param name="paramOffsetName">
    ///// Имя параметра, в котором передаётся смещение.
    ///// </param>
    ///// <param name="paramCountName">
    ///// Имя параметра, в котором передаётся количество элементов.
    ///// </param>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="vector"/> передана пустая ссылка.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="offset"/> передано отрицательное значение.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="count"/> передано отрицательное значение.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="offset"/> передано значение,
    ///// которое превышает длину вектора <paramref name="vector"/>.
    ///// </exception>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// Сумма значений параметров <paramref name="offset"/> и <paramref name="count"/>
    ///// превышает длину вектора <paramref name="vector"/>.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static void IsRange<T>(Vector<T>? vector, int offset, int count,
    //    string? paramVectorName, string? paramOffsetName,
    //    string? paramCountName)
    //{
    //    //  Проверка ссылки на массив.
    //    vector = IsNotNull(vector, paramVectorName);

    //    //  Проверка смещения в массиве.
    //    IsNotNegative(offset, paramOffsetName);

    //    //  Проверка количества элементов.
    //    IsNotNegative(count, paramCountName);

    //    //  Получение длины массива.
    //    var length = vector.Length;

    //    //  Проверка вхождения смещения в массив.
    //    _ = IsNotLarger(offset, length, nameof(offset));

    //    //  Проверка вхождения количества элементов в массив.
    //    _ = IsNotLarger(offset + (long)count, length, nameof(count));
    //}

    /// <summary>
    /// Выполняет проверку длины строки.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxLength">
    /// Максимальная длина строки.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxLength"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передана строка, длина которой больше максимально допустимой.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string IsLength(string? value, int maxLength, string? paramName)
    {
        //  Проверка ссылки на объект.
        value = IsNotNull(value, paramName);

        //  Проверка неотрицательности максимальной длины строки.
        IsNotNegative(maxLength, nameof(maxLength));

        //  Проверка длины строки.
        if (value.Length > maxLength)
        {
            throw ExceptionsCreator.ArgumentStringLargerMax(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку таймаута.
    /// </summary>
    /// <param name="timeout">
    /// Таймаут.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передан недопустимый таймаут.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsTimeout(
        int timeout,
        [CallerArgumentExpression(nameof(timeout))] string? paramName = null)
    {
        //  Проверка значения.
        if (timeout < 0 && timeout != Timeout.Infinite)
        {
            //  В параметре передан недопустимый таймаут.
            throw new ArgumentOutOfRangeException(paramName, $"В параметре {paramName} передан недопустимый таймаут.");
        }

        //  Возврат проверенного значения.
        return timeout;
    }

    /// <summary>
    /// Выполняет проверку таймаута.
    /// </summary>
    /// <param name="timeout">
    /// Таймаут.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Значение таймаута в миллисекундах.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передан недопустимый таймаут.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsTimeout(
        TimeSpan timeout,
        [CallerArgumentExpression(nameof(timeout))] string? paramName = null)
    {
        //  Проверка бесконечного значения.
        if (timeout == Timeout.InfiniteTimeSpan)
        {
            //  Возврат бесконечного значения.
            return Timeout.Infinite;
        }

        //  Получение значения в миллисекундах.
        long totalMilliseconds = timeout.Ticks / TimeSpan.TicksPerMillisecond;

        //  Проверка значения.
        if (totalMilliseconds < 0 || totalMilliseconds > int.MaxValue)
        {
            //  В параметре передан недопустимый таймаут.
            throw new ArgumentOutOfRangeException($"В параметре {paramName} передан недопустимый таймаут.");
        }

        //  Возврат проверенного значения.
        return unchecked((int)totalMilliseconds);
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsNotNegative(sbyte value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsNotNegative(short value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsNotNegative(int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsNotNegative(long value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsNotNegative(float value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotNegative(double value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsNotNegative(decimal value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < 0)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsNotNegative(TimeSpan value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        if (value < TimeSpan.Zero)
        {
            //  В параметре передано отрицательное значение.
            throw ExceptionsCreator.ArgumentNegativeValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IsNotZero(byte value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort IsNotZero(ushort value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint IsNotZero(uint value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IsNotZero(ulong value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsNotZero(sbyte value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsNotZero(short value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsNotZero(int value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsNotZero(long value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsNotZero(float value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsNotZero(double value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsNotZero(decimal value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == 0)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr IsNotZero(IntPtr value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == IntPtr.Zero)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsNotZero(TimeSpan value, string? paramName)
    {
        //  Проверка на нулевое значение.
        if (value == TimeSpan.Zero)
        {
            //  В параметре передано нулевое значение.
            throw ExceptionsCreator.ArgumentZeroValue(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsPositive(sbyte value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsPositive(short value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsPositive(int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsPositive(long value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsPositive(float value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsPositive(double value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsPositive(decimal value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на отрицательность и равенство нулю.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsPositive(TimeSpan value, string? paramName)
    {
        //  Проверка на отрицательность.
        IsNotNegative(value, paramName);

        //  Проверка на равенство нулю.
        IsNotZero(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IsLess(byte value, byte maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort IsLess(ushort value, ushort maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint IsLess(uint value, uint maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IsLess(ulong value, ulong maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsLess(sbyte value, sbyte maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsLess(short value, short maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsLess(int value, int maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsLess(long value, long maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsLess(float value, float maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsLess(double value, double maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsLess(decimal value, decimal maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime IsLess(DateTime value, DateTime maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку на равенство или превышение максимального значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="maxValue">
    /// Максимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое равно или превышает значение <paramref name="maxValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsLess(TimeSpan value, TimeSpan maxValue, string? paramName)
    {
        //  Проверка значения.
        if (value >= maxValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IsLarger(byte value, byte minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort IsLarger(ushort value, ushort minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint IsLarger(uint value, uint minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IsLarger(ulong value, ulong minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte IsLarger(sbyte value, sbyte minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short IsLarger(short value, short minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IsLarger(int value, int minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IsLarger(long value, long minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IsLarger(float value, float minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double IsLarger(double value, double minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal IsLarger(decimal value, decimal minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime IsLarger(DateTime value, DateTime minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку значения:
    /// значение должно быть больше заданного.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="minValue">
    /// Минимальное значение.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше или равно значению <paramref name="minValue"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan IsLarger(TimeSpan value, TimeSpan minValue, string? paramName)
    {
        //  Проверка значения.
        if (value <= minValue)
        {
            //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку потока на поддержку чтения.
    /// </summary>
    /// <param name="stream">
    /// Поток, который необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный поток.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает чтение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsReadable(Stream? stream, string? paramName)
    {
        //  Проверка ссылки на поток.
        stream = IsNotNull(stream, paramName);

        //  Проверка поддержки чтения.
        if (!stream.CanRead)
        {
            //  Поток не поддерживает чтение.
            throw ExceptionsCreator.StreamNotReadable();
        }

        //  Возврат проверенного потока.
        return stream;
    }

    /// <summary>
    /// Выполняет проверку потока на поддержку поиска.
    /// </summary>
    /// <param name="stream">
    /// Поток, который необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный поток.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает поиск.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsSearchable(Stream? stream, string? paramName)
    {
        //  Проверка ссылки на поток.
        stream = IsNotNull(stream, paramName);

        //  Проверка поддержки поиска.
        if (!stream.CanRead)
        {
            //  Поток не поддерживает поиск.
            throw ExceptionsCreator.StreamNotSearchable();
        }

        //  Возврат проверенного потока.
        return stream;
    }

    /// <summary>
    /// Выполняет проверку потока на поддержку записи.
    /// </summary>
    /// <param name="stream">
    /// Поток, который необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный поток.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает запись.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsWritable(Stream? stream, string? paramName)
    {
        //  Проверка ссылки на поток.
        stream = IsNotNull(stream, paramName);

        //  Проверка поддержки записи.
        if (!stream.CanWrite)
        {
            //  Поток не поддерживает запись.
            throw ExceptionsCreator.StreamNotWritable();
        }

        //  Возврат проверенного потока.
        return stream;
    }

    /// <summary>
    /// Проверяет, является ли массив одномерным.
    /// </summary>
    /// <param name="array">
    /// Массив для проверки.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный массив.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="RankException">
    /// В параметре <paramref name="array"/> передан многомерный массив.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="array"/> передан массив, индексация которого не начинается с нуля.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array IsOneDimensional(Array? array, string? paramName)
    {
        //  Проверка ссылки на массив.
        array = IsNotNull(array, paramName);

        //  Проверка ранга массива.
        if (array.Rank != 1)
        {
            //  Передан многомерный массив.
            throw ExceptionsCreator.ArgumentMultidimensionalArray(paramName);
        }

        //  Проверка индексации.
        if (array.GetLowerBound(0) != 0)
        {
            //  Индексация массива не начинается с нуля.
            throw ExceptionsCreator.ArgumentOutOfRange(paramName);
        }

        //  Возврат проверенного массива.
        return array;
    }

    /// <summary>
    /// Проверяет тип элементов массива.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов массива.
    /// </typeparam>
    /// <param name="array">
    /// Массив для проверки.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенный массив.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="RankException">
    /// В параметре <paramref name="array"/> передан многомерный массив.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="array"/> передан массив, индексация которого не начинается с нуля.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Произошла попытка выполнить недопустимое преобразование.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] IsArray<T>(Array? array, string? paramName)
    {
        //  Проверка размерности массива.
        array = IsOneDimensional(array, paramName);

        //  Проверка типа массива.
        if (array is T[] items)
        {
            //  Возврат проверенного массива.
            return items;
        }
        else
        {
            //  Ошибка преобразования.
            throw ExceptionsCreator.OpertionInvalidCast();
        }
    }












    ///// <summary>
    ///// Выполняет проверку:
    ///// значение <paramref name="value"/> должно содержаться в перечислении <typeparamref name="T"/>.
    ///// </summary>
    ///// <typeparam name="T">
    ///// Тип значения.
    ///// </typeparam>
    ///// <param name="value">
    ///// Проверяемое значение.
    ///// </param>
    ///// <param name="paramName"></param>
    ///// <returns>
    ///// Проверенное значение.
    ///// </returns>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="value"/> передано значение,
    ///// которое не содержится в перечислении <typeparamref name="T"/>.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static T IsDefined<T>(
    //    T value,
    //    [NoVerify, CallerArgumentExpression(nameof(value))] string? paramName = null)
    //    where T : struct, Enum
    //{
    //    //  Выполнение проверки.
    //    if (!Enum.IsDefined(value))
    //    {
    //        //  Проверка имени параметра.
    //        if (paramName is not null)
    //        {
    //            //  Выброс исключения.
    //            throw new ArgumentOutOfRangeException(
    //                paramName,
    //                $"В параметре \"{paramName}\" передано значение, которое не содержится в перечислении \"{typeof(T).Name}\".");
    //        }
    //        else
    //        {
    //            //  Выброс исключения.
    //            throw new ArgumentOutOfRangeException(
    //                paramName,
    //                $"Передано значение, которое не содержится в перечислении \"{typeof(T).Name}\".");
    //        }
    //    }

    //    //  Возврат проверенного значения.
    //    return value;
    //}

    ///// <summary>
    ///// Выполняет проверку:
    ///// значение <paramref name="value"/> должно быть больше нуля.
    ///// </summary>
    ///// <param name="value">
    ///// Проверяемое значение.
    ///// </param>
    ///// <param name="paramName">
    ///// Имя параметра.
    ///// </param>
    ///// <returns>
    ///// Проверенное значение.
    ///// </returns>
    ///// <exception cref="ArgumentOutOfRangeException">
    ///// В параметре <paramref name="value"/> передано отрицательное или равное нулю значение.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int IsPositive(
    //    int value,
    //    [NoVerify, CallerArgumentExpression(nameof(value))] string? paramName = null)
    //{
    //    //  Выполнение проверки.
    //    if (value <= 0)
    //    {
    //        //  Проверка имени параметра.
    //        if (paramName is not null)
    //        {
    //            //  Выброс исключения.
    //            throw new ArgumentOutOfRangeException(
    //                paramName,
    //                $"В параметре \"{paramName}\" передано отрицательное или равное нулю значение.");
    //        }
    //        else
    //        {
    //            //  Выброс исключения.
    //            throw new ArgumentOutOfRangeException(
    //                paramName,
    //                $"Передано отрицательное или равное нулю значение.");
    //        }
    //    }

    //    //  Возврат проверенного значения.
    //    return value;
    //}

    /// <summary>
    /// Выполняет проверку:
    /// значение <paramref name="value"/> должно содержать поток доступный для чтения.
    /// </summary>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передан недоступный для чтения поток.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsCanRead(
        Stream? value,
        [NoVerify, CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка ссылки на поток.
        value = IsNotNull(value, paramName);

        //  Проверка возможности чтения.
        if (!value.CanRead)
        {
            //  Проверка имени параметра.
            if (paramName is not null)
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"В параметре \"{paramName}\" передан недоступный для чтения поток.");
            }
            else
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"Передан недоступный для чтения поток.");
            }
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку:
    /// значение <paramref name="value"/> должно содержать поток доступный для записи.
    /// </summary>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передан недоступный для записи поток.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsCanWrite(
        Stream? value,
        [NoVerify, CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка ссылки на поток.
        value = IsNotNull(value, paramName);

        //  Проверка возможности чтения.
        if (!value.CanWrite)
        {
            //  Проверка имени параметра.
            if (paramName is not null)
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"В параметре \"{paramName}\" передан недоступный для записи поток.");
            }
            else
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"Передан недоступный для записи поток.");
            }
        }

        //  Возврат проверенного значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку:
    /// значение <paramref name="value"/> должно содержать поток доступный для поиска.
    /// </summary>
    /// <param name="value">
    /// Проверяемое значение.
    /// </param>
    /// <param name="paramName">
    /// Имя параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передан недоступный для поиска поток.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream IsCanSeek(
        Stream? value,
        [NoVerify, CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        //  Проверка ссылки на поток.
        value = IsNotNull(value, paramName);

        //  Проверка возможности чтения.
        if (!value.CanSeek)
        {
            //  Проверка имени параметра.
            if (paramName is not null)
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"В параметре \"{paramName}\" передан недоступный для поиска поток.");
            }
            else
            {
                //  Выброс исключения.
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"Передан недоступный для поиска поток.");
            }
        }

        //  Возврат проверенного значения.
        return value;
    }
}
