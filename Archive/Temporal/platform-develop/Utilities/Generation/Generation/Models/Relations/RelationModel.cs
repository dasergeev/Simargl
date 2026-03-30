using System.Text.Json.Serialization;

namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет модель связи.
/// </summary>
public sealed class RelationModel :
    Model<RelationModel>
{
    /// <summary>
    /// Поле для хранения модели сущности.
    /// </summary>
    private EntityModel _EntityModel = null!;

    /// <summary>
    /// Возвращает название свойства.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Возвращает имя связанной сущности.
    /// </summary>
    public string EntityTypeName { get; init; } = null!;

    /// <summary>
    /// Возвращает значение, определяющее, может ли связь быть пустой.
    /// </summary>
    public bool MayEmpty { get; init; }

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string PropertyName { get; init; } = null!;

    /// <summary>
    /// Возвращает имя связанной коллекции.
    /// </summary>
    public string RelationCollectionName { get; init; } = null!;

    /// <summary>
    /// Возвращает значение, определяющее, следует ли использовать каскадное удаление.
    /// </summary>
    public bool IsCascade { get; init; }

    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    [JsonIgnore]
    public string ParameterName => PropertyName[..1].ToLower() + PropertyName[1..];

    /// <summary>
    /// Возвращает модель сущности, содержащую связь.
    /// </summary>
    [JsonIgnore]
    public EntityModel ContainsEntityModel { get; set; } = null!;

    /// <summary>
    /// Возвращает модель сущности.
    /// </summary>
    /// <returns></returns>
    public EntityModel GetEntityModel() => _EntityModel;

    /// <summary>
    /// Выполняет построение модели объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public override void Build([ParameterNoChecks] Generator generator)
    {
        //  Получение модели сущности.
        _EntityModel = generator.EntityModels[EntityTypeName];

        //  Добавление внешней связи.
        _EntityModel.ExternalRelations.Add(this);
    }

    /// <summary>
    /// Выполняет запись постоянных, определяющих имена свойств.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteConstantNames([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода для идентификатора.
        writer.WriteLine(
$@"/// <summary>
/// Постоянная, определяющая имя идентификатора свойства ""{Name}"".
/// </summary>
private const string _{PropertyName}IdPropertyName = nameof({PropertyName}Id);");

        //  Пропуск строки.
        writer.WriteLine();

        //  Запись кода для сущности.
        writer.WriteLine(
$@"/// <summary>
/// Постоянная, определяющая имя свойства ""{Name}"".
/// </summary>
private const string _{PropertyName}PropertyName = nameof({PropertyName});");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись статических полей, содержащих аргументы для событий изменения значений свойств.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WritePropertiesChangedEventArgs([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода для идентификатора.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения аргументов события изменения значения идентификатора свойства ""{Name}"".
/// </summary>
private readonly PropertyChangedEventArgs _{PropertyName}IdPropertyChangedEventArgs = new(_{PropertyName}IdPropertyName);");

        //  Пропуск строки.
        writer.WriteLine();

        //  Запись кода для сущности.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения аргументов события изменения значения свойства ""{Name}"".
/// </summary>
private readonly PropertyChangedEventArgs _{PropertyName}PropertyChangedEventArgs = new(_{PropertyName}PropertyName);");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись полей, в которых содержатся данные для свойства.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteFields([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода для идентификатора.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения значения идентификатора свойства ""{Name}"".
/// </summary>
private long{(MayEmpty ? "?" : string.Empty)} _{PropertyName}Id;");

        //  Пропуск строки.
        writer.WriteLine();

        //  Запись кода сущности.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения значения свойства ""{Name}"".
/// </summary>
private {EntityTypeName}? _{PropertyName};");

        //  Пропуск строки.
        writer.WriteLine();

        //  Запись кода для объекта синхронизации.
        writer.WriteLine(
$@"/// <summary>
/// Поле для хранения объекта, с помощью которого можно синхронизироват доступ к значению свойства ""{Name}"".
/// </summary>
private readonly object _{PropertyName}Sync;");

        //  Пропуск строки.
        writer.WriteLine();

        //  Проверка необходимости записи флага загрузки.
        if (MayEmpty)
        {
            //  Запись кода для флага загрузки сущности.
            writer.WriteLine(
$@"/// <summary>
/// Поле для хранения значения, определяющего загружены ли данные свойства ""{Name}"".
/// </summary>
private bool _{PropertyName}IsLoad;");

            //  Пропуск строки.
            writer.WriteLine();
        }
    }

    /// <summary>
    /// Выполняет запись свойств.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteProperties([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода для идентификатора.
        writer.WriteLine(
$@"/// <summary>
/// Возвращает или задаёт идентификатор свойства ""{Name}"".
/// </summary>
public long{(MayEmpty ? "?" : string.Empty)} {PropertyName}Id
{{
    get => _{PropertyName}Id;
    set
    {{
        //  Блокировка критического объекта.
        lock (_{PropertyName}Sync)
        {{
            //  Проверка изменения идентификатора.
            if (_{PropertyName}Id != value)
            {{
                //  Установка нового значения идентификатора.
                _{PropertyName}Id = value;

                //  Проверка изменения сущности.
                if (_{PropertyName} is not null && _{PropertyName}.Id != value)
                {{
                    //  Изменение ссылки на сущность.
                    _{PropertyName} = null;
{(MayEmpty ?
$@"
                    //  Установка значения, определяющего загружены ли данные.
                    _{PropertyName}IsLoad = false;
" :
string.Empty)
}
                    //  Вызов события об изменении сущности.
                    OnPropertyChanged(_{PropertyName}PropertyChangedEventArgs);
                }}

                //  Вызов события об изменении значения идентификатора.
                OnPropertyChanged(_{PropertyName}IdPropertyChangedEventArgs);
            }}
        }}
    }}
}}");

        //  Пропуск строки.
        writer.WriteLine();

        //  Запись кода для сущности.
        writer.WriteLine(
$@"/// <summary>
/// {Name}.
/// </summary>{(MayEmpty ? string.Empty:
$@"
/// <exception cref=""ObjectNotFoundException"">
/// Данные базы данных не загружены.
/// </exception>
/// <exception cref=""ArgumentNullException"">
/// Передана пустая ссылка.
/// </exception>")
}
public {EntityTypeName}{(MayEmpty ? "?" : string.Empty)} {PropertyName}
{{
    get
    {{
        //  Блокировка критического объекта.
        lock (_{PropertyName}Sync)
        {{
            //  Проверка необходимости загрузки.
            if ({(MayEmpty ? $"!_{PropertyName}IsLoad" : $"_{PropertyName} is null")})
            {{
                //  Запрос сущности из базы данных.
                _{PropertyName} = CentralDatabaseAgent.Request(
                    session => session.{_EntityModel.CollectionName}
                        .AsNoTracking()
                        .Where({_EntityModel.ParameterName} => {_EntityModel.ParameterName}.Id == _{PropertyName}Id)
                        .FirstOrDefault());
{(MayEmpty ?
$@"
                //  Установка значения, определяющего загружены ли данные.
                _{PropertyName}IsLoad = true;
" :
$@"
                //  Проверка полученной ссылки.
                if (_{PropertyName} is null)
                {{
                    throw Exceptions.DatabaseDataNotLoaded();
                }}
")
}
                //  Вызов события об изменении сущности.
                OnPropertyChanged(_{PropertyName}PropertyChangedEventArgs);
            }}

            //  Возврат ссылки на сущность.
            return _{PropertyName};
        }}
    }}
    set
    {{{(MayEmpty ? string.Empty :
$@"
        //  Проверка ссылки на сущность.
        value = Check.IsNotNull(value, _{PropertyName}PropertyName);
")
}
        //  Блокировка критического объекта.
        lock (_{PropertyName}Sync)
        {{
            //  Проверка изменения ссылки на сущность.
            if (!ReferenceEquals(_{PropertyName}, value))
            {{
                //  Проверка текущей ссылки.
                if (_{PropertyName} is not null)
                {{
                    //  Удаление обработчика события.
                    _{PropertyName}.IdChanged -= {PropertyName}IdChanged;
                }}

                //  Установка новой ссылки на сущность.
                _{PropertyName} = value;
{(MayEmpty ?
$@"
                //  Установка значения, определяющего загружены ли данные.
                _{PropertyName}IsLoad = false;

                //  Проверка новой ссылки.
                if (value is not null)
                {{
                    //  Установка обработчика события.
                    value.IdChanged += {PropertyName}IdChanged;
                }}
" :
$@"
                //  Установка обработчика события.
                value.IdChanged += {PropertyName}IdChanged;
")
}
                //  Вызов события об изменении сущности.
                OnPropertyChanged(_{PropertyName}PropertyChangedEventArgs);

                //  Установка нового идентификатора.
                {PropertyName}Id = value{(MayEmpty ? "?" : string.Empty)}.Id;
            }}
        }}
    }}
}}");

        //  Пропуск строки.
        writer.WriteLine();
    }

    /// <summary>
    /// Выполняет запись метода, обрабатывающего событие изменения идентификатора сущности.
    /// </summary>
    /// <param name="writer">
    /// Средство записи файла с исходным кодом.
    /// </param>
    public void WriteEventHandler([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Запись кода.
        writer.WriteLine(
$@"/// <summary>
/// Обрабатывает событие изменения идентификатора свойства ""{Name}"".
/// </summary>
/// <param name=""sender"">
/// Объект, создавший событие.
/// </param>
/// <param name=""e"">
/// Аргументы, связанные с событием.
/// </param>
private void {PropertyName}IdChanged(object? sender, EventArgs e)
{{
    //  Блокировка критического объекта.
    lock (_{PropertyName}Sync)
    {{
        //  Проверка ссылки на сущность.
        if (_{PropertyName} is not null)
        {{
            //  Установка нового идентификатора.
            {PropertyName}Id = _{PropertyName}.Id;
        }}
    }}
}}");

        //  Пропуск строки.
        writer.WriteLine();
    }
}
