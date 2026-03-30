namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий имя таблицы.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class TableNameAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="tableName">
    /// Имя таблицы.
    /// </param>
    public TableNameAttribute([ParameterNoChecks] string tableName)
    {
        //  Установка имени таблицы.
        TableName = tableName;
    }

    /// <summary>
    /// Возвращает имя таблицы.
    /// </summary>
    public string TableName { get; }
}
