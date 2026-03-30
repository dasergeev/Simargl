using Apeiron.Platform.Utilities.Generation.Schemes;
using System.Text.Json.Serialization;

namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет модель свойства сущности.
/// </summary>
public sealed class EntityPropertyModel :
    Model<EntityPropertyModel>
{
    /// <summary>
    /// Поле для хранения модели типа.
    /// </summary>
    private SimpleTypeScheme _TypeModel = null!;

    /// <summary>
    /// Возвращает название свойства.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Возвращает отображаемое имя сущности.
    /// </summary>
    public string? DisplayName { get; init; } = null!;

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string PropertyName { get; init; } = null!;

    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    [JsonIgnore]
    public string ParameterName => PropertyName[..1].ToLower() + PropertyName[1..];

    /// <summary>
    /// Возвращает имя типа значения свойства.
    /// </summary>
    public string TypeName { get; init; } = null!;

    /// <summary>
    /// Возвращает значение, определяющее, является ли значение свойства неповторимым.
    /// </summary>
    public bool IsUniquable { get; init; }

    /// <summary>
    /// Возвращает значение, определяющее, является ли значение свойства индексируемым.
    /// </summary>
    public bool IsIndexable { get; init; }

    /// <summary>
    /// Выполняет построение модели объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public override void Build([ParameterNoChecks] Generator generator)
    {
        //  Установка модели типа.
        _TypeModel = generator.Scheme.SimpleTypes[TypeName];
    }

    /// <summary>
    /// Возвращает модель типа.
    /// </summary>
    /// <returns></returns>
    public SimpleTypeScheme GetTypeModel() => _TypeModel;

    /// <summary>
    /// Выполняет запись постоянной, определяющей имя свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteConstantName([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода.
        writer.WriteLine(
$@"/// <summary>
/// Постоянная, определяющая имя свойства ""{Name}"".
/// </summary>
private const string _{PropertyName}PropertyName = nameof({PropertyName});");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись статического поля, содержащего аргументы для события изменения значения свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WritePropertyChangedEventArgs([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения аргументов события изменения значения свойства ""{Name}"".
/// </summary>
private readonly PropertyChangedEventArgs _{PropertyName}PropertyChangedEventArgs = new(_{PropertyName}PropertyName);");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись поля, в котором содержится свойство.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteField([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения значения свойства ""{Name}"".
/// </summary>
private {_TypeModel.CodeTypeName} _{PropertyName};");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteProperty([ParameterNoChecks] SourceFileWriter writer)
    {
        ////  Получение строки с описанием исключения.
        //string checkingDescription = _TypeModel.GetCheckingModel().DescriptionExceptionProperty.ToString();

        ////  Получение строки с текстом проверки.
        //string checkingExpression = _TypeModel.GetCheckingModel().Expression.ToString();

        //  Запись кода.
        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// {Name}.");
        writer.WriteLine("/// </summary>");
        
        //  Проверка строки с описанием исключения.
        if (_TypeModel.Checking != CheckingScheme.IsAnything)
        {
            //  Вывод строки с описанием исключения.
            _TypeModel.Checking.WritePropertyDescriptionException(writer);
            //writer.WriteLine(checkingDescription);
        }

        ////  Проверка строки с отображаемым именем.
        //if (!string.IsNullOrEmpty(DisplayName))
        //{
        //    //  Добавление атрибута.
        //    writer.WriteLine($"[DisplayName(\"{DisplayName}\")]");
        //}

        writer.WriteLine($"public {_TypeModel.CodeTypeName} {PropertyName}");
        writer.BeginBlock();
        writer.WriteLine($"get => _{PropertyName};");
        writer.WriteLine("set");
        writer.BeginBlock();

        //  Проверка строки с проверкой.
        if (_TypeModel.Checking != CheckingScheme.IsAnything)
        {
            writer.WriteLine("//  Проверка нового значения.");
            _TypeModel.Checking.WriteExpression(
                writer, "value", "value", $"_{PropertyName}PropertyName");

            //writer.WriteLine(string.Format(checkingExpression, "value", "value", $"_{PropertyName}PropertyName"));
            writer.WriteLine();
        }

        writer.WriteLine("//  Проверка изменения значения свойства.");
        writer.WriteLine($"if (_{PropertyName} != value)");
        writer.BeginBlock();
        writer.WriteLine("//  Установка нового значения.");
        writer.WriteLine($"_{PropertyName} = value;");
        writer.WriteLine();
        writer.WriteLine("//  Вызов события об изменении значения свойства.");
        writer.WriteLine($"OnPropertyChanged(_{PropertyName}PropertyChangedEventArgs);");
        writer.EndBlock();
        writer.EndBlock();
        writer.EndBlock();

        //  Пропуск строки.
        writer.WriteLine();
    }
}
