using Simargl.Algebra;

namespace Simargl.Platform.Heap;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="Vector{T}"/>
/// </summary>
public static class VectorExstensions
{
    /// <summary>
    /// Смещает значения всех элементов вектора.
    /// </summary>
    /// <param name="vector">
    /// Вектор.
    /// </param>
    /// <param name="offset">
    /// Смещение.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    public static void Move(this Vector<double> vector, double offset)
    {
        //  Проверка ссылки на вектор.
        IsNotNull(vector, nameof(vector));

        //  Получение массива значений.
        double[] items = vector.Items;

        //  Поэлементное смещение.
        for (int i = 0; i != items.Length; ++i)
        {
            items[i] += offset;
        }
    }

    /// <summary>
    /// Масштабирует значения всех элементов вектора.
    /// </summary>
    /// <param name="vector">
    /// Вектор.
    /// </param>
    /// <param name="factor">
    /// Масштабный множитель.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    public static void Scale(this Vector<double> vector, double factor)
    {
        //  Проверка ссылки на вектор.
        IsNotNull(vector, nameof(vector));

        //  Получение массива значений.
        double[] items = vector.Items;

        //  Поэлементное масштабирование.
        for (int i = 0; i != items.Length; ++i)
        {
            items[i] *= factor;
        }
    }

    /// <summary>
    /// Возвращает индекс первого вхождения минимального значения элементов вектора.
    /// </summary>
    /// <param name="vector">
    /// Вектор.
    /// </param>
    /// <returns>
    /// Индекс первого вхождения минимального значения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="vector"/> передан вектор нулевой длины.
    /// </exception>
    public static int IndexMin(this Vector<double> vector)
    {
        //  Проверка ссылки на вектор.
        IsNotEmpty(vector, nameof(vector));

        //  Получение массива значений.
        double[] items = vector.Items;


        //  Переменная для хранения результата метода.
        int result = 0;

        //  Переменна для хранения текущего значения.
        double value = items[0];

        //  Перебор всех элементов вектора.
        for (int i = 0; i != items.Length; ++i)
        {
            if (value > items[i])
            {
                result = i;
                value = items[i];
            }
        }

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Возвращает индекс первого вхождения максимального значения элементов вектора.
    /// </summary>
    /// <param name="vector">
    /// Вектор.
    /// </param>
    /// <returns>
    /// Индекс первого вхождения максимального значения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="vector"/> передан вектор нулевой длины.
    /// </exception>
    public static int IndexMax(this Vector<double> vector)
    {
        //  Проверка ссылки на вектор.
        IsNotEmpty(vector, nameof(vector));

        //  Получение массива значений.
        double[] items = vector.Items;


        //  Переменная для хранения результата метода.
        int result = 0;

        //  Переменна для хранения текущего значения.
        double value = items[0];

        //  Перебор всех элементов вектора.
        for (int i = 0; i != items.Length; ++i)
        {
            if (value < items[i])
            {
                result = i;
                value = items[i];
            }
        }

        //  Возврат результата.
        return result;
    }
}
