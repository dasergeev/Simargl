namespace Simargl.Embedded.Tunings.TuningsEditor.Core;

/// <summary>
/// Представляет простой тип.
/// </summary>
public sealed class SimpleType
{
	/// <summary>
	/// Возвращает коллекцию целочисленных типов.
	/// </summary>
	public static IEnumerable<SimpleType> IntegerTypes { get; } = new SimpleType[]
	{
        new("UInt8", 1),
        new("UInt16", 2),
        new("UInt32", 4),
        new("UInt64", 8),
        new("Int8", 1),
        new("Int16", 2),
        new("Int32", 4),
        new("Int64", 8),
	}.AsReadOnly();

    /// <summary>
    /// Возвращает коллекцию нецелочисленных типов.
    /// </summary>
    public static IEnumerable<SimpleType> NotIntegerTypes { get; } = new SimpleType[]
    {
        new("Float32", 4),
        new("Float64", 8),
        new("Boolean", 1),
        new("Version", 4),
        new("DeviceType", 2),
        new("SerialNumber", 4),
        new("DateOnly", 4),
        new("TimeOnly", 4),
        new("DateTime", 8),
        new("TimeSpan", 8),
        new("IPv4Address", 4),
        new("MacAddress", 6),
    }.AsReadOnly();
    
    /// <summary>
    /// Возвращает коллекцию всех типов.
    /// </summary>
    public static IEnumerable<SimpleType> AllTypes { get; } = Enumerable.Union(IntegerTypes, NotIntegerTypes);

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="name">
    /// Имя типа.
    /// </param>
    /// <param name="size">
    /// Размер типа.
    /// </param>
    private SimpleType(string name, int size)
    {
        //	Установка имени типа.
        Name = name;

        //  Установка размера типа.
        Size = size;
    }

    /// <summary>
    /// Возвращает имя типа.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает размер типа.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Проверяет, содержит ли строка допустимое значение.
    /// </summary>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    /// <remarks>
    /// Реализовано только для <see cref="IntegerTypes"/>.
    /// </remarks>
    public bool IsValue(string value)
    {
        return Name switch
        {
            "UInt8" => byte.TryParse(value, out _),
            "UInt16" => ushort.TryParse(value, out _),
            "UInt32" => uint.TryParse(value, out _),
            "UInt64" => ulong.TryParse(value, out _),
            "Int8" => sbyte.TryParse(value, out _),
            "Int16" => short.TryParse(value, out _),
            "Int32" => int.TryParse(value, out _),
            "Int64" => long.TryParse(value, out _),
            _ => false,
        };
    }

    /// <summary>
    /// Возвращает тип с указанным именем.
    /// </summary>
    /// <param name="name">
    /// Имя типа.
    /// </param>
    /// <returns>
    /// Тип с указанным именем.
    /// </returns>
    public static SimpleType FromString(string name) => AllTypes.First(t => t.Name == name);
}
