namespace Apeiron.Platform.Utilities.Generation.CodeElements;

/// <summary>
/// Представляет описание исключения.
/// </summary>
public sealed class ExceptionDescription
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="exceptionTypeName">
    /// Имя типа исключения.
    /// </param>
    /// <param name="parameterDescription">
    /// Описание исключения для параметра.
    /// </param>
    /// <param name="propertyDescription">
    /// Описание исключения для свойства.
    /// </param>
    internal ExceptionDescription(
        [ParameterNoChecks] string exceptionTypeName,
        [ParameterNoChecks] string parameterDescription,
        [ParameterNoChecks] string propertyDescription)
    {
        //  Установка имени типа исключения.
        ExceptionTypeName = exceptionTypeName;

        //  Установка описания исключения для параметра.
        ParameterDescription = parameterDescription;

        //  Установка описания исключения для свойства.
        PropertyDescription = propertyDescription;
    }

    /// <summary>
    /// Возвращает имя типа исключения.
    /// </summary>
    public string ExceptionTypeName { get; }

    /// <summary>
    /// Возвращает описание исключения для параметра.
    /// </summary>
    public string ParameterDescription { get; }

    /// <summary>
    /// Возвращает описание исключения для свойства.
    /// </summary>
    public string PropertyDescription { get; }

    /// <summary>
    /// Выполняет запись описания исключения для параметра.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    /// <param name="parameterName">
    /// Имя параметра.
    /// </param>
    public void WriteParameterDescription(
        [ParameterNoChecks] SourceFileWriter writer,
        [ParameterNoChecks] string parameterName)
    {
        //  Начало записи комментария.
        writer.IndentPush("/// ");

        //  Запись описания исключения.
        writer.WriteLine($"<exception cref=\"{ExceptionTypeName}\">");
        writer.WriteLine(string.Format(ParameterDescription, parameterName));
        writer.WriteLine("</exception>");

        //  Завершение записи комментария.
        writer.IndentPop();
    }

    /// <summary>
    /// Выполняет запись описания исключения для свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WritePropertyDescription([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Начало записи комментария.
        writer.IndentPush("/// ");

        //  Запись описания исключения.
        writer.WriteLine($"<exception cref=\"{ExceptionTypeName}\">");
        writer.WriteLine(PropertyDescription);
        writer.WriteLine("</exception>");

        //  Завершение записи комментария.
        writer.IndentPop();
    }
}
