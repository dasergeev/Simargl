using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Simargl.Engineering.Utilities.CertificateSheet.Core;

/// <summary>
/// Представляет методы для загрузки данных.
/// </summary>
public static class Loader
{
    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public static Heart Heart => ((App)Application.Current).Heart;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных.
    /// </returns>
    public static async Task LoadAsync(string path, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение типа приложения Excel.
        Type type = Type.GetTypeFromProgID("Excel.Application") ??
            throw new InvalidOperationException("Не удалось найти программный идентификатор приложения Excel.");

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание стека объектов.
        Stack<dynamic> stack = [];

        //  Запуск приложения.
        dynamic application = await get(() => Activator.CreateInstance(type) ??
            throw new InvalidOperationException("Не удалось запустить приложение Excel."),
            "Не удалось запустить приложение Excel.");

        //  Блок c гарантированным завершением.
        try
        {
            //  Установка видимости приложения.
            application.Visible = false;

            //  Получение коллекции рабочих книг.
            dynamic workbooks = await get(() => application.Workbooks, "Не удалось получить доступ к рабочим книгам Excel..");

            //  Загрузка рабочей книги.
            dynamic workbook = await get(() => workbooks.Open(path, ReadOnly: true), "Не удалось открыть рабочую книгу Excel.");

            //  Получение коллекции рабочих листов.
            dynamic sheets = await get(() => workbook.Sheets, "Не удалось получить коллеецию рабочих листов книги Excel.");

            //  Создание карты настроек.
            Dictionary<string, string?> settings = [];

            //  Получение листа с настройками.
            dynamic sheet = await get(() => sheets["Настройки"], "Не удалось получить лист с настройками книги Excel.");

            //  Получение коллекции ячеек.
            dynamic cells = await get(() => sheet.Cells, "Не удалось получить доступ к ячейкам листа настроек книги Excel.");

            //  Номер строки.
            int row = 2;

            //  Цикл чтения настроек.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение ячейки с именем.
                dynamic cell = await get(() => cells[row, 1], "Не удалось получить доступ к ячейке Excel");

                //  Получение имени.
                string? name = ((object?)cell.Value)?.ToString();

                //  Получение ячейки со значением.
                cell = await get(() => cells[row, 2], "Не удалось получить доступ к ячейке Excel");

                //  Получение значения.
                string? value = ((object?)cell.Value)?.ToString();

                //  Проверка значений.
                if (string.IsNullOrWhiteSpace(name))
                {
                    //  Завершение разбора настроек.
                    break;
                }

                //  Добавление в карту.
                settings.Add(name.Trim(), value);

                //  Переход к следующей строке.
                ++row;
            }

            //  Определение корневого каталога.
            if (!settings.TryGetValue("Корневой каталог", out string? rootPath) || rootPath is null)
                throw new InvalidOperationException($"В настройках книги Excel не найден корневой путь.");

            //  Нормализация корневого пути.
            rootPath = rootPath.Trim();

            //  Проверка корневого пути.
            if (!Directory.Exists(rootPath))
                throw new InvalidOperationException($"Корневой путь \"{rootPath}\" не существует.");

            //  Получение списка файлов.
            FileInfo[] allFiles = new DirectoryInfo(rootPath).GetFiles("*", SearchOption.AllDirectories);

            //  Определение организации.
            if (!settings.TryGetValue("Организация", out string? organization) || organization is null)
                throw new InvalidOperationException($"В настройках книги Excel не найдена организация.");

            //  Нормализация организации.
            organization = organization.Trim();

            //  Проверка организации.
            if (organization.Length == 0)
                throw new InvalidOperationException($"В настройках книги Excel не задана организация.");

            //  Определение постфикса.
            if (!settings.TryGetValue("Постфикс УЛ", out string? postfix) || postfix    is null)
                throw new InvalidOperationException($"В настройках книги Excel не найден постфикс УЛ.");

            //  Проверка постфикса.
            if (postfix.Length == 0)
                throw new InvalidOperationException($"В настройках книги Excel не задан постфикс УЛ.");

            //  Определение режима сохранения.
            if (!settings.TryGetValue("Сохранять Word", out string? isWordText) || isWordText is null)
                throw new InvalidOperationException($"В настройках книги Excel не найден режим сохранения в Word.");

            //  Нормализация режима сохранения.
            bool isWord = isWordText.Trim().Equals("да", StringComparison.CurrentCultureIgnoreCase);

            //  Создание карты шаблонов.
            Dictionary<string, string> patterns = [];

            //  Получение листа с шаблонами.
            sheet = await get(() => sheets["Шаблоны"], "Не удалось получить лист с шаблонами книги Excel.");

            //  Получение коллекции ячеек.
            cells = await get(() => sheet.Cells, "Не удалось получить доступ к ячейкам листа с шаблонами книги Excel.");

            //  Номер строки.
            row = 2;

            //  Цикл чтения настроек.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение ячейки с именем.
                dynamic cell = await get(() => cells[row, 1], "Не удалось получить доступ к ячейке Excel");

                //  Получение имени.
                string? name = ((object?)cell.Value)?.ToString();

                //  Получение ячейки со значением.
                cell = await get(() => cells[row, 2], "Не удалось получить доступ к ячейке Excel");

                //  Получение значения.
                string? value = ((object?)cell.Value)?.ToString();

                //  Проверка значений.
                if (string.IsNullOrWhiteSpace(name))
                {
                    //  Завершение разбора шаблонов.
                    break;
                }

                //  Корректировка имени.
                name = name.Trim();

                //  Проверка значения.
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException($"Указан пустой путь к шаблону \"{name}\".");

                //  Корректировка пути.
                value = value.Trim();

                //  Проверка файла.
                if (!File.Exists(value))
                    throw new InvalidOperationException($"Не найден шаблон: файл \"{value}\" не существует.");

                //  Добавление в карту.
                patterns.Add(name.Trim(), value);

                //  Переход к следующей строке.
                ++row;
            }

