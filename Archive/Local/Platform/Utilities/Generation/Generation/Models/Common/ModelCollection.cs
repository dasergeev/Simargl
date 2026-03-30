namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет коллекцию моделей объекта.
/// </summary>
/// <typeparam name="TModel">
/// Тип модели объекта.
/// </typeparam>
public abstract class ModelCollection<TModel> :
    List<TModel>
    where TModel : Model<TModel>
{
    /// <summary>
    /// Поле для хранения фильтра файлов.
    /// </summary>
    private readonly string _FileFilter;

    /// <summary>
    /// Поле для хранения метода, извлекающего ключ модели.
    /// </summary>
    private readonly Func<TModel, string> _KeyExtractor;

    /// <summary>
    /// Поле для хранения карты моделей объекта.
    /// </summary>
    private readonly SortedDictionary<string, TModel> _Map;

    /// <summary>
    /// Инициалазирует новый экземпляр класса.
    /// </summary>
    /// <param name="fileFilter">
    /// Фильтр файлов.
    /// </param>
    /// <param name="keyExtractor">
    /// Метод, извлекающий ключ модели.
    /// </param>
    public ModelCollection(
        [ParameterNoChecks] string fileFilter,
        [ParameterNoChecks] Func<TModel, string> keyExtractor)
    {
        //  Установка фильтра файлов.
        _FileFilter = fileFilter;

        //  Установка метода, извлекающего ключ модели.
        _KeyExtractor = keyExtractor;

        //  Создание карты моделей объекта.
        _Map = new();
    }

    /// <summary>
    /// Возвращает модель с указанным ключом.
    /// </summary>
    /// <param name="key">
    /// Ключ модели.
    /// </param>
    /// <returns>
    /// Модель с указанным ключом.
    /// </returns>
    public TModel this[string key] => _Map[key];

    /// <summary>
    /// Загружает элементы коллекции из файлов.
    /// </summary>
    /// <param name="fileInfos">
    /// Коллекция информации о файлах.
    /// </param>
    public void Load([ParameterNoChecks] IEnumerable<FileInfo> fileInfos)
    {
        //  Загрузка моделей объектов.
        IEnumerable<TModel> models = fileInfos
            .Where(fileInfo => fileInfo.Name.Length >= _FileFilter.Length)
            .Where(fileInfo => fileInfo.Name.Substring(fileInfo.Name.Length - _FileFilter.Length, _FileFilter.Length) == _FileFilter)
            .Select(fileInfo => Load(fileInfo.FullName));

        //  Добавление загруженных моделей в коллекцию.
        AddRange(models);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    protected virtual TModel Load(string path)
    {
        return Model<TModel>.Load(path);
    }

    /// <summary>
    /// Выполняет построение моделей объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public void BuildModels(Generator generator)
    {
        //  Очитска карты моделей объектов.
        _Map.Clear();

        //  Перебор всех моделей объектов.
        foreach (TModel model in this)
        {
            //  Проверка ссылки на модель.
            if (model is null)
            {
                throw new InvalidDataException("Найдена пустая ссылка на модель.");
            }

            //  Получение ключа.
            string key = _KeyExtractor(model);

            //  Проверка ссылки на ключ.
            if (key is null)
            {
                throw new InvalidDataException("Найдена пустая ссылка на ключ модели.");
            }

            //  Добавление модели объекта в карту.
            _Map.Add(key, model);
        }

        //  Перебор всех моделей объектов.
        foreach (TModel model in this)
        {
            //  Построение модели.
            model.Build(generator);
        }
    }
}
