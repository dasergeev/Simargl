using System.Globalization;
using System.IO;

namespace Simargl.IO;

/// <summary>
/// Предоставляет методы для построения путей.
/// </summary>
public static class PathBuilder
{
    /// <summary>
    /// Поле для хранения массива разделителей каталогов, которые встречаются на всех платформах.
    /// </summary>
    private static readonly char[] _DirectorySeparatorChars = ['/', '\\'];

    /// <summary>
    /// Возвращает разделители каталогов, которые встречаются на всех платформах.
    /// </summary>
    public static IEnumerable<char> DirectorySeparatorChars { get; } = _DirectorySeparatorChars;

    /// <summary>
    /// Объединяет две строки в путь.
    /// </summary>
    /// <param name="path1">
    /// Первый путь для объединения.
    /// </param>
    /// <param name="path2">
    /// Второй путь для объединения.
    /// </param>
    /// <returns>
    /// Объединенный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path1"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="path1"/> передана пустая строка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path2"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="path2"/> передана пустая строка.
    /// </exception>
    public static string Combine(string path1, string path2)
    {
        //  Проверка ссылки на первый путь.
        path1 = IsNotEmpty(path1, nameof(path1));

        //  Проверка ссылки на второй путь.
        path2 = IsNotEmpty(path2, nameof(path2));

        //  Нормализация первого пути.
        path1 = Normalize(path1);

        //  Нормализация второго пути.
        path2 = RelativeNormalize(path2);

        //  Возврат объединённого пути.
        return Path.Join(path1, path2);
    }

    /// <summary>
    /// Объединяет массив строк в путь.
    /// </summary>
    /// <param name="paths">
    /// Массив частей пути.
    /// </param>
    /// <returns>
    /// Объединенный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="paths"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="paths"/> передан пустой массив.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В одном из элементов массива <paramref name="paths"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В одном из элементов массива <paramref name="paths"/> передана пустая строка.
    /// </exception>
    public static string Combine(params string[] paths)
    {
        //  Проверка ссылки на массив путей.
        paths = IsNotEmpty(paths);

        //  Определение количества частей пути.
        int count = paths.Length;

        //  Проверка частей строки.
        for (int i = 0; i < count; i++)
        {
            //  Проверка строки массива.
            IsNotEmpty(paths[i], nameof(paths) + i.ToString(CultureInfo.InvariantCulture));
        }

        //  Нормализация первого пути.
        paths[0] = Normalize(paths[0]);

        //  Нормализация последующих частей пути.
        for (int i = 1; i < count; i++)
        {
            //  Нормализация части пути.
            paths[i] = RelativeNormalize(paths[i]);
        }

        //  Возврат объединённого пути.
        return Path.Join(paths);
    }

    /// <summary>
    /// Выполняет общую нормализацию относительного пути.
    /// </summary>
    /// <param name="path">
    /// Путь для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public static string RelativeNormalize(string path)
    {
        //  Нормализация нормализация пути с начальным разделителем.
        path = NormalizeStartSeparator(path);

        //  Общая нормализация.
        path = Normalize(path);

        //  Возврат нормализованного пути.
        return path;
    }

    /// <summary>
    /// Выполняет общую нормализацию пути.
    /// </summary>
    /// <param name="path">
    /// Путь для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public static string Normalize(string path)
    {
        //  Нормализация пути с завершающим разделителем.
        path = NormalizeEndSeparator(path);

        //  Нормализация разделителей каталогов.
        path = NormalizeDirectorySeparators(path);

        //  Возврат нормализованного пути.
        return path;
    }

    /// <summary>
    /// Выполняет нормализацию пути с начальным разделителем.
    /// </summary>
    /// <param name="path">
    /// Путь для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public static string NormalizeStartSeparator(string path)
    {
        //  Проверка ссылки на путь.
        IsNotNull(path, nameof(path));

        //  Нормализация пути.
        path = path.TrimStart(_DirectorySeparatorChars);

        //  Возврат нормализованного пути.
        return path;
    }

    /// <summary>
    /// Выполняет нормализацию пути с завершающим разделителем.
    /// </summary>
    /// <param name="path">
    /// Путь для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public static string NormalizeEndSeparator(string path)
    {
        //  Проверка ссылки на путь.
        IsNotNull(path, nameof(path));

        //  Нормализация пути.
        path = path.TrimEnd(_DirectorySeparatorChars);

        //  Возврат нормализованного пути.
        return path;
    }

    /// <summary>
    /// Выполняет нормализацию разделителей каталогов.
    /// </summary>
    /// <param name="path">
    /// Путь для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованный путь.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public static string NormalizeDirectorySeparators(string path)
    {
        //  Проверка ссылки на путь.
        IsNotNull(path, nameof(path));

        //  Перебор всех разделителей.
        foreach (char separator in DirectorySeparatorChars)
        {
            //  Замена разделителя.
            path = path.Replace(separator, Path.DirectorySeparatorChar);
        }

        //  Возврат нормализованного пути.
        return path;
    }
}
