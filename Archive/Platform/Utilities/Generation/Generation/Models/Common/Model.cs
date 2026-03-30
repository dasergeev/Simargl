using System.Text.Json;

namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет модель объекта.
/// </summary>
/// <typeparam name="TModel">
/// Тип модели объекта.
/// </typeparam>
public abstract class Model<TModel>
    where TModel : Model<TModel>
{
    /// <summary>
    /// Выполняет построение модели объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public abstract void Build([ParameterNoChecks] Generator generator);

    /// <summary>
    /// Загружает модель объекта из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить модель объекта.
    /// </param>
    /// <param name="model">
    /// Загруженная модель объекта, если модель удалось загрузить.
    /// </param>
    /// <returns>
    /// Значение, определяющее удалось ли загрузить модель объекта.
    /// </returns>
    public static bool TryLoad([ParameterNoChecks] Stream stream, out TModel model)
    {
        //  Загрузка модели объекта.
        model = JsonSerializer.Deserialize<TModel>(stream)!;

        //  Возврат результата.
        return model is not null;
    }

    /// <summary>
    /// Загружает модель объетка из файла.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <returns>
    /// Загруженная модель объекта.
    /// </returns>
    public static TModel Load([ParameterNoChecks] string path)
    {
        //  Создание потока для чтения файла.
        using FileStream stream = new(path, FileMode.Open, FileAccess.Read);

        //  Загрузка модели из потока.
        if (!TryLoad(stream, out TModel model))
        {
            //  Генерация исключения.
            throw new IOException(
                $"Не удалось загрузить схему объекта типа \"{typeof(TModel).Name}\" из файла \"{path}\"");
        }

        //  Возврат загруженной модели.
        return model;
    }
}
