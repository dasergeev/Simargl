using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции для работы с числовыми массивами.
/// </summary>
public class Arrays
{
    /// <summary>
    /// Возвращает последний элемент входного массива.
    /// </summary>
    /// <typeparam name="T">
    /// Тип входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <returns>
    /// Последний элемент массива.
    /// </returns>
    public static T LastItem<T>(T[] array)
    {
        int n = array.Length;

        return array[n - 1];

        /*
        if (n > 0)
        {
            return array[n - 1];
        }
        else
        {
            return default(T);
        }
        */
    }

    /// <summary>
    /// Возвращает элемент входного массива, отстоящий от последнего на заданное число позиций.
    /// </summary>
    /// <typeparam name="T">
    /// Тип входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <param name="number">
    /// Число позиций, на которое нужный элемент отстоит от последнего.
    /// </param>
    /// <returns>
    /// Элемент массива, отстоящий от последнего на заданное число позиций.
    /// </returns>
    public static T LastItem<T>(T[] array, int number)
    {
        int n = array.Length - number;

        return array[n - 1];

        /*
        if (n > 0)
        {
            return array[n - 1];
        }
        else
        {
            return default(T);
        }
        */
    }

    /// <summary>
    /// Возвращает массив (арифметическую прогрессию) целых чисел с заданными первым членом, последним членом и шагом.  
    /// </summary>
    /// <param name="origin">
    /// Первое число в массиве.
    /// </param>
    /// <param name="step">
    /// Шаг (разность арифметической прогрессии).
    /// </param>
    /// <param name="end">
    /// Последнее число в массиве.
    /// </param>
    /// <returns>
    /// Массив целых чисел.
    /// </returns>
    public static int[] IndexesArray(int origin, int step, int end)
    {
        int L = (end - origin) / step + 1; // Длина массива.

        int[] intarray = new int[L];

        for (int i = 0; i < L; i++)
        {
            intarray[i] = origin + i * step;
        }

        return intarray;
    }

    /// <summary>
    /// Возвращает массив (арифметическую прогрессию с шагом 1) целых чисел с заданными первым членом и длиной массива.
    /// </summary>
    /// <param name="origin">
    /// Первый индекс.
    /// </param>
    /// <param name="length">
    /// Длина массива.
    /// </param>
    /// <returns>
    /// Массив целых чисел.
    /// </returns>
    public static int[] IndexesArray(int origin, int length)
    {
        int[] intarray = new int[length];

        for (int i = 0; i < length; i++)
        {
            intarray[i] = origin + i;
        }

        return intarray;
    }

    /// <summary>
    /// Вычисляет сумму элементов вещественного массива.
    /// </summary>
    /// <param name="array">
    /// Одномерный массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Сумма элементов массива. Если массив пуст, то возвращается нуль.
    /// </returns>
    [Obsolete(@"
        Использовать метод расширения:
            array.Sum()
            из пространства имён System.Linq.
    ")]
    public static double Sum(double[] array)
    {
        int length = array.Length;

        double s = 0.0;

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                s += array[i];
            }
        }
        return s;
    }

    /// <summary>
    /// Вычисляет сумму элементов целочисленного массива.
    /// </summary>
    /// <param name="array">
    /// Одномерный массив целых чисел.
    /// </param>
    /// <returns>
    /// Сумма элементов массива. Если массив пуст, то возвращается нуль.
    /// </returns>
    [Obsolete(@"
        Использовать метод расширения:
            array.Sum()
            из пространства имён System.Linq.
    ")]
    public static long Sum(long[] array)
    {
        int length = array.Length;

        long s = 0;

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                s += array[i];
            }
        }

        return s;
    }

