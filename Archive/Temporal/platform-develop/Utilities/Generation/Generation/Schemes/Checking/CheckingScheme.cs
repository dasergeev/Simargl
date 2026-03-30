using Apeiron.Platform.Utilities.Generation.CodeElements;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему проверки значения.
/// </summary>
public sealed class CheckingScheme //: Scheme<CheckingScheme>
{
    /// <summary>
    /// Поле для хранения выражения для выполнения проверки.
    /// </summary>
    private readonly string _Expression;

    /// <summary>
    /// Поле для хранения списка описаний исключений.
    /// </summary>
    private readonly List<ExceptionDescription> _ExceptionDescriptions;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="schemeName">
    /// Название схемы проверки.
    /// </param>
    private CheckingScheme([ParameterNoChecks] string schemeName) :
        this(schemeName, string.Empty, Array.Empty<ExceptionDescription>())
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="schemeName">
    /// Название схемы проверки.
    /// </param>
    /// <param name="expression">
    /// Выражение для выполнения проверки.
    /// </param>
    /// <param name="exceptionDescriptions">
    /// Массив описаний исключений.
    /// </param>
    private CheckingScheme(
        [ParameterNoChecks] string schemeName,
        [ParameterNoChecks] string expression,
        [ParameterNoChecks] ExceptionDescription[] exceptionDescriptions)
    {
        //  Установка схемы проверки.
        SchemeName = schemeName;

        //  Установка выражения для выполнения проверки.
        _Expression = expression;

        //  Установка масива описаний исключений.
        _ExceptionDescriptions = new(exceptionDescriptions);
    }

    /// <summary>
    /// Возвращает название схемы проверки.
    /// </summary>
    public string SchemeName { get; }

    /// <summary>
    /// Выполняет запись выражения для выполнения проверки.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    /// <param name="receiving">
    /// Имя переменной, которой присваивается значение.
    /// </param>
    /// <param name="parameter">
    /// Имя проверяемой переменной.
    /// </param>
    /// <param name="parameterName">
    /// Имя параметра.
    /// </param>
    public void WriteExpression(
        [ParameterNoChecks] SourceFileWriter writer,
        [ParameterNoChecks] string receiving,
        [ParameterNoChecks] string parameter,
        [ParameterNoChecks] string parameterName)
    {
        //  Проверка выражения для выполнения проверки.
        if (!string.IsNullOrEmpty(_Expression))
        {
            //  Запись выражения проверки.
            writer.WriteLine(string.Format(_Expression, receiving, parameter, parameterName));
        }
        else
        {
            //  Запись выражения установки значения.
            writer.WriteLine($"{receiving} = {parameter};");
        }
    }

    /// <summary>
    /// Выполняет запись описания исключения для параметра.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    /// <param name="parameterName">
    /// Имя параметра.
    /// </param>
    public void WriteParameterDescriptionException(
        [ParameterNoChecks] SourceFileWriter writer,
        [ParameterNoChecks] string parameterName)
    {
        //  Запись описаний исключений.
        _ExceptionDescriptions.ForEach(
            description => description.WriteParameterDescription(
                writer, parameterName));
    }

    /// <summary>
    /// Выполняет запись описания исключения для свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WritePropertyDescriptionException(
        [ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись описаний исключений.
        _ExceptionDescriptions.ForEach(
            description => description.WritePropertyDescription(writer));
    }


    /// <summary>
    /// Возвращает проверку, которая ничего не делает.
    /// </summary>
    public static CheckingScheme IsAnything { get; } = new CheckingScheme(
        "IsAnything");

    /// <summary>
    /// Возвращает проверку пустой ссылки.
    /// </summary>
    public static CheckingScheme IsNotNull { get; } = new CheckingScheme(
        "IsNotNull",
        "{0} = Check.IsNotNull({1}, {2});",
        new ExceptionDescription[]
        {
            new ExceptionDescription(
                "ArgumentNullException",
                "В параметре <paramref name=\"{0}\"/> передана пустая ссылка.",
                "Передана пустая ссылка.")
        });

    /// <summary>
    /// Возвращает проверку на положительное значение.
    /// </summary>
    public static CheckingScheme IsPositive { get; } = new CheckingScheme(
        "IsPositive",
        "{0} = Check.IsPositive({1}, {2});",
        new ExceptionDescription[]
        {
            new ExceptionDescription(
                "ArgumentOutOfRangeException",
                "В параметре <paramref name=\"{0}\"/> передано отрицательное значение.",
                "Передано отрицательное значение."),
            new ExceptionDescription(
                "ArgumentOutOfRangeException",
                "В параметре <paramref name=\"{0}\"/> передано нулевое значение.",
                "Передано нулевое значение.")
        });
}
