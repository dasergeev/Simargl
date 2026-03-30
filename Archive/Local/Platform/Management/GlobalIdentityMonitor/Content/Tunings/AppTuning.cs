using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apeiron.Services.GlobalIdentity.Tunings;

/// <summary>
/// Представляет настройки WPF приложения для мониторинга.
/// </summary>
[Serializable]
public sealed class AppTuning
{
    /// <summary>
    /// Хранит основные настройки приложения. 
    /// Композиция классов для формирования структуры Json с узлами.
    /// </summary>
    public MainTuning MainTuning { get; set; } = new();

    /// <summary>
    /// Создаёт экземпляр класса с помощью Lazy реализует Singleton.
    /// </summary>
    private static readonly Lazy<AppTuning> lazy = new(() => new AppTuning());

    /// <summary>
    /// Возвращает экземпляр класса.
    /// </summary>
    public static AppTuning Instance
    {
        get
        {
            return lazy.Value;
        }
    }

    /// <summary>
    /// Задаёт опции серилизации.
    /// </summary>
    private JsonSerializerOptions Options { get; } = new()
    {
        // Задаёт дополнительные пробелы для удобства чтения файла или минимицирует файл
        WriteIndented = true,

        // Игнорируем регистр написания свойств
        PropertyNameCaseInsensitive = true,

        // Игнорируем свойства со значением null если есть nullable свойства
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

        // Разрешаем комментарии в JSON
        ReadCommentHandling = JsonCommentHandling.Skip,

        // Разрешаем завершающие запятые в JSON
        AllowTrailingCommas = true,

        // Разрешаем запись чисел в кавычках
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
    };

    /// <summary>
    /// Приватный конструктор по умолчанию.
    /// </summary>
    private AppTuning()
    {
    }

    /// <summary>
    /// Загружает и проверяет настройки программы из файла.
    /// Если настройки не корректные загружает настройки по умолчанию.
    /// </summary>
    public AppTuning Load()
    {
        try
        {
            // Получение файла конфигурации из JSON файла.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Проекция(связывание) файла настроек с классом.
            configuration.Bind(this);

            // Проверка настроек.
            MainTuning.Validation();
            // Возвращаем загруженные валидированные настройки.
            return this;
        }
        catch (Exception ex)
        {
            if (ex is InvalidDataException)
            {
                MessageBox.Show($"Ошибка загрузки настроек из файла Json!\n" +
                    $"Проверьте корректность формата файла настроек Json\n\n" +
                    $"(Загружены настройки по умолчанию)\n\n\r" +
                    $"{ex.Message}",
                    "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"Ошибка загрузки одного или нескольких параметров из файла настроек Json!\n" +
                    $"Проверьте корректность настроек в файле Json\n\n" +
                    $"(Загружены настройки по умолчанию)\n\n\r" +
                    $"{ex.Message}",
                    "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            // Загружаем настройки по умолчанию.
            MainTuning = new MainTuning();

            return this;
        }
    }

    /// <summary>
    /// Асинхронно сохраняет настройки в файл настроек Json формата.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу или пустая ссылка, если необходимо ...
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сохранение настройки в файл.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Произошла ошибка при сохранении файла.
    /// </exception>
    public static async Task JsonSerializeAsync(string? path, CancellationToken cancellationToken = default)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(true);

        try
        {
            // Если передан путой путь к файлу настроек, то сохраняем рядом с исполняемым файлом.
            path ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            // Cохранение данных
            using FileStream fileStream = new(path, FileMode.OpenOrCreate);

            // Сериализация данных в созданный или открытый файл JSON
            await JsonSerializer.SerializeAsync(fileStream, Instance, Instance.Options, cancellationToken);
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс.
                throw;
            }

            // Выброс исключения.
            throw new IOException($"Ошибка сохранения настроек в файл {path} Json.", ex);
        }
    }
}
