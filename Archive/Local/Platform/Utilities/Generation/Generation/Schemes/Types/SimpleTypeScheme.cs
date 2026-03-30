namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему простого типа.
/// </summary>
public sealed class SimpleTypeScheme :
    TypeScheme<SimpleTypeScheme>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="typeName">
    /// Имя типа.
    /// </param>
    /// <param name="codeTypeName">
    /// Имя типа, которое используется в коде.
    /// </param>
    /// <param name="checking">
    /// Схема проверки.
    /// </param>
    private SimpleTypeScheme(
        [ParameterNoChecks] string typeName,
        [ParameterNoChecks] string codeTypeName,
        [ParameterNoChecks] CheckingScheme checking)
    {
        //  Установка имени типа.
        TypeName = typeName;

        //  Установка имени типа, которое используется в коде.
        CodeTypeName = codeTypeName;

        //  Установка схемы проверки.
        Checking = checking;
    }

    /// <summary>
    /// Возвращает имя типа.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Возвращает имя типа, которое используется в коде.
    /// </summary>
    public string CodeTypeName { get; }

    /// <summary>
    /// Возвращает схему проверки.
    /// </summary>
    public CheckingScheme Checking { get; }

    /// <summary>
    /// Возвращает схему типа байт.
    /// </summary>
    public static SimpleTypeScheme Byte { get; } = new(
        "Byte",
        "byte",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа время.
    /// </summary>
    public static SimpleTypeScheme DateTime { get; } = new(
        "DateTime",
        "DateTime",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа флага.
    /// </summary>
    public static SimpleTypeScheme Flag { get; } = new(
        "Flag",
        "bool",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа флага.
    /// </summary>
    public static SimpleTypeScheme Boolean { get; } = new(
        "Boolean",
        "bool",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа числа с плавающей точкой.
    /// </summary>
    public static SimpleTypeScheme Float64 { get; } = new(
        "Float64",
        "double",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа числа с плавающей точкой.
    /// </summary>
    public static SimpleTypeScheme Double { get; } = new(
        "Double",
        "double",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа целое.
    /// </summary>
    public static SimpleTypeScheme Int32 { get; } = new(
        "Int32",
        "int",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа положительное целое.
    /// </summary>
    public static SimpleTypeScheme Int32Positive { get; } = new(
        "Int32Positive",
        "int",
        CheckingScheme.IsPositive);

    /// <summary>
    /// Возвращает схему типа целое.
    /// </summary>
    public static SimpleTypeScheme Int64 { get; } = new(
        "Int64",
        "long",
        CheckingScheme.IsAnything);

    /// <summary>
    /// Возвращает схему типа положительное целое.
    /// </summary>
    public static SimpleTypeScheme Int64Positive { get; } = new(
        "Int64Positive",
        "long",
        CheckingScheme.IsPositive);

    /// <summary>
    /// Возвращает схему типа строка.
    /// </summary>
    public static SimpleTypeScheme String { get; } = new(
        "String",
        "string",
        CheckingScheme.IsNotNull);

    /// <summary>
    /// Возвращает схему типа необязательная строка.
    /// </summary>
    public static SimpleTypeScheme StringNullable { get; } = new(
        "StringNullable",
        "string?",
        CheckingScheme.IsAnything);
}
