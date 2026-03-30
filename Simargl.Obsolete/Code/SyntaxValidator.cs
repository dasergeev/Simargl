using System.Text.RegularExpressions;

namespace Simargl.Code;

/// <summary>
/// Представляет методы для проверки синтаксиса.
/// </summary>
public sealed partial class SyntaxValidator
{
    /// <summary>
    /// Выполняет проверку идентификатора.
    /// </summary>
    /// <param name="identifier">
    /// Идентификатор.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public static bool IsIdentifier(string identifier)
    {
        //  Проверка.
        return IdentifiesRegex().IsMatch(identifier);
    }

    /// <summary>
    /// Выполняет проверку пространства имён.
    /// </summary>
    /// <param name="namespace">
    /// Пространство имён.
    /// </param>
    /// <param name="separator">
    /// Разделитель пространства имён.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public static bool IsNamespace(string @namespace, string separator)
    {
        //  Разбивка.
        string[] paths = @namespace.Split(separator);

        //  Перебор частей.
        foreach (string path in paths)
        {
            //  Проверка части.
            if (!IsIdentifier(path))
            {
                //  Не является пространством имён.
                return false;
            }
        }

        //  Проверка пройдена.
        return true;
    }

    /// <summary>
    /// Выполняет проверку целочисленного литерала.
    /// </summary>
    /// <param name="literal">Строка с целочисленным литералом.</param>
    /// <returns>Истина, если строка является корректным целочисленным литералом.</returns>
    public static bool IsIntegerLiteral(string literal)
    {
        return IntegerLiteralRegex().IsMatch(literal);
    }

    [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex IdentifiesRegex();

    // Разрешает: 123, -42, +17, 0x1A3F, 0b1010, 0B0101, 0XFF
    [GeneratedRegex(@"^[+-]?(0x[0-9A-Fa-f]+|0b[01]+|[0-9]+)$")]
    private static partial Regex IntegerLiteralRegex();
}
