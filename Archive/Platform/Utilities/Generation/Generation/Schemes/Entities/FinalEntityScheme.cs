namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет итоговую сущность.
/// </summary>
public sealed class FinalEntityScheme :
    EntityScheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="generalScheme">
    /// Общая схема.
    /// </param>
    /// <param name="sourceType">
    /// Исходный тип.
    /// </param>
    internal FinalEntityScheme(
        [ParameterNoChecks] GeneralScheme generalScheme,
        [ParameterNoChecks] Type sourceType) :
        base(generalScheme, sourceType, true)
    {
        //  Установка имени таблицы.
        TableName = ((TableNameAttribute)sourceType.GetCustomAttributes(
            typeof(TableNameAttribute), false).First()).TableName;

    }

    /// <summary>
    /// Возвращает имя таблицы.
    /// </summary>
    public string TableName { get; }
}
