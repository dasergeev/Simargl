using Apeiron.Platform.Utilities.Generation.Schemes;
using System.Text.Json.Serialization;

namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет модель сущности.
/// </summary>
public sealed class EntityModel :
    Model<EntityModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public EntityModel()
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="scheme">
    /// Схема сущнсоти.
    /// </param>
    public EntityModel(EntityScheme scheme)
    {
        Name = scheme.Name;
        Category = scheme.Category;
        TypeName = scheme.TypeName;
        CollectionName = scheme is FinalEntityScheme final ? final.TableName : null!;
        ParentTypeName = scheme.ParentTypeName;
        IsFinal = scheme.IsFinal;

        foreach (PropertyScheme property in scheme.Properties)
        {
            if (property is SimplePropertyScheme simple)
            {
                Properties.Add(new()
                {
                    Name = simple.Name,
                    DisplayName = simple.DisplayName,
                    PropertyName = simple.PropertyName,
                    TypeName = simple.TypeName + (simple.Positive ? "Positive" : string.Empty) + (simple.Nullable ? "Nullable" : string.Empty),
                    IsUniquable = simple.Uniquable,
                    IsIndexable = simple.Indexable,
                });
            }

            if (property is RelationPropertyScheme relation)
            {
                Relations.Add(new()
                {
                    Name = relation.Name,
                    EntityTypeName = relation.TypeName,
                    MayEmpty = relation.Nullable,
                    PropertyName = relation.PropertyName,
                    RelationCollectionName = relation.RelationCollectionName,
                    IsCascade = relation.Cascade,
                });
            }
        }

        foreach (AlternateKeyScheme alternateKey in scheme.AlternateKeys)
        {
            AlternateKeys.Add(new()
            {
                Name = alternateKey.Name,
                Fields = new(alternateKey.Fields),
            });
        }
    }

    /// <summary>
    /// Возвращает название сущности.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Возвращает категорию сущности.
    /// </summary>
    public string Category { get; init; } = null!;

    /// <summary>
    /// Возвращает имя типа сущности.
    /// </summary>
    public string TypeName { get; init; } = null!;

    /// <summary>
    /// Возвращает имя коллекции сущностей.
    /// </summary>
    public string CollectionName { get; init; } = null!;

    /// <summary>
    /// Возвращает имя типа родительской сущности.
    /// </summary>
    public string ParentTypeName { get; init; } = null!;

    /// <summary>
    /// Возвращает значение, определяющее, является ли сущность итоговой.
    /// </summary>
    public bool IsFinal { get; init; }

    /// <summary>
    /// Возвращает коллекцию моделей свойств сущности.
    /// </summary>
    public EntityPropertyModelCollection Properties { get; init; } = new();

    /// <summary>
    /// Возвращает коллекцию моделей связей сущности.
    /// </summary>
    public RelationModelCollection Relations { get; init; } = new();

    /// <summary>
    /// Возвращает коллекцию альтернативных ключей.
    /// </summary>
    public AlternateKeyModelCollection AlternateKeys { get; init; } = new();

    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    [JsonIgnore]
    public string ParameterName => TypeName[..1].ToLower() + TypeName[1..];

    /// <summary>
    /// Возвращает коллекцию внешних связей.
    /// </summary>
    [JsonIgnore]
    public List<RelationModel> ExternalRelations { get; } = new();

    /// <summary>
    /// Возвращает родительскую сущность.
    /// </summary>
    [JsonIgnore]
    public EntityModel? ParentModel { get; set; }

    /// <summary>
    /// Выполняет построение модели объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public override void Build([ParameterNoChecks] Generator generator)
    {
        //  Построение моделей свойств сущности.
        Properties.BuildModels(generator);

        //  Проверка связей.
        if (IsFinal)
        {
            //  Настройка связей.
            Relations.ForEach(relationModel => relationModel.ContainsEntityModel = this);
        }
        else
        {
            //  Проверка связей.
            if (Relations is not null && Relations.Count != 0)
            {
                //  Выброс исключения.
                throw new InvalidDataException("У промежуточной сущности не может быть связей.");
            }
        }
    }

    /// <summary>
    /// Выполняет генерацию файла.
    /// </summary>
    /// <param name="generator">
    /// Генератор.
    /// </param>
    public void Generate([ParameterNoChecks] Generator generator)
    {
        //  Формирование пути к файлу.
        string path = Path.Combine(generator.RootPath, "Entities", Category, TypeName + ".g.cs");

        //  Создание средства записи исходного кода.
        using SourceFileWriter writer = new(path);

        //  Запись используемых пространств имён.
        writer.WriteLine("using System.ComponentModel;");
        writer.WriteLine("using System.Data.Entity.Core;");
        writer.WriteLine();

        //  Запись пространства имён.
        writer.WriteLine($"namespace Apeiron.Platform.Databases.CentralDatabase.Entities;");
        writer.WriteLine();

        //  Запись начала сущности.
        writer.WriteLine(
$@"/// <summary>
/// Представляет сущность базы данных ""{Name}"".
/// </summary>
public { (IsFinal ? "sealed" : "abstract")} partial class {TypeName} :
    {ParentTypeName}");

        //  Запись начала блока.
        writer.BeginBlock();

        //  Запись постоянных, содержащих имена свойств.
        Properties.ForEach(property => property.WriteConstantName(writer));
        Relations.ForEach(relation => relation.WriteConstantNames(writer));

        //  Запись статических полей, содержащих аргументы для события изменения значения свойста.
        Properties.ForEach(property => property.WritePropertyChangedEventArgs(writer));
        Relations.ForEach(relation => relation.WritePropertiesChangedEventArgs(writer));

        //  Запись полей свойств.
        Properties.ForEach(property => property.WriteField(writer));
        Relations.ForEach(relation => relation.WriteFields(writer));

        //  Запись полного конструктора.
        WriteFullConstructor(writer);

        //  Запись основного конструктора.
        WriteMainConstructor(writer);

        //  Запись свойств.
        Properties.ForEach(property => property.WriteProperty(writer));
        Relations.ForEach(relation => relation.WriteProperties(writer));

        //  Запись свойств внешних связей.
        ExternalRelations.ForEach(relation =>
        {
            //  Запись кода свойства, возвращающего коллекцию.
            writer.WriteLine(
$@"/// <summary>
/// Возвращает коллекцию связанных сущностей базы данных ""{relation.ContainsEntityModel.Name}"".
/// </summary>
public EntityCollection<{relation.ContainsEntityModel.TypeName}> {relation.RelationCollectionName} {{ get; }}");

            //  Пропуск строки.
            writer.WriteLine();
        });

        //  Запись обработчиков событий.
        Relations.ForEach(relation => relation.WriteEventHandler(writer));

        ////  Запись метода, прикрепляющего сущность к контексту.
        //WriteAttachToContext(writer);

        //  Запись метода настройки данного типа сущности в модели.
        WriteBuildAction(writer);

        //  Запись конца блока класса.
        writer.EndBlock();
        writer.WriteLine();
    }

    /// <summary>
    /// Возвращает коллекцию свойств родителя.
    /// </summary>
    /// <returns>
    /// Коллекция свойств родителя.
    /// </returns>
    public List<EntityPropertyModel> GetParentProperties()
    {
        //  Создание списка.
        List<EntityPropertyModel> properties = new();

        //  Проверка родительской сущности.
        if (ParentModel is not null)
        {
            //  Загрузка прародительских свойств.
            properties.AddRange(ParentModel.GetParentProperties());

            //  Загрузка родительских свойств.
            properties.AddRange(ParentModel.Properties);
        }

        //  Возврат списка.
        return properties;
    }

    /// <summary>
    /// Возвращает коллекцию всех свойств.
    /// </summary>
    /// <returns>
    /// Коллекция всех свойств.
    /// </returns>
    public List<EntityPropertyModel> GetAllProperties()
    {
        //  Создание списка.
        List<EntityPropertyModel> properties = new();

        //  Получение свойств родителя.
        properties.AddRange(GetParentProperties());

        //  Загрузка собственных свойств.
        properties.AddRange(Properties);

        //  Возврат списка.
        return properties;
    }

    /// <summary>
    /// Возвращает коллекцию альтернативных ключей родителя.
    /// </summary>
    /// <returns>
    /// Коллекция альтернативных ключей родителя.
    /// </returns>
    public List<AlternateKeyModel> GetParentAlternateKeys()
    {
        //  Создание списка.
        List<AlternateKeyModel> keys = new();

        //  Проверка родительской сущности.
        if (ParentModel is not null)
        {
            //  Загрузка прародительских ключей.
            keys.AddRange(ParentModel.GetParentAlternateKeys());

            //  Загрузка родительских ключей.
            keys.AddRange(ParentModel.AlternateKeys);
        }

        //  Возврат списка.
        return keys;
    }

    /// <summary>
    /// Возвращает коллекцию всех альтернативных ключей.
    /// </summary>
    /// <returns>
    /// Коллекция всех альтернативных ключей.
    /// </returns>
    public List<AlternateKeyModel> GetAllAlternateKeys()
    {
        //  Создание списка.
        List<AlternateKeyModel> keys = new();

        //  Получение ключей родителя.
        keys.AddRange(GetParentAlternateKeys());

        //  Загрузка собственных ключей.
        keys.AddRange(AlternateKeys);

        //  Возврат списка.
        return keys;
    }

    /// <summary>
    /// Выполняет запись полного конструктора.
    /// </summary>
    /// <param name="writer">
    /// Средство записи исходного кода.
    /// </param>
    private void WriteFullConstructor([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Получение всех свойств.
        List<EntityPropertyModel> allProperties = GetAllProperties();

        //  Получение родительских свойств.
        List<EntityPropertyModel> parentProperties = GetParentProperties();

        //  Запись описания конструктора.
        writer.WriteLine("/// <summary>");
        writer.WriteLine("/// Инициализирует новый экземпляр класса.");
        writer.WriteLine("/// </summary>");

        //  Запись описания идентификатора.
        writer.WriteLine("/// <param name=\"id\">");
        writer.WriteLine("/// Идентификатор сущности.");
        writer.WriteLine("/// </param>");

        //  Запись описания параметров свойств.
        allProperties.ForEach(property =>
        {
            writer.WriteLine($"/// <param name=\"{property.ParameterName}\">");
            writer.WriteLine($"/// {property.Name}.");
            writer.WriteLine("/// </param>");
        });

        //  Проверка итоговой сущности.
        if (IsFinal)
        {
            //  Запись описания параметров связей.
            Relations.ForEach(relation =>
            {
                writer.WriteLine($"/// <param name=\"{relation.ParameterName}Id\">");
                writer.WriteLine($"/// Идентификатор свойства \"{relation.Name}\".");
                writer.WriteLine("/// </param>");
            });
        }

        //  Запись описания исключений параметров.
        allProperties.ForEach(property => property.GetTypeModel().Checking
            .WriteParameterDescriptionException(writer, property.ParameterName));
        //{
        //    //  Получение текста описания исключения.
        //    string text = property.GetTypeModel().GetCheckingModel().DescriptionExceptionParameter.ToString();

        //    //  Проверка текста описания исключения.
        //    if (text.Length != 0)
        //    {
        //        //  Вывод текста описания исключения.
        //        writer.WriteLine(string.Format(text, property.ParameterName));
        //    }
        //});

        //  Запись начала сигнатуры конструктора.
        writer.Write($"internal {TypeName}(long id");

        //  Запись параметров свойств.
        allProperties.ForEach(property =>
        {
            writer.WriteLine(",");
            writer.Write($"    {property.GetTypeModel().CodeTypeName} {property.ParameterName}");
        });

        //  Запись параметров связей.
        Relations.ForEach(relation =>
        {
            writer.WriteLine(",");
            writer.Write($"    long{(relation.MayEmpty ? "?" : string.Empty)} {relation.ParameterName}Id");
        });

        //  Запись конца сигнатуры конструктора.
        writer.WriteLine(") :");

        //  Запись начала вызова конструктора базовой сущности.
        writer.Write("    base(id");

        //  Запись параметров родительских свойств.
        parentProperties.ForEach(property => writer.Write($", {property.ParameterName}"));

        //  Запись завершения вызова конструктора базовой сущности.
        writer.WriteLine(")");

        //  Начало блока конструктора.
        writer.BeginBlock();

        //  Запись кода установки значений свойств.
        Properties.ForEach(property =>
        {
            //  Запись комментария.
            writer.WriteLine($"//  Установка значения свойства \"{property.Name}\".");

            //  Запись выражения проверки.
            property.GetTypeModel().Checking.WriteExpression(
                writer, $"_{property.PropertyName}", property.ParameterName, $"nameof({property.ParameterName})");


            ////  Получение строки проверки.
            //string expression = property.GetTypeModel().GetCheckingModel().Expression.ToString();

            ////  Проверка строки.
            //if (expression.Length != 0)
            //{
            //    //  Запись выражения проверки.
            //    writer.WriteLine(string.Format(expression, $"_{property.PropertyName}", property.ParameterName, $"nameof({property.ParameterName})"));
            //}
            //else
            //{
            //    //  Запись выражения установки значения.
            //    writer.WriteLine($"_{property.PropertyName} = {property.ParameterName};");
            //}

            //  Пропуск строки.
            writer.WriteLine("");
        });

        //  Запись кода установки идентификаторов связей.
        Relations.ForEach(relation =>
        {
            //  Запись комментария.
            writer.WriteLine($"//  Установка идентификатора свойства \"{relation.Name}\".");

            //  Запись выражения установки идентификатора.
            writer.WriteLine($"_{relation.PropertyName}Id = {relation.ParameterName}Id;");

            //  Пропуск строки.
            writer.WriteLine("");
        });

        //  Запись кода для создания объектов синхронизации.
        Relations.ForEach(relation =>
        {
            //  Запись комментария.
            writer.WriteLine($"//  Создание объекта, с помощью которого можно синхронизироват доступ к значению свойства \"{relation.Name}\".");

            //  Запись выражения создания объекта синхронизации.
            writer.WriteLine($"_{relation.PropertyName}Sync = new();");

            //  Пропуск строки.
            writer.WriteLine("");

            //  Проверка необязательной связи.
            if (relation.MayEmpty)
            {
                //  Запись комментария.
                writer.WriteLine($"//  Установка значения, определяющего загружены ли данные свойства \"{relation.Name}\".");

                //  Запись выражения установки значения.
                writer.WriteLine($"_{relation.PropertyName}IsLoad = false;");

                //  Пропуск строки.
                writer.WriteLine("");
            }
        });

        //  Запись кода создания связанных коллекций.
        ExternalRelations.ForEach(relation =>
        {
            //  Запись комментария.
            writer.WriteLine($"//  Создание коллекции связанных сущностей базы данных \"{relation.ContainsEntityModel.Name}\".");

            //  Запись выражения создания коллекции.
            writer.WriteLine($"{relation.RelationCollectionName} = new(session => session.{relation.ContainsEntityModel.CollectionName}");
            writer.IndentPush();
            writer.WriteLine($".Where({relation.ContainsEntityModel.ParameterName} => {relation.ContainsEntityModel.ParameterName}.{relation.PropertyName}Id == Id));");
            writer.IndentPop();

            //  Пропуск строки.
            writer.WriteLine("");
        });

        //  Завершение блока конструктора.
        writer.EndBlock();

        //  Пропуск строки.
        writer.WriteLine("");
    }

    /// <summary>
    /// Выполняет запись основного конструктора.
    /// </summary>
    /// <param name="writer">
    /// Средство записи исходного кода.
    /// </param>
    private void WriteMainConstructor([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Проверка итоговой сущности.
        if (!IsFinal)
        {
            //  Основной конструктор добавляется только к итоговой сущности.
            return;
        }

        //  Получение всех свойств.
        List<EntityPropertyModel> allProperties = GetAllProperties();

        //  Получение родительских свойств.
        List<EntityPropertyModel> parentProperties = GetParentProperties();

        //  Запись описания конструктора.
        writer.WriteLine("/// <summary>");
        writer.WriteLine("/// Инициализирует новый экземпляр класса.");
        writer.WriteLine("/// </summary>");

        //  Запись описания параметров свойств.
        allProperties.ForEach(property =>
        {
            //  Проверка строки.
            if (property.GetTypeModel().Checking != CheckingScheme.IsAnything)
            {
                writer.WriteLine($"/// <param name=\"{property.ParameterName}\">");
                writer.WriteLine($"/// {property.Name}.");
                writer.WriteLine("/// </param>");
            }


            ////  Получение строки проверки.
            //string expression = property.GetTypeModel().GetCheckingModel().Expression.ToString();

            ////  Проверка строки.
            //if (expression.Length != 0)
            //{
            //    writer.WriteLine($"/// <param name=\"{property.ParameterName}\">");
            //    writer.WriteLine($"/// {property.Name}.");
            //    writer.WriteLine("/// </param>");
            //}
        });

        //  Запись описания параметров связей.
        Relations.ForEach(relation =>
        {
            //  Проверка обязательной связи.
            if (!relation.MayEmpty)
            {
                writer.WriteLine($"/// <param name=\"{relation.ParameterName}Id\">");
                writer.WriteLine($"/// Идентификатор свойства \"{relation.Name}\".");
                writer.WriteLine("/// </param>");
            }
        });

        //  Запись описания исключений свойств.
        allProperties.ForEach(property => property.GetTypeModel().Checking
            .WritePropertyDescriptionException(writer));
        //{
        //    //  Получение текста описания исключения.
        //    string text = property.GetTypeModel().GetCheckingModel().DescriptionExceptionParameter.ToString();

        //    //  Проверка текста описания исключения.
        //    if (text.Length != 0)
        //    {
        //        //  Вывод текста описания исключения.
        //        writer.WriteLine(string.Format(text, property.ParameterName));
        //    }
        //});

        ////  Запись исключений параметров связей.
        //Relations.ForEach(relation =>
        //{
        //    //  Проверка обязательной связи.
        //    if (!relation.MayEmpty)
        //    {
        //        writer.WriteLine($"/// <exception cref=\"ArgumentNullException\">");
        //        writer.WriteLine($"/// В параметре <paramref name=\"{relation.ParameterName}\"/> передана пустая ссылка.");
        //        writer.WriteLine("/// </exception>");
        //    }
        //});

        //  Запись начала сигнатуры конструктора.
        writer.Write($"public {TypeName}(");

        //  Значение, определяющее, является ли параметр первым.
        bool isFirstParameter = true;

        //  Запись параметров свойств.
        allProperties.ForEach(property =>
        {
            ////  Получение строки проверки.
            //string expression = property.GetTypeModel().GetCheckingModel().Expression.ToString();

            //  Проверка строки.
            if (property.GetTypeModel().Checking != CheckingScheme.IsAnything)
            {
                //  Проверка первого параметра.
                if (isFirstParameter)
                {
                    isFirstParameter = false;
                }
                else
                {
                    writer.WriteLine(",");
                    writer.Write("    ");
                }
                writer.Write($"{property.GetTypeModel().CodeTypeName} {property.ParameterName}");
            }
        });

        //  Запись параметров связей.
        Relations.ForEach(relation =>
        {
            //  Проверка обязательной связи.
            if (!relation.MayEmpty)
            {
                //  Проверка первого параметра.
                if (isFirstParameter)
                {
                    isFirstParameter = false;
                }
                else
                {
                    writer.WriteLine(",");
                    writer.Write("    ");
                }
                //writer.Write($"{relation.EntityTypeName} {relation.ParameterName}");
                writer.Write($"long {relation.ParameterName}Id");
            }
        });

        //  Запись конца сигнатуры конструктора.
        writer.WriteLine(") :");

        //  Запись начала вызова полного конструктора.
        writer.Write("    this(default");

        //  Запись параметров свойств.
        allProperties.ForEach(property =>
        {
            ////  Получение строки проверки.
            //string expression = property.GetTypeModel().GetCheckingModel().Expression.ToString();

            //  Проверка строки.
            if (property.GetTypeModel().Checking != CheckingScheme.IsAnything)
            {
                writer.Write($", {property.ParameterName}");
            }
            else
            {
                writer.Write($", default");
            }
        });

        //  Запись параметров связей.
        Relations.ForEach(relation =>
        {
            writer.WriteLine(",");
            writer.Write("    ");

            //  Проверка обязательной связи.
            if (!relation.MayEmpty)
            {
                writer.Write($"{relation.ParameterName}Id");
                //writer.Write($"Check.IsNotNull({relation.ParameterName}, nameof({relation.ParameterName})).Id");
            }
            else
            {
                writer.Write("default");
            }
        });

        //  Запись завершения вызова конструктора базовой сущности.
        writer.WriteLine(")");

        //  Начало блока конструктора.
        writer.BeginBlock();

        //  Пропуск строки.
        writer.WriteLine();

        //  Завершение блока конструктора.
        writer.EndBlock();

        //  Пропуск строки.
        writer.WriteLine();
    }

    ///// <summary>
    ///// Выполняет запись метода, прикрепляющего сущность к контексту.
    ///// </summary>
    ///// <param name="writer">
    ///// Средство записи исходного кода.
    ///// </param>
    //private void WriteAttachToContext([ParameterNoChecks] SourceFileWriter writer)
    //{
    //    //  Проверка итоговой сущности.
    //    if (IsFinal)
    //    {
    //        //  Запись заголовка метода.
    //        writer.WriteLine("/// <summary>");
    //        writer.WriteLine("/// Прикрепляет сущность к контексту.");
    //        writer.WriteLine("/// </summary>");
    //        writer.WriteLine("/// <param name=\"attachment\">");
    //        writer.WriteLine("/// Объект, для прикрепления сущностей к контексту.");
    //        writer.WriteLine("/// </param>");
    //        writer.WriteLine("internal override void AttachToContext([ParameterNoChecks] Attachment attachment)");

    //        //  Начало блока метода.
    //        writer.BeginBlock();

    //        //  Проверка простой записи.
    //        if (Relations.Count == 0 && ExternalRelations.Count == 0)
    //        {
    //            //  Добавление простой записи.
    //            writer.WriteLine("//  Прикрепление сущности.");
    //            writer.WriteLine("attachment.Attach(this);");
    //        }
    //        else
    //        {
    //            //  Начало записи основного кода.
    //            writer.WriteLine("//  Прикрепление сущности и проверка необходимости прикрепления связанных сущностей.");
    //            writer.WriteLine("if (attachment.Attach(this))");

    //            //  Начало блока прикрепления.
    //            writer.BeginBlock();

    //            //  Запись кода для прикрепления внутренних сущностей.
    //            Relations.ForEach(relation =>
    //            {
    //                //  Запись комментария.
    //                writer.WriteLine($"//  Прикрепление свойства \"{relation.Name}\".");

    //                //  Запись выражения прикрепления внутренней сущности.
    //                writer.WriteLine($"_{relation.PropertyName}?.AttachToContext(attachment);");

    //                //  Пропуск строки.
    //                writer.WriteLine();
    //            });

    //            //  Запись кода для прикрепления внешних сущностей.
    //            ExternalRelations.ForEach(relation =>
    //            {
    //                //  Запись комментария.
    //                writer.WriteLine($"//  Прикрепление коллекции сущностей \"{relation.ContainsEntityModel.Name}\".");

    //                //  Запись выражения прикрепления внешних сущностей.
    //                writer.WriteLine($"{relation.RelationCollectionName}.AttachToContext(attachment);");

    //                //  Пропуск строки.
    //                writer.WriteLine();
    //            });

    //            //  Завершение блока прикрепления.
    //            writer.EndBlock();
    //        }

    //        //  Завершение блока метода.
    //        writer.EndBlock();

    //        //  Пропуск строки.
    //        writer.WriteLine();
    //    }
    //}

    /// <summary>
    /// Выполняет запись метода настройки данного типа сущности в модели.
    /// </summary>
    /// <param name="writer">
    /// Средство записи исходного кода.
    /// </param>
    private void WriteBuildAction([ParameterNoChecks] SourceFileWriter writer)
    {
        //  Проверка итоговой сущности.
        if (IsFinal)
        {
            //  Запись начала метода настройки данного типа сущности в модели.
            writer.WriteLine(
$@"/// <summary>
/// Выполняет настройку данного типа сущности в модели.
/// </summary>
/// <param name=""typeBuilder"">
/// Построитель типа.
/// </param>
[MethodImpl(MethodImplOptions.AggressiveInlining)]
internal static void BuildAction([ParameterNoChecks] EntityTypeBuilder<{TypeName}> typeBuilder)");

            //  Начало блока метода.
            writer.BeginBlock();

            //  Запись базовой части метода.
            writer.WriteLine(
$@"//  Установка имени таблицы.
typeBuilder.ToTable(""{CollectionName}"");

//  Указание того, как контекст обнаруживает
//  изменения свойств для экземпляра типа сущности.
typeBuilder.HasChangeTrackingStrategy(
    //  Свойства помечаются как измененные,
    //  когда сущность вызывает событие PropertyChanged.
    ChangeTrackingStrategy.ChangedNotifications);

//  Настройка идентификатора сущности.
typeBuilder.HasKey({ParameterName} => {ParameterName}.Id);
typeBuilder.Property({ParameterName} => {ParameterName}.Id)
    .ValueGeneratedOnAdd();
typeBuilder.HasIndex({ParameterName} => {ParameterName}.Id);");

            //  Пропуск строки.
            writer.WriteLine();

            //  Перебор всех свойств.
            foreach (EntityPropertyModel property in GetAllProperties())
            {
                //  Запись комментария.
                writer.WriteLine($"//  Настройка свойства \"{property.Name}\".");

                //  Объявление свойства.
                writer.WriteLine($"typeBuilder.Property({ParameterName} => {ParameterName}.{property.PropertyName});");

                //  Проверка уникального значения.
                if (property.IsUniquable)
                {
                    //  Установка альтернативного ключа.
                    writer.WriteLine($"typeBuilder.HasAlternateKey({ParameterName} => {ParameterName}.{property.PropertyName});");
                }

                //  Проверка индексирования.
                if (property.IsIndexable)
                {
                    //  Установка индекса.
                    writer.WriteLine($"typeBuilder.HasIndex({ParameterName} => {ParameterName}.{property.PropertyName});");
                }

                //  Пропуск строки.
                writer.WriteLine();
            }

            //  Перебор всех связей.
            foreach (RelationModel relation in Relations)
            {
                //  Настройка идентификатора.
                writer.WriteLine($"//  Настройка идентификатора свойства \"{relation.Name}\".");
                writer.WriteLine($"typeBuilder.Property({ParameterName} => {ParameterName}.{relation.PropertyName}Id);");
                writer.WriteLine($"typeBuilder.HasIndex({ParameterName} => {ParameterName}.{relation.PropertyName}Id);");
                writer.WriteLine();

                //  Настройка сущности.
                writer.WriteLine($"//  Настройка свойства \"{relation.Name}\".");
                writer.WriteLine($"typeBuilder.HasOne({ParameterName} => {ParameterName}.{relation.PropertyName})");
                writer.IndentPush();
                writer.WriteLine($".WithMany({relation.GetEntityModel().ParameterName} => {relation.GetEntityModel().ParameterName}.{relation.RelationCollectionName})");
                writer.WriteLine($".HasForeignKey({ParameterName} => {ParameterName}.{relation.PropertyName}Id)");
                if (relation.IsCascade)
                {
                    writer.WriteLine($".OnDelete(DeleteBehavior.Cascade)");
                }
                else
                {
                    writer.WriteLine($".OnDelete(DeleteBehavior.NoAction)");
                }
                writer.WriteLine($".HasConstraintName(\"FK_{CollectionName}_{relation.GetEntityModel().CollectionName}\");");
                writer.IndentPop();
                writer.WriteLine();
            }

            //  Перебор всех ключей.
            foreach (AlternateKeyModel key in AlternateKeys)
            {
                //  Вывод комментария
                writer.WriteLine($"//  Настройка ключа \"{key.Name}\".");

                //  Начало записи ключа.
                writer.WriteLine($"typeBuilder.HasAlternateKey({ParameterName} => new");
                writer.WriteLine("{");
                writer.IndentPush();

                //  Перебор полей ключа.
                foreach (string field in key.Fields)
                {
                    //  Запись поля.
                    writer.WriteLine($"{ParameterName}.{field},");
                }

                //  Конец записи ключа.
                writer.IndentPop();
                writer.WriteLine("});");

                //  Пропуск строки.
                writer.WriteLine();
            }

            //  Конец блока метода.
            writer.EndBlock();

            //  Пропуск строки.
            writer.WriteLine();
        }
    }
}