            //  Получение листа с документами.
            sheet = await get(() => sheets["Документы"], "Не удалось получить лист с документами книги Excel.");

            //  Получение коллекции ячеек.
            cells = await get(() => sheet.Cells, "Не удалось получить доступ к ячейкам листа с документами книги Excel.");

            //  Номер строки.
            row = 2;

            //  Цикл чтения настроек.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение ячейки с именем.
                dynamic cell = await get(() => cells[row, 2], "Не удалось получить доступ к ячейке Excel");

                //  Получение имени файла.
                string? fileName = ((object?)cell.Value)?.ToString();

                //  Проверка значений.
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    //  Завершение разбора документов.
                    break;
                }

                //  Получение значений.
                string? version = ((object?)(await get(() => cells[row, 3], "Не удалось получить доступ к ячейке Excel")).Value)?.ToString();
                string? documentType = ((object?)(await get(() => cells[row, 4], "Не удалось получить доступ к ячейке Excel")).Value)?.ToString();
                string? pattern = ((object?)(await get(() => cells[row, 5], "Не удалось получить доступ к ячейке Excel")).Value)?.ToString();
                string? productName = ((object?)(await get(() => cells[row, 6], "Не удалось получить доступ к ячейке Excel")).Value)?.ToString();
                string? productDesignation = ((object?)(await get(() => cells[row, 7], "Не удалось получить доступ к ячейке Excel")).Value)?.ToString();

                //  Создание удостоверяющего листа.
                Certificate certificate = new(allFiles, organization, postfix, isWord, patterns, fileName, version, documentType, pattern, productName, productDesignation);

                //  Выполнение в основном потоке.
                await Heart.Invoker(delegate
                {
                    //  Добавление удостоверияющего листа.
                    Heart.Certificates.Add(certificate);
                });

                //  Переход к следующей строке.
                ++row;
            }

            //  Закрытие книги.
            workbook.Close(false);
        }
        finally
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Закрытие приложения.
                application.Quit();
            }
            catch { }

            //  Извлечение объектов из стека.
            while (stack.TryPop(out dynamic? obj))
            {
                //  Проверка объекта.
                if (obj is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(obj);
                    }
                    catch { }
                }
            }

            //  Сборка мусора.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        //  Получает объект.
        async Task<dynamic> get(Func<dynamic> action, string message)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Результат.
            dynamic result = null!;

            //  Блок перехвата исключений.
            try
            {
                //  Выполнение действия.
                result = action();
            }
            catch (Exception ex)
            {
                //  Выброс исключения.
                throw new InvalidOperationException(message, ex);
            }

            //  Проверка объекта.
            if (Marshal.IsComObject(result))
            {
                //  Добавление в стек.
                stack.Push(result);

                //  Добавление в глобальный стек.
                Heart.Attach(result);

                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(result);
                    }
                    catch { }
                }
            }

            //  Возврат проверенного объекта.
            return result;
        }
    }

    /// <summary>
    /// Асинхронно проверяет токен отмены.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, проверяющая токен отмены.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async ValueTask IsNotCancelledAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        if (cancellationToken.IsCancellationRequested)
        {
            //  Выброс исключения.
            throw new OperationCanceledException("Операция отменена.");
        }

        //  Ожидание завершённой задачи.
        await ValueTask.CompletedTask.ConfigureAwait(false);
    }
}
