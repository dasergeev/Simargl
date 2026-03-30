using Apeiron.Platform.Utilities.Generation.Models;
using Apeiron.Platform.Utilities.Generation.Schemes;

namespace Apeiron.Platform.Utilities.Generation;

/// <summary>
/// Представляет генератор кода.
/// </summary>
public sealed class Generator
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="rootPath">
    /// Путь к корневому каталогу для генерации файлов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rootPath"/> передана пустая ссылка.
    /// </exception>
    public Generator(string rootPath)
    {
        //  Установка пути к корневому каталогу для генерации файлов.
        RootPath = Check.IsNotNull(rootPath, nameof(rootPath));

        //  Создание общей схемы.
        Scheme = new();

        //  Создание коллекции моделей сущностей.
        EntityModels = new();
    }

    /// <summary>
    /// Возвращает путь к корневому каталогу для генерации файлов.
    /// </summary>
    public string RootPath { get; }

    /// <summary>
    /// Возвращает общую схему.
    /// </summary>
    public GeneralScheme Scheme { get; }

    /// <summary>
    /// Возвращает коллекцию моделей сущностей.
    /// </summary>
    public EntityModelCollection EntityModels { get; }

    /// <summary>
    /// Выполняет работу генератора.
    /// </summary>
    public void Invoke()
    {
        //  Очистка генератора.
        Clear();

        //  Загрузка схем.
        LoadSchemes();

        //  Построение моделей.
        BuildModels();

        //  Генерация кода.
        Generate();
    }

    /// <summary>
    /// Выполняет очистку генератора.
    /// </summary>
    private void Clear()
    {
        //  Очистка коллекции моделей сущностей.
        EntityModels.Clear();
    }

    /// <summary>
    /// Выполняет загрузку схем.
    /// </summary>
    /// <exception cref="IOException">
    /// Не удалось найти каталог, содержащий схемы.
    /// </exception>
    private void LoadSchemes()
    {
        foreach (EntityScheme entity in Scheme.Entities)
        {
            EntityModels.Add(new(entity));
        }
    }

    /// <summary>
    /// Выполняет построение моделей.
    /// </summary>
    private void BuildModels()
    {
        //  Построение коллекции моделей сущностей.
        EntityModels.BuildModels(this);

        //  Загрузка родительских сущностей.
        EntityModels.ForEach(entityModel =>
        {
            //  Проверка родительской сущности.
            if (entityModel.ParentTypeName != "Entity")
            {
                //  Получение родительской сущности.
                entityModel.ParentModel = EntityModels[entityModel.ParentTypeName];
            }
            else
            {
                //  Установка пустой ссылки.
                entityModel.ParentModel = null;
            }
        });

        //  Построение моделей связей сущностей.
        EntityModels.ForEach(entityModel => entityModel.Relations.BuildModels(this));
    }

    /// <summary>
    /// Выполняет генерацию кода.
    /// </summary>
    private void Generate()
    {
        //  Генерация кода сущностей.
        EntityModels.ForEach(entityModel => entityModel.Generate(this));

        //  Генерация кода класса сеанса работы с центральной базой данных.
        SessionGenerate();

        //  Генерация кода класса контекста сеанса работы с центральной базой данных.
        ContextGenerate();
    }

    /// <summary>
    /// Выполняет генерацию кода класса сеанса работы с центральной базой данных.
    /// </summary>
    private void SessionGenerate()
    {
        //  Получение списка итоговых сущностей.
        List<EntityModel> finalEntityModels = EntityModels
            .Where(entityModel => entityModel.IsFinal)
            .ToList();

        //  Создание средства записи исходного кода.
        using SourceFileWriter writer = new(Path.Combine(RootPath, "Common", "Session.g.cs"));

        //  Запись начала класса.
        writer.Write(
$@"namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет сеанс работы с центральной базой данных.
/// </summary>
public sealed partial class Session");

        //  Вывод интерфейсов.
        if (finalEntityModels.Count != 0)
        {
            //  Начало блока описания интерфейсов.
            writer.Write(" :");

            //  Увеличение отступа для вывода интерфейсов.
            writer.IndentPush();

            //  Перебор сущностей.
            for (int i = 0; i < finalEntityModels.Count; i++)
            {
                if (i != 0)
                {
                    writer.Write(",");
                }

                //  Переход на новую строку.
                writer.WriteLine();

                //  Запись реализуемого интерфейса.
                writer.Write($"ITableProvider<{finalEntityModels[i].TypeName}>");
            }

            //  Уменьшение отступа после вывода интерфейсов.
            writer.IndentPop();
        }

        //  Переход на новую строку.
        writer.WriteLine();

        //  Начало блока класса.
        writer.BeginBlock();

        //  Запись поля для хранения контекста сеанса.
        writer.WriteLine(
@"/// <summary>
/// Поле для хранения контекста сеанса работы с центральной базой данных.
/// </summary>
private readonly CentralDatabaseContext _Context;");
        writer.WriteLine();

        //  Запись поля для хранения массива таблиц.
        writer.WriteLine(
@"/// <summary>
/// Поле для хранения массива таблиц.
/// </summary>
private readonly Table[] _Tables;");
        writer.WriteLine();

        //  Запись начала конструктора.
        writer.WriteLine(
@"/// <summary>
/// Инициализирует новый экземпляр класса.
/// </summary>
/// <param name=""context"">
/// Контекст сеанса работы с центральной базой данных.
/// </param>
internal Session(
    [ParameterNoChecks] CentralDatabaseContext context)");

        //  Начало блока конструктора.
        writer.BeginBlock();

        //  Запись установки контекста.
        writer.WriteLine("//  Установка контекста сеанса работы с центральной базой данных.");
        writer.WriteLine("_Context = context;");
        writer.WriteLine();

        //  Запись создания таблиц сущностей.
        finalEntityModels.ForEach(entityModel =>
        {
            //  Запись создания таблицы.
            writer.WriteLine(
$@"//  Создание таблицы сущностей базы данных ""{entityModel.Name}"".
{entityModel.CollectionName} = new(""{entityModel.CollectionName}"", ""{entityModel.Category}"", context.Set<{entityModel.TypeName}>());");

            //  Переход на новую строку.
            writer.WriteLine();
        });

        //  Начало записи создания массива таблиц.
        writer.WriteLine("//  Создание массива таблиц.");
        writer.WriteLine("_Tables = new Table[]");

        //  Начало блока создания массива таблиц.
        writer.BeginBlock();

        //  Запись элементов массива таблиц.
        finalEntityModels.ForEach(entityModel => writer.WriteLine($"{entityModel.CollectionName},"));

        //  Конец блока создания массива таблиц.
        writer.EndBlock(";");

        //  Конец блока конструктора.
        writer.EndBlock();
        writer.WriteLine();

        //  Запись свойств таблиц сущностей.
        finalEntityModels.ForEach(entityModel =>
        {
            //  Запись свойства.
            writer.WriteLine(
$@"/// <summary>
/// Возвращает таблицу сущностей базы данных ""{entityModel.Name}"".
/// </summary>
public Table<{entityModel.TypeName}> {entityModel.CollectionName} {{ get; }}");

            //  Переход на новую строку.
            writer.WriteLine();
        });

        //  Запись начала метода, возвращающего таблицу по имени.
        writer.WriteLine(
@"/// <summary>
/// Возвращает таблицу базы данных с указанным именем.
/// </summary>
/// <param name=""name"">
/// Имя таблицы.
/// </param>
/// <returns>
/// Таблица с указанным именем.
/// </returns>
public Table GetTable(string name)");
        writer.BeginBlock();

        //  Начало оператора выбора по имени.
        writer.WriteLine(
@"//  Разбор имени таблицы.
return name switch");
        writer.BeginBlock();

        //  Запись операций выбора по имени.
        finalEntityModels.ForEach(entityModel => writer.WriteLine($"\"{entityModel.CollectionName}\" => {entityModel.CollectionName},"));

        writer.WriteLine(@"_ => throw new KeyNotFoundException(),");

        //  Конец оператора выбора по имени.
        writer.EndBlock(";");

        //  Конец метода, возвращающего таблицу по имени.
        writer.EndBlock();

        //  Переход на новую строку.
        writer.WriteLine();

        //  Запись начала метода, возвращающего таблицу по типу.
        writer.WriteLine(
@"/// <summary>
/// Возвращает таблицу базы данных с указанным типом сущности.
/// </summary>
/// <typeparam name=""TEntity"">
/// Тип сущности.
/// </typeparam>
/// <returns>
/// Таблица с указанным типом сущности.
/// </returns>
public Table<TEntity> GetTable<TEntity>()
    where TEntity : Entity
{
    //  Проверка интерфейса.
    if (this is ITableProvider<TEntity> provider)
    {
        //  Возврат таблицы.
        return provider.Table;
    }

    //  Генерация исключения.
    throw new KeyNotFoundException();
}");

        //  Переход на новую строку.
        writer.WriteLine();

        //  Запись метода, возвращающего коллекцию всех таблиц.
        writer.WriteLine(
@"/// <summary>
/// Возвращает коллекцию всех таблиц.
/// </summary>
/// <returns>
/// Коллекция всех таблиц.
/// </returns>
public IEnumerable<Table> GetAllTables()
{
    //  Возврат доступной только для чтения коллекции таблиц.
    return new List<Table>(_Tables).AsReadOnly();
}");

        //  Переход на новую строку.
        writer.WriteLine();


        //  Явная реализация интерфейсов.
        finalEntityModels.ForEach(entityModel =>
        {
            //  Запись свойства.
            writer.WriteLine(
$@"/// <summary>
/// Возвращает таблицу сущностей базы данных ""{entityModel.Name}"".
/// </summary>
Table<{entityModel.TypeName}> ITableProvider<{entityModel.TypeName}>.Table => {entityModel.CollectionName};");

            //  Переход на новую строку.
            writer.WriteLine();
        });

        //  Конец блока класса.
        writer.EndBlock();
        writer.WriteLine();
    }


    /// <summary>
    /// Выполняет генерацию кода класса контекста сеанса работы с центральной базой данных.
    /// </summary>
    private void ContextGenerate()
    {
        //  Получение списка итоговых сущностей.
        List<EntityModel> finalEntityModels = EntityModels
            .Where(entityModel => entityModel.IsFinal)
            .ToList();

        //  Создание средства записи исходного кода.
        using SourceFileWriter writer = new(Path.Combine(RootPath, "Common", "Context.g.cs"));


        //  Запись начала класса.
        writer.WriteLine(
@"namespace Apeiron.Platform.Databases.CentralDatabase;

partial class CentralDatabaseContext");

        //  Начало блока класса.
        writer.BeginBlock();

        //  Запись начала метода, выполняющего настройку модели базы данных.
        writer.WriteLine(
@"/// <summary>
/// Выполняет настройку модели базы данных.
/// </summary>
/// <param name=""modelBuilder"">
/// Построитель, используемый для настройки модели базы данных.
/// </param>
private void OnModelCreatingCore([ParameterNoChecks] ModelBuilder modelBuilder)");

        //  Начало блока метода.
        writer.BeginBlock();

        //  Запись создания таблиц сущностей.
        finalEntityModels.ForEach(entityModel =>
        {
            //  Запись создания таблицы.
            writer.WriteLine(
$@"//  Настройка таблицы сущностей базы данных ""{entityModel.Name}"".
 modelBuilder.Entity<{entityModel.TypeName}>({entityModel.TypeName}.BuildAction);");

            //  Переход на новую строку.
            writer.WriteLine();
        });

        //  Конец блока метода.
        writer.EndBlock();

        //  Конец блока класса.
        writer.EndBlock();
        writer.WriteLine();
    }
}