    /// <summary>
    /// Вычисляет сумму элементов комплексного массива.
    /// </summary>
    /// <param name="array">
    /// Одномерный массив комплексных чисел.
    /// </param>
    /// <returns>
    /// Сумма элементов массива. Если массив пуст, то возвращается нуль.
    /// </returns>
    public static Complex Sum(Complex[] array)
    {
        int length = array.Length;

        Complex s = Complex.Zero;// new Complex(0.0, 0.0);

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                s += array[i];
            }
        }

        return s;
    }

    /// <summary>
    /// Масштабирование комплексного массива.
    /// </summary>
    /// <param name="array">
    /// Массив комплексных чисел
    /// </param>
    /// <param name="scale">
    /// Масштабный вещественный множитель.
    /// </param>
    /// <returns>
    /// Отмасштабированный массив.
    /// </returns>
    public static Complex[] Scaling(Complex[] array, double scale)
    {
        int n = array.Length;
        Complex[] scaledArray = new Complex[n];

        for (int i = 0; i < n; i++)
        {
            scaledArray[i] = scale * array[i];
        }

        return scaledArray;
    }

    /// <summary>
    /// Масштабирование вещественного массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="scale">
    /// Масштабный вещественный множитель.
    /// </param>
    /// <returns>
    /// Отмасштабированный массив.
    /// </returns>
    public static double[] Scaling(double[] array, double scale)
    {
        int n = array.Length;
        double[] scaledArray = new double[n];

        for (int i = 0; i < n; i++)
        {
            scaledArray[i] = scale * array[i];
        }

        return scaledArray;
    }

    /// <summary>
    /// Возвращает массив конечных разностей первого порядка: Diff1(x) = (x[1] - x[0], x[2] - x[1],...).
    /// </summary>
    /// <param name="x">
    /// Массив целых чисел.
    /// </param>
    /// <returns>
    /// Массив конечных разностей исходного массива.
    /// </returns>
    public static int[] Diff1(int[] x)
    {
        IsNotNull(x, nameof(x));

        int n = x.Length;
        int[] dx = new int[n - 1];

        for (int i = 0; i < (n - 1); i++)
        {
            dx[i] = x[i + 1] - x[i];
        }

        return dx;
    }

    /// <summary>
    /// Возвращает массив конечных разностей первого порядка: Diff1(x) = (x[1] - x[0], x[2] - x[1],...).
    /// </summary>
    /// <param name="x">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Массив конечных разностей исходного массива.
    /// </returns>
    public static double[] Diff1(double[] x)
    {
        IsNotNull(x, nameof(x));

        int n = x.Length;
        double[] dx = new double[n - 1];

        for (int i = 0; i < (n - 1); i++)
        {
            dx[i] = x[i + 1] - x[i];
        }

        return dx;
    }

    /// <summary>
    /// Находит все элементы входного массива, удовлетворяющие заданному условию. Возвращает их индексы.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <param name="predicate">
    /// Предикат, описывающий условие, которому должны удовлетворять искомые элементы входного массива.
    /// </param>
    /// <returns>
    /// Индексы (в порядке возрастания) всех элементов входного массива, удовлетворяющих заданному условию.
    /// </returns>
    public static int[] FindIndexes<T>(T[] array, Predicate<T> predicate)
    {
        IsNotNull(array, nameof(array));
        IsNotNull(predicate, nameof(predicate));

        int n = array.Length;
        int[] ind = new int[n];

        int k = 0;

        for (int i = 0; i < n; i++)
        {
            if (predicate(array[i]))
            {
                ind[k++] = i;
            }
        }

        Array.Resize(ref ind, k);
        return ind;
    }

    /// <summary>
    /// Находит все элементы коллекции, удовлетворяющие заданному условию. Возвращает их индексы.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входной коллекции.
    /// </typeparam>
    /// <param name="tuple">
    /// Входная коллекция.
    /// </param>
    /// <param name="predicate">
    /// Предикат, описывающий условие, которому должны удовлетворять искомые элементы входной коллекции.
    /// </param>
    /// <returns>
    /// Индексы (в порядке возрастания) всех элементов входной коллекции, удовлетворяющих заданному условию.
    /// </returns>
    public static int[] FindIndexes<T>(IEnumerable<T> tuple, Predicate<T> predicate)
    {
        IsNotNull(tuple, nameof(tuple));
        IsNotNull(predicate, nameof(predicate));

        int n = tuple.Count();
        int[] ind = new int[n];

        int k = 0;

        for (int i = 0; i < n; i++)
        {
            if ( predicate(tuple.ElementAt(i)) )
            {
                ind[k] = i;
                k += 1;  // или, короче, ind[k++] = i;
            }
        }

        Array.Resize(ref ind, k);
        return ind;
    }

    /// <summary>
    /// Находит все элементы коллекции, удовлетворяющие двум заданным условиям. Возвращает их индексы.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входной коллекции.
    /// </typeparam>
    /// <param name="tuple">
    /// Входная коллекция.
    /// </param>
    /// <param name="predicate1">
    /// Предикат 1, описывающий условие 1, которому должны удовлетворять искомые элементы входной коллекции.
    /// </param>
    /// <param name="predicate2">
    /// Предикат 2, описывающий условие 2, которому должны удовлетворять искомые элементы входной коллекции.
    /// </param>
    /// <returns>
    /// Два массива индексов (в порядке возрастания) всех элементов входного массива, удовлетворяющих заданным условиям.
    /// </returns>
    public static int[][] FindIndexes<T>(IEnumerable<T> tuple, Predicate<T> predicate1, Predicate<T> predicate2)
    {
        IsNotNull(tuple, nameof(tuple));
        IsNotNull(predicate1, nameof(predicate1));
        IsNotNull(predicate2, nameof(predicate2));

        int n = tuple.Count();
        int[] ind1 = new int[n];
        int[] ind2 = new int[n];
        int[][] ind = new int[2][];

        int k1 = 0;
        int k2 = 0;

        for (int i = 0; i < n; i++)
        {
            T currentElement = tuple.ElementAt(i);

            if (predicate1(currentElement))
            {
                ind1[k1] = i;
                k1 += 1;
            }

            if (predicate2(currentElement))
            {
                ind2[k2] = i;
                k2 += 1;
            }
        }
                
        Array.Resize(ref ind1, k1);
        Array.Resize(ref ind2, k2);

        ind[0] = ind1;
        ind[1] = ind2;

        return ind;
    }

    /// <summary>
    /// Возвращает индекс первого элемента входного массива, удовлетворяющего заданному условию.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <param name="predicate">
    /// Предикат, описывающий условие, которому должны удовлетворять искомые элементы входного массива.
    /// </param>
    /// <returns>
    /// Наименьший индекс из индексов элементов массива, удовлетворяющих заданному условию. Если таковых элементов нет, то возвращается -1.
    /// </returns>
    public static int FindFirstIndex<T>(T[] array, Predicate<T> predicate)
    {
        IsNotNull(array, nameof(array));
        IsNotNull(predicate, nameof(predicate));

        int length = array.Length;
        int ind = -1; // Возвращаемое значение (default).
        int c = 0; // Счётчик.                       

        while (ind == -1 && c < length)
        {
            if (predicate(array[c]))
            {
                ind = c;
            }
            c++;
        }

        return ind;
    }

    /// <summary>
    /// Возвращает индекс последнего элемента входного массива, удовлетворяющего заданному условию.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <param name="predicate">
    /// Предикат, описывающий условие, которому должны удовлетворять искомые элементы входного массива.
    /// </param>
    /// <returns>
    /// Наибольший индекс из индексов элементов массива, удовлетворяющих заданному условию. Если таковых элементов нет, то возвращается -1.
    /// </returns>
    public static int FindLastIndex<T>(T[] array, Predicate<T> predicate)
    {
        IsNotNull(array, nameof(array));
        IsNotNull(predicate, nameof(predicate));

        int length = array.Length;
        int ind = -1; // Возвращаемое значение (default).
        int c = length - 1; // Счётчик.

        while (ind == -1 && c >= 0)
        {
            if (predicate(array[c]))
            {
                ind = c;
            }
            c--;
        }
        return ind;
    }

    /// <summary>
    /// Конкатенация двух однотипных массивов в заданном порядке.
    /// </summary>
    /// <typeparam name="T">
    /// Параметр типа.
    /// </typeparam>
    /// <param name="array1">
    /// Первый массив.
    /// </param>
    /// <param name="array2">
    /// Второй массив.
    /// </param>
    /// <returns>
    /// Новый массив, составленный из двух исходных.
    /// </returns>
    public static T[] Concatenation<T>(T[] array1, T[] array2)
    {
        int n1 = array1.Length; int n2 = array2.Length;

        if (n1 == 0)
        {
            return array2;
        }

        if (n2 == 0)
        {
            return array1;
        }

        T[] arrayResult = new T[n1 + n2];

        for (int i = 0; i < n1; i++)
        {
            arrayResult[i] = array1[i];
        }

        for (int i = 0; i < n2; i++)
        {
            arrayResult[n1 + i] = array2[i];
        }

        return arrayResult;
    }

    /// <summary>
    /// Конкатенация трёх однотипных массивов в заданном порядке.
    /// </summary>
    /// <typeparam name="T">
    /// Параметр типа.
    /// </typeparam>
    /// <param name="array1">
    /// Первый массив.
    /// </param>
    /// <param name="array2">
    /// Второй массив.
    /// </param>
    /// <param name="array3">
    /// Третий массив.
    /// </param>
    /// <returns>
    /// Новый массив, составленный из трёх исходных.
    /// </returns>
    public static T[] Concatenation<T>(T[] array1, T[] array2, T[] array3)
    {
        int n1 = array1.Length; int n2 = array2.Length; int n3 = array3.Length;

        if (n1 == 0)
        {
            return Concatenation(array2, array3);
        }

        if (n2 == 0)
        {
            return Concatenation(array1, array3);
        }

        if (n3 == 0)
        {
            return Concatenation(array1, array2);
        }

        T[] arrayResult = new T[n1 + n2 + n3];

        for (int i = 0; i < n1; i++)
        {
            arrayResult[i] = array1[i];
        }

        for (int i = 0; i < n2; i++)
        {
            arrayResult[n1 + i] = array2[i];
        }

        int n12 = n1 + n2;
        for (int i = 0; i < n3; i++)
        {
            arrayResult[n12 + i] = array3[i];
        }

        return arrayResult;
    }

    /// <summary>
    /// "Аккуратная" конкатенация двух однотипных массивов - без дублирования элемента "на стыке".
    /// </summary>
    /// <param name="array1">
    /// Первый ("левый") массив.
    /// </param>
    /// <param name="array2">
    /// Второй ("правый") массив.
    /// </param>
    /// <returns>
    /// Массив, составленный из первых двух. При этом, если последний эл-нт левого массива совпадает с первым эл-нтом правого массива, то правый массив берётся начиная со второго элемента.
    /// </returns>
    public static int[] ConcatenationNeat(int[] array1, int[] array2)
    {
        int n1 = array1.Length; int n2 = array2.Length;

        if (n1 == 0)
        {
            return array2;
        }

        if (n2 == 0)
        {
            return array1;
        }

        int i1 = 0;
        if (array1[n1 - 1] == array2[0])
        {
            i1 = 1;
        }

        int[] arrayResult = new int[n1 + n2 - i1];

        for (int i = 0; i < n1; i++)
        {
            arrayResult[i] = array1[i];
        }

        for (int i = i1; i < n2; i++)
        {
            arrayResult[n1 - i1 + i] = array2[i];
        }

        return arrayResult;

    }

    /// <summary>
    /// Выясняет, существуют ли в данном массиве компоненты, удовлетворяющие данному условию.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов входного массива.
    /// </typeparam>
    /// <param name="array">
    /// Входной массив.
    /// </param>
    /// <param name="predicate">
    /// Предикат, описывающий условие, которому должны удовлетворять искомые элементы входного массива.
    /// </param>
    /// <returns>
    /// true, если существуют; false, если не существуют.
    /// </returns>
    public static bool ExistsComponents<T>(T[] array, Predicate<T> predicate)
    {
        int length = array.Length;
        bool answer = false; // Возвращаемое значение.

        if (length == 0)
        {
            return answer;
        }

        int c = 0; // Счётчик.
        while (answer == false && c < length)
        {
            if (predicate(array[c]))
            {
                answer = true;
            }
            c++;
        }
        return answer;
    }

    /// <summary>
    /// Подмассив исходного массива, соответствующий заданному массиву индексов. 
    /// </summary>
    /// <typeparam name="T">
    /// Параметр типа.
    /// </typeparam>
    /// <param name="array">
    /// Исходный массив.
    /// </param>
    /// <param name="indexes">
    /// Массив индексов.
    /// </param>
    /// <returns>
    /// Подмассив массива arrray, соответствующий массиву индексов indexes.
    /// </returns>
    public static T[] SubArray<T>(T[] array, int[] indexes)
    {
        int length = indexes.Length;
        T[] subArray = new T[length];

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                subArray[i] = array[indexes[i]];
            }
        }

        return subArray;
    }

    /// <summary>
    /// Подмассив исходной коллекции, соответствующий заданному массиву индексов. 
    /// </summary>
    /// <typeparam name="T">
    /// Параметр типа.
    /// </typeparam>
    /// <param name="array">
    /// Исходная коллекция.
    /// </param>
    /// <param name="indexes">
    /// Массив индексов.
    /// </param>
    /// <returns>
    /// Подмассив коллекции arrray, соответствующий массиву индексов indexes.
    /// </returns>
    public static IEnumerable<T> SubArray<T>(IEnumerable<T> array, int[] indexes)
    {
        int length = indexes.Length;
        T[] subArray = new T[length];
        //IEnumerable<T> subArray = new T[length];

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                subArray[i] = array.ElementAt(indexes[i]);
                //subArray.ElementAt(i) = array.ElementAt(indexes[i]);
            }
        }

        return subArray;
    }

    /// <summary>
    /// Максимум абсолютного значения.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Наибольшее из абсолютных значений входного массива.
    /// </returns>
    public static double AbsValueMaximum(double[] array)
    {
        int n = array.Length;

        if (n == 0)
        {
            return 0.0;
        }

        double maxValue = Math.Abs(array[0]);

        if (n == 1)
        {
            return maxValue;
        }

        double currentValue;
        for (int i = 1; i < n; i++)
        {
            currentValue = Math.Abs(array[i]);

            if (currentValue > maxValue)
            {
                maxValue = currentValue;
            }
        }

        return maxValue;
    }

    /// <summary>
    /// Находит наименьший индекс максимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив целых чисел.
    /// </param>
    /// <returns>
    /// Наименьший индекс максимального элемента массива.
    /// </returns>
    public static int IndexOfMax(int[] array)
    {
        int n = array.Length;
        int maxElement = array[0];
        int indexMax = 0;

        for (int i = 1; i < n; i++)
        {
            if (array[i] > maxElement)
            {
                maxElement = array[i];
                indexMax = i;
            }
        }

        return indexMax;
    }

    /// <summary>
    /// Находит наименьший индекс максимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Наименьший индекс максимального элемента массива.
    /// </returns>
    public static int IndexOfMax(double[] array)
    {
        int n = array.Length;
        double maxElement = array[0];
        int indexMax = 0;

        for (int i = 1; i < n; i++)
        {
            if (array[i] > maxElement)
            {
                maxElement = array[i];
                indexMax = i;
            }
        }

        return indexMax;
    }

    /// <summary>
    /// Все индексы максимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Все индексы максимального элемента.
    /// </returns>
    public static int[] AllIndexesOfMax(double[] array)
    {
        int i0 = IndexOfMax(array); // Наименьший индекс максимального элемента массива.
        double arrayMax = array[i0]; // Максимальный элемент массива.

        int[] indmax = FindIndexes(array, x => x == arrayMax); // Все индексы максимального элемента.

        return indmax;
    }

    /// <summary>
    /// Несколько максимальных значений.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="n">
    /// Требуемое число максимумов.
    /// </param>
    /// <returns>
    /// n наибольших значений.
    /// </returns>
    public static double[] FewMaximumValues(double[] array, int n)
    {
        if (array.Length == 0)
        {
            return Array.Empty<double>();
        }

        if (array.Length < n)
        {
            n = array.Length;
        }

        double[] maximumValues = new double[n];
        double minValue = array.Min();

        double[] arrayClone = new double[array.Length];
        array.CopyTo(arrayClone, 0);

        for (int i = 0; i < n; i++)
        {
            int[] indmax = AllIndexesOfMax(arrayClone);
            maximumValues[i] = arrayClone[indmax[0]];

            int lengthInd = indmax.Length;
            for (int j = 0; j < lengthInd; j++)
            {
                arrayClone[indmax[j]] = minValue;
            }
        }

        return maximumValues;
    }


    /// <summary>
    /// Находит наименьший индекс минимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив целых чисел.
    /// </param>
    /// <returns>
    /// Наименьший индекс минимального элемента массива.
    /// </returns>
    public static int IndexOfMin(int[] array)
    {
        int n = array.Length;

        if (n == 0)
        {
            return 0;
        }

        int minElement = array[0];
        int indexMin = 0;

        for (int i = 1; i < n; i++)
        {
            if (array[i] < minElement)
            {
                minElement = array[i];
                indexMin = i;
            }
        }

        return indexMin;
    }

    /// <summary>
    /// Находит наименьший индекс минимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Наименьший индекс минимального элемента массива.
    /// </returns>
    public static int IndexOfMin(double[] array)
    {
        int n = array.Length;

        if (n == 0)
        {
            return 0;
        }

        double minElement = array[0];
        int indexMin = 0;

        for (int i = 1; i < n; i++)
        {
            if (array[i] < minElement)
            {
                minElement = array[i];
                indexMin = i;
            }
        }

        return indexMin;
    }

    /// <summary>
    /// Все индексы минимального элемента массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Все индексы минимального элемента.
    /// </returns>
    public static int[] AllIndexesOfMin(double[] array)
    {
        int i0 = IndexOfMin(array); // Наименьший индекс минимального элемента массива.
        double arrayMin = array[i0]; // Минимальный элемент массива.
        int[] indmax = FindIndexes(array, x => x == arrayMin); // Все индексы минимального элемента.

        return indmax;
    }        

    /// <summary>
    /// Находит самый длинный сплошной подмассив (т.е. с шагом 1: (k, k + 1, k + 2,...)) во входном массиве целых чисел.
    /// </summary>
    /// <param name="array">
    /// Массив целочисленных индексов.
    /// </param>
    /// <returns>
    /// Самый длинный сплошной подмассив.
    /// </returns>
    public static int[] LargestContSubArray(int[] array)
    {
        // Алгоритм правильно работает только в том случае, когда входной массив строго упорядочен по возрастанию.

        IsNotNull(array, nameof(array));

        int[] da = Diff1(array);
        int[] n1 = FindIndexes(da, x => x > 1);

        int l2 = n1.Length + 2;
        int[] n2 = new int[l2];
        n2[0] = -1;
        n1.CopyTo(n2, 1);
        n2[l2 - 1] = array.Length - 1;

        int[] L = Diff1(n2);
        int indMaxL = IndexOfMax(L);
        int maxL = L[indMaxL];

        int n = n2[indMaxL + 1];

        int[] indexesArray = new int[maxL];
        Array.ConstrainedCopy(array, n - maxL + 1, indexesArray, 0, maxL);

        return indexesArray;
    }

    /// <summary>
    /// Возвращает все "сплошные" подмассивы (т.е. с шагом 1: (k, k + 1, k + 2,...)) целочисленного массива с количеством элементов не меньшим заданной величины.       
    /// </summary>
    /// <param name="array">
    /// Массив целочисленных индексов.
    /// </param>
    /// <param name="minEl">
    /// Минимальное количество элементов в сплошных подмассивах (не менее 2).
    /// </param>
    /// <returns>
    /// Массив из всех "сплошных" подмассивов с количеством элементов не меньшим, чем требуется. Если таковых нет, то возвращается массив нулевой длины (пустой).
    /// </returns>
    public static int[][] AllContSubArrays(int[] array, int minEl)
    {
        int length = array.Length;

        //Console.WriteLine($"AllContSubArrays - Длина входного массива: {length}");

        int excesLength = (int)Math.Ceiling((double)(length / minEl)); // избыточная длина
        int[] starts = new int[excesLength];
        int[] ends = new int[excesLength];

        byte sIn = 0; // Признак входа в серию.

        int seriesBegin = 0;
        int seriesEnd;
        int seriesLength;

        int c = -1; // Счётчик серий.
        int prev = array[0];

        for (int i = 1; i < length; i++)
        {
            int next = array[i];

            if (prev + 1 == next)
            {
                if (sIn == 0)
                {
                    sIn = 1;
                    seriesBegin = i - 1;
                }
            }
            else
            {
                if (sIn == 1)
                {
                    sIn = 0;
                    seriesEnd = i - 1;

                    seriesLength = seriesEnd - seriesBegin + 1;
                    if (seriesLength >= minEl)
                    {
                        c++;
                        starts[c] = seriesBegin;
                        ends[c] = seriesEnd;
                    }
                }
            }
            prev = next;
        }

        // На случай, если исходный массив заканчивается серией достаточной длины:
        if (sIn == 1)
        {
            seriesEnd = length - 1;

            seriesLength = seriesEnd - seriesBegin + 1;
            if (seriesLength >= minEl)
            {
                c++;
                starts[c] = seriesBegin;
                ends[c] = seriesEnd;
            }
        }

        int realLength = c + 1;
        int[][] subArrays = new int[realLength][];

        for (int k = 0; k < realLength; k++)
        {
            seriesLength = ends[k] - starts[k] + 1;
            subArrays[k] = new int[seriesLength];
            Array.ConstrainedCopy(array, starts[k], subArrays[k], 0, seriesLength);
        }

        return subArrays;
    }

    /// <summary>
    /// Массив одинаковых элементов.
    /// </summary>
    /// <param name="value">
    /// Значение всех элементов массива.
    /// </param>
    /// <param name="length">
    /// Длина массива.
    /// </param>
    /// <returns>
    /// Массив, все элементы которого равны заданному.
    /// </returns>
    public static T[] IdenticalElements<T>(T value, int length)
    {
        T[] result = new T[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = value;
        }

        return result;
    }        

    /// <summary>
    /// Обнуление заданных элементов массива вещественных чисел.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="ind">
    /// Индексы элементов, которые надо заменить нулями.
    /// </param>
    public static void Zeroing(double[] array, int[] ind)
    {
        //int arrayLength = array.Length;
        int indLength = ind.Length;

        if (indLength == 0)
        {
            return;
        }

        for (int i = 0; i < indLength; i++)
        {
            array[ind[i]] = 0.0;
        }

        return;
    }

    /// <summary>
    /// Обнуление глобальных экстремумов массива.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="nIt">
    /// Число итераций. Если nIt равно нулю, возвращется исходный массив.
    /// </param>
    /// <returns>
    /// Массив, полученный из исходного обнулением экстремумов.
    /// </returns>
    public static double[] NeglectExtremums(double[] array, int nIt)
    {
        if (nIt == 0)
        {
            return array;
        }

        int n = array.Length;

        if (n == 0)
        {
            return array;
        }

        int[] indMax; int[] indMin;

        for (int i = 0; i < nIt; i++)
        {
            indMax = AllIndexesOfMax(array);
            Zeroing(array, indMax);

            indMin = AllIndexesOfMin(array);
            Zeroing(array, indMin);
        }

        return array;
    }

    /// <summary>
    /// Удаляет из массива компоненты с заданными индексами.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элементов массива.
    /// </typeparam>
    /// <param name="array">
    /// Исходный массив.
    /// </param>
    /// <param name="indexes">
    /// Массив индексов удаляемых компонент.
    /// </param>
    /// <returns>
    /// Прореженный массив.
    /// </returns>
    public static T[] RemoveSites<T>(T[] array, int[] indexes)
    {
        int indLength = indexes.Length;
        if (indLength == 0)
        {
            return array;
        }

        int arrayLength = array.Length;
        int newLength = arrayLength - indLength;

        T[] arrayThinnedOut = new T[newLength]; // Возвращаемый прореженный массив.

        int c = 0; // Счётчик удалённых компонент.
        int i = 0; // Счётчик компонент исходного массива.
        int j = 0; // Счётчик компонент возвращаемого массива.

        while (c < indLength)
        {
            int k = FindFirstIndex(indexes, x => x == i);
            if (k == -1)
            {
                arrayThinnedOut[j] = array[i];
                j++;
            }
            else
            {
                c++;
            }

            i++;
        }

        if (i < arrayLength)
        {
            for (int k = i; k < arrayLength; k++)
            {
                arrayThinnedOut[j] = array[k];
                j++;
            }
        }

        return arrayThinnedOut;
    }

    /// <summary>
    /// Создаёт массив равноудалённых чисел в заданных границах.
    /// </summary>
    /// <param name="a">
    /// Первая точка массива.
    /// </param>
    /// <param name="b">
    /// Последняя точка массива.
    /// </param>
    /// <param name="n">
    /// Количество точек в массиве. Если n не превосходит 2, то возвращается {a, b}.
    /// </param>
    /// <returns>
    /// Массив равноудалённых чисел.
    /// </returns>
    public static double[] LinSpace(double a, double b, int n)
    {
        if (a == b)
        {
            return new double[1] { a };
        }

        if (a > b)
        {
            (b, a) = (a, b);
        }

        if (n <= 2)
        {
            return new double[2] { a, b };
        }

        double[] dots = new double[n];
        double h = (b - a) / (n - 1);

        for (int i = 0; i < n; i++)
        {
            dots[i] = a + i * h;
        }

        return dots;
    }

    /// <summary>
    /// Создаёт массив равноудалённых чисел в заданных границах.
    /// </summary>
    /// <param name="a">
    /// Первая точка массива.
    /// </param>
    /// <param name="step">
    /// Шаг (расстояние между соседними точками).
    /// </param>
    /// <param name="b">
    /// Последняя точка массива.
    /// </param>
    /// <returns>
    /// Массив равноудалённых чисел.
    /// </returns>
    public static double[] LinSpace(double a, double step, double b)
    {
        int n = (int)Math.Round(Math.Abs(b - a) / step) + 1;

        return LinSpace(a, b, n); 
    }

    /*
    /// <summary>
    /// Создаёт массив равноудалённых вещественных чисел в заданных границах с заданным шагом.
    /// </summary>
    /// <param name="first">
    /// Первый элемент массива.
    /// </param>
    /// <param name="step">
    /// Шаг.
    /// </param>
    /// <param name="last">
    /// Последний элемент массива.
    /// </param>    
    /// <returns>
    /// Массив равноудалённых чисел.
    /// </returns>
    public static double[] EquallySpacedDouble(double first, double step, double last)
    {
        if (first == last)
        {
            return new double[1] { first };
        }

        if (first > last)
        {
            (last, first) = (first, last);
        }

        int n = (int)Math.Floor((last - first) / step);
        double actualLast = first + step * n;
        while (actualLast < last)
        {
            n += 1;
            actualLast = first + step * n;
        }

        double first1 = 0.5 * (first + last - n * step);

        n += 1;        

        if (n <= 2)
        {
            return new double[2] { first, last };
        }

        double[] array = new double[n];

        double prev = first1; double next;
        array[0] = first1;
        for (int i = 1; i < n; i++)
        {
            next = prev + step;
            array[i] = next;
            prev = next;
        }

        return array;
    }
    */
        
    /// <summary>
    /// Одномерный массив одинаковых значений заданной длины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип.
    /// </typeparam>
    /// <param name="constValue">
    /// Значение всех элементов массива.
    /// </param>
    /// <param name="length">
    /// Длина массива.
    /// </param>
    /// <returns>
    /// Одномерный массив одинаковых значений constValue длины length.
    /// </returns>
    public static T[] ConstArray<T>(T constValue, int length)
    {
        T[] array = new T[length];

        Array.Fill(array, constValue);

        //for (int i = 0; i < length; i++)
        //{
        //    array[i] = constValue;
        //}

        return array;
    }

    /// <summary>
    /// Ранговые значения (в статистическом смысле) вариационного ряда. Используется для вычисления коэффициента корреляции Спирмена.
    /// </summary>
    /// <param name="x">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Массив ранговых значений исходного массива.
    /// </returns>
    public static double[] RangValues(double[] x)
    {
        int n = x.Length;
        double[] r = new double[n]; // Возвращаемый массив.

        double lastRange = 0; // Последнее ранговое значение.

        int c = 0; // Счётчик элементов, ранговое значение которых уже вычислено.

        double[] xc = (double[])x.Clone(); // Клон x.

        double em = 0.0;

        while (c < n)
        {
            double currentMax = xc.Max();

            if (c == 0)
            {
                double m = xc.Min();
                em = m - 0.5 * (currentMax - m); // Число, заведомо меньшее минимума исходного массива.
            }

            int[] ind = FindIndexes(x, a => a == currentMax);
            int lInd = ind.Length;
            c += lInd;

            if (lInd == 1)
            {
                r[ind[0]] = lastRange + 1;
                xc[ind[0]] = em;
            }
            else
            {
                for (int k = 0; k < lInd; k++)
                {
                    r[ind[k]] = lastRange + 0.5 * (1 + lInd);
                    xc[ind[k]] = em;
                }
            }

            lastRange += lInd;
        }

        return r;
    }

    /// <summary>
    /// Определитель 3-на-3 матрицы.
    /// </summary>
    /// <param name="A">
    /// 2-мерный массив, задающий матрицу.
    /// </param>
    /// <returns>
    /// Детерминант матрицы A.
    /// </returns>
    public static double Det3(double[,] A)
    {
        double dp = A[0, 0] * A[1, 1] * A[2, 2] + A[2, 0] * A[0, 1] * A[1, 2] + A[1, 0] * A[0, 2] * A[2, 1];
        double dm = A[2, 0] * A[1, 1] * A[0, 2] + A[0, 0] * A[1, 2] * A[2, 1] + A[2, 2] * A[1, 0] * A[0, 1];
        return dp - dm;
    }

    /// <summary>
    /// Обращение 3-на-3 матрицы.
    /// </summary>
    /// <param name="A">
    /// 2-мерный массив, задающий матрицу.
    /// </param>
    /// <returns>
    /// 2-мерный массив, задающий матрицу, обратную к матрице A.
    /// </returns>
    public static double[,] Inv3(double[,] A)
    {
        double d = Det3(A);
            
        double[,] A1 = new double[3, 3];

        A1[0, 0] = (A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]) / d;
        A1[0, 1] = (A[2, 1] * A[0, 2] - A[2, 2] * A[0, 1]) / d;
        A1[0, 2] = (A[0, 1] * A[1, 2] - A[1, 1] * A[0, 2]) / d;

        A1[1, 0] = (A[2, 0] * A[1, 2] - A[1, 0] * A[2, 2]) / d;
        A1[1, 1] = (A[0, 0] * A[2, 2] - A[2, 0] * A[0, 2]) / d;
        A1[1, 2] = (A[1, 0] * A[0, 2] - A[0, 0] * A[1, 2]) / d;

        A1[2, 0] = (A[1, 0] * A[2, 1] - A[2, 0] * A[1, 1]) / d;
        A1[2, 1] = (A[2, 0] * A[0, 1] - A[0, 0] * A[2, 1]) / d;
        A1[2, 2] = (A[0, 0] * A[1, 1] - A[1, 0] * A[0, 1]) / d;

        return A1;
    }

    /// <summary>
    /// Обращение 3-на-3 матрицы в случае, когда известен её определитель.
    /// </summary>
    /// <param name="A">
    /// 2-мерный массив, задающий матрицу.
    /// </param>
    /// <param name="d">
    /// Детерминант матрицы A.
    /// </param>
    /// <returns>
    /// 2-мерный массив, задающий матрицу, обратную к матрице A.
    /// </returns>
    public static double[,] Inv3(double[,] A, double d)
    {
        double[,] A1 = new double[3, 3];

        A1[0, 0] = (A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]) / d;
        A1[0, 1] = (A[2, 1] * A[0, 2] - A[2, 2] * A[0, 1]) / d;
        A1[0, 2] = (A[0, 1] * A[1, 2] - A[1, 1] * A[0, 2]) / d;

        A1[1, 0] = (A[2, 0] * A[1, 2] - A[1, 0] * A[2, 2]) / d;
        A1[1, 1] = (A[0, 0] * A[2, 2] - A[2, 0] * A[0, 2]) / d;
        A1[1, 2] = (A[1, 0] * A[0, 2] - A[0, 0] * A[1, 2]) / d;

        A1[2, 0] = (A[1, 0] * A[2, 1] - A[2, 0] * A[1, 1]) / d;
        A1[2, 1] = (A[2, 0] * A[0, 1] - A[0, 0] * A[2, 1]) / d;
        A1[2, 2] = (A[0, 0] * A[1, 1] - A[1, 0] * A[0, 1]) / d;

        return A1;
    }

    /// <summary>
    /// Произведение квадратной матрицы (левый сомножитель) и вектор-столбца (правый сомножитель).
    /// </summary>
    /// <param name="matrix">
    /// 2-мерный массив, задающий квадратную матрицу.
    /// </param>
    /// <param name="vector">
    /// 1-мерный массив, задающий вектор-столбец.
    /// </param>
    /// <returns>
    /// 1-мерный массив, задающий произведение.
    /// </returns>
    public static double[] MatrixVectorProduct(double[,] matrix, double[] vector)
    {
        int n = vector.Length; // Единственный размер, который нужно знать.

        double[] result = new double[n];

        double s;
        for (int i = 0; i < n; i++)
        {
            s = 0.0;
            for (int j = 0; j < n; j++)
            {
                s += matrix[i, j] * vector[j];
            }
            result[i] = s;
        }

        return result;
    }

    /// <summary>
    /// Проверка коллекции на наличие ненулевых элементов.
    /// </summary>
    /// <param name="collection">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// TRUE, если коллекция состоит только из нулей, FALSE в противном случае.
    /// </returns>
    public static bool OnlyZeros(IEnumerable<double> collection)
    {
        bool result = true;

        foreach (double value in collection)
        {
            if (value != 0.0)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Проверка двух коллекций одинаковой длины на наличие ненулевых элементов.
    /// </summary>
    /// <param name="collection1">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <param name="collection2">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// TRUE, если хотя бы одна коллекция состоит только из нулей, FALSE в противном случае.
    /// </returns>
    public static bool OnlyZeros(IEnumerable<double> collection1, IEnumerable<double> collection2)
    {
        if (OnlyZeros(collection1) || OnlyZeros(collection2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Проверка трёх коллекций одинаковой длины на наличие ненулевых элементов.
    /// </summary>
    /// <param name="collection1">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <param name="collection2">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <param name="collection3">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// TRUE, если хотя бы одна коллекция состоит только из нулей, FALSE в противном случае.
    /// </returns>
    public static bool OnlyZeros(IEnumerable<double> collection1, IEnumerable<double> collection2, IEnumerable<double> collection3)
    {
        if (OnlyZeros(collection1) || OnlyZeros(collection2) || OnlyZeros(collection3))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    /// <summary>
    /// Проверка двух массивов одинаковой длины на наличие ненулевых элементов.
    /// </summary>
    /// <param name="array1">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="array2">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// TRUE, если оба массива состоят только из нулей, FALSE в противном случае.
    /// </returns>
    public static bool OnlyZeros(double[] array1, double[] array2)
    {
        bool result = true;

        int n = array1.Length;
                
        for (int i = 0; i < n; i++)
        {            
            if (array1[i] != 0.0 || array2[i] != 0.0)
            {
                result = false;
                break;
            }
        }

        return result;
    }
    */

    /// <summary>
    /// Возвращает номер полуинтервала вида [a; b), которому принадлежит заданная точка. 
    /// </summary>
    /// <param name="bnds">
    /// Массив границ полуинтервалов (возрастающая последовательность).
    /// </param>
    /// <param name="point">
    /// Заданная точка.
    /// </param>
    /// <returns>
    /// Номер полуинтервала, которому принадлежит заданная точка, и -1, если таких полуинтервалов нет.
    /// </returns>
    public static int IntervalNumber(double[] bnds, double point)
    {
        int nInt = bnds.Length - 1; // Число интервалов.
        int n = -1;

        for (int i = 0; i < nInt; i++)
        {
            if ( (point >= bnds[i]) && (point < bnds[i + 1]) )
            {
                n = i;
                break;
            }
        }

        if (point == bnds[nInt])
        {
            n = nInt - 1;
        }

        return n;
    }

    /// <summary>
    /// Прореживание массива с повторяющимися значениями.
    /// </summary>
    /// <param name="x">
    /// Исходный массив.
    /// </param>
    /// <returns>
    /// Массив индексов ind: x[ind] - прореженный массив.
    /// </returns>
    public static int[] Thinning(double[] x)
    {
        double[] dx = Diff1(x);

        int[] n0 = FindIndexes(dx, v => v != 0);

        if (n0.Length == 0)
        {
            return Array.Empty<int>();
        }

        int s = n0.Length;
        int[] ind = new int[s + 1];

        ind[0] = WhatNot.Halving(n0[0]);

        for (int i = 1; i < s; i++)
        {            
            ind[i] = WhatNot.Halving(n0[i] + n0[i - 1]);
        }
                
        ind[s] = WhatNot.Halving(n0[s - 1] + x.Length);

        return ind;
    }
    
}
