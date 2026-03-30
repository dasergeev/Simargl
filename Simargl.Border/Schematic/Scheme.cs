using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Simargl.Border.Schematic;

/// <summary>
/// Представляет схему.
/// </summary>
public sealed class Scheme
{
    /// <summary>
    /// Возвращает коллекцию модулей.
    /// </summary>
    public required IReadOnlyList<ModuleScheme> Modules { get; init; }

    /// <summary>
    /// Возвращает коллекцию секций.
    /// </summary>
    public required IReadOnlyList<SectionScheme> Sections { get; init; }

    /// <summary>
    /// Асинхронно создаёт схему.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая схему.
    /// </returns>
    public static async Task<Scheme> CreateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение каталога.
        string? directory = Path.GetDirectoryName(typeof(Scheme).Assembly.Location);

        //  Проверка каталога.
        if (string.IsNullOrEmpty(directory))
        {
            //  Выброс исключения.
            throw new InvalidOperationException("Не удалось опеределить каталог, содержащий схему обработки.");
        }

        //  Определение пути к файлу со схемой.
        string path = Path.Combine(directory, "scheme.json");

        //  Проверка пути к файлу со схемой.
        if (!File.Exists(path))
        {
            //  Выброс исключения.
            throw new InvalidOperationException(
                $"Файл схемы обработки не найден: \"{path}\"");
        }

        //  Создание параметров десериализации.
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = false,
        };

        //  Добавление конвертеров.
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new IPv4AddressJsonConverter());

        //  Открытие потока для чтения файл.
        await using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true);

        //  Десериализация.
        Scheme scheme = await JsonSerializer.DeserializeAsync<Scheme>(
            stream, options, cancellationToken).ConfigureAwait(false) ??
            throw new InvalidOperationException(
                "Не удалось загрузить схему обработки.");

        //  Возврат схемы.
        return scheme;
    }
}
