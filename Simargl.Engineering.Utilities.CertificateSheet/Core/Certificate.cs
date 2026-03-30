using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Simargl.Engineering.Utilities.CertificateSheet.Core;

/// <summary>
/// Представляет удостоверяющий лист.
/// </summary>
public sealed class Certificate(
        FileInfo[] allFiles, string organization, string postfix, bool isWord,
        Dictionary<string, string> patterns,
        string fileName, string? version, string? documentType,
        string? pattern, string? productName, string? productDesignation) :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private Visibility _ExceptionVisibility = Visibility.Visible;
    private string _ExceptionMessage = "Ожидание ...";
    private int _CountFiles = 0;

    /// <summary>
    /// 
    /// </summary>
    public int CountFiles
    {
        get => _CountFiles;
        set
        {
            if (_CountFiles != value)
            {
                _CountFiles = value;
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(CountFiles)));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Visibility ExceptionVisibility
    {
        get => _ExceptionVisibility;
        set
        {
            if (_ExceptionVisibility != value)
            {
                _ExceptionVisibility = value;
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(ExceptionVisibility)));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string ExceptionMessage
    {
        get => _ExceptionMessage;
        set
        {
            if (_ExceptionMessage != value)
            {
                _ExceptionMessage = value;
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(ExceptionMessage)));
            }
        }
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public static Heart Heart => ((App)Application.Current).Heart;

    /// <summary>
    /// Возвращает имя файла.
    /// </summary>
    public string FileName => fileName;

    /// <summary>
    /// Асинхронно выполняет работу с удостоверяющим листом.
    /// </summary>
    /// <param name="application">
    /// Приложение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с удостоверяющим листом.
    /// </returns>
    public async Task InvokeAsync(dynamic application, CancellationToken cancellationToken)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Создание стека объектов.
            Stack<dynamic> stack = [];

            //  Блок с гарантированным завершением.
            try
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Проверка входных данных.
                    if (string.IsNullOrWhiteSpace(productName))
                        throw new InvalidOperationException("Пустое значение наименования изделия.");
                    if (string.IsNullOrWhiteSpace(productDesignation))
                        throw new InvalidOperationException("Пустое значение обозначения изделия.");
                    if (string.IsNullOrWhiteSpace(documentType))
                        throw new InvalidOperationException("Пустое значение типа документа.");
                    if (string.IsNullOrWhiteSpace(version))
                        throw new InvalidOperationException("Пустое значение версии документа.");
                    if (string.IsNullOrWhiteSpace(organization))
                        throw new InvalidOperationException("Пустое значение организации.");
                    if (string.IsNullOrWhiteSpace(postfix))
                        throw new InvalidOperationException("Пустое значение постфикса.");

                    //  Получение целевого имени.
                    string targetFileName = fileName + postfix;

                    //  Получение файлов.
                    IEnumerable<FileInfo> files = allFiles
                        .Where(x => x.Name[..^x.Extension.Length] == fileName);

                    //  Проверка файлов.
                    if (!files.Any())
                        throw new InvalidOperationException($"Не найдены файлы с именем \"{fileName}\".");

                    //  Рабочий каталог.
                    DirectoryInfo? directoryInfo = null;

                    //  Перебор файлов.
                    foreach (FileInfo file in files)
                    {
                        //  Проверка рабочего каталога.
                        if (directoryInfo is null)
                        {
                            //  Установка рабочего каталога.
                            directoryInfo = file.Directory ??
                                throw new InvalidOperationException($"Не удалось определить каталог файла \"{file.FullName}\".");
                        }
                        else
                        {
                            //  Проверка рабочего каталога.
                            if (!directoryInfo.FullName.Equals((file.Directory ??
                                throw new InvalidOperationException($"Не удалось определить каталог файла \"{file.FullName}\".")).FullName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                throw new InvalidOperationException($"Несколько файлов \"{fileName}\" находятся в разных каталогах: \"{directoryInfo.FullName}\", \"{file.Directory.FullName}\".");
                            }
                        }
                    }

                    //  Получение данных.
                    var data = files
                        .Select(x => new
                        {
                            Name = x.Name[..^x.Extension.Length],
                            x.Extension,
                            Size = $"{x.Length}",
                            Code = $"{Crc32.Compute(File.ReadAllBytes(x.FullName)):x8}",
                            Algorithm = "CRC32",
                        });

                    //  Определение даты изменения.
                    DateTime time = files.Max(x => x.LastWriteTime);

                    //  Проверка шаблона.
                    if (pattern is null || !patterns.TryGetValue(pattern, out string? patternPath) || patternPath is null)
                        throw new InvalidOperationException("Не найден шаблон");

                    //  Получение коллекции документов.
                    dynamic documents = await get(() => application.Documents, "Не удалось получить коллекцию документов Word.");

                    //  Открытие документа.
                    dynamic document = await get(() => documents.Add(Template: patternPath, NewTemplate:false, DocumentType:0, Visible: true), $"Не удалось получить создать документ Word на основе шаблона \"{patternPath}\".");

                    //  Получение таблиц.
                    dynamic tables = await get(() => document.Tables, $"Не удалось получить коллекцию таблиц.");

                    //  Получение первой таблицы
                    dynamic table = await get(() => tables[1], $"Не удалось получить таблицу.");

                    //  Заполнение первой таблицы.
                    await write(2, 2, productName);
                    await write(2, 3, productDesignation);
                    await write(2, 4, documentType);
                    await write(2, 5, version);
                    await write(2, 6, $"{time:dd.MM.yyyy}");

                    //  Получение второй таблицы
                    table = await get(() => tables[2], $"Не удалось получить таблицу.");

                    //  Индекс строки.
                    int row = 2;

                    //  Получение строк таблицы.
                    dynamic rows = await get(() => table.Rows, $"Не удалось получить коллекцию строк таблицы.");

                    //  Перебор данных.
                    foreach (var item in data)
                    {
                        //  Проверка индекса строки.
                        if (row != 2)
                        {
                            //  Добавление строки.
                            await invoke(() => rows.Add(), "Не удалось добавить строку в таблицу");
                        }

                        //  Заполнение строки таблицы.
                        await write(row, 2, item.Name);
                        await write(row, 3, item.Extension);
                        await write(row, 4, item.Size);
                        await write(row, 5, item.Code);
                        await write(row, 6, item.Algorithm);

                        //  Переход к следующей строке.
                        ++row;
                    }

                    //  Определение количества файлов.
                    int filesCount = row - 2;

                    //  Получение третьей таблицы
                    table = await get(() => tables[3], $"Не удалось получить таблицу.");

                    //  Заполнение третьей таблицы.
                    await write(1, 4, $"{time:dd.MM.yyyy}");
                    await write(2, 4, $"{time:dd.MM.yyyy}");
                    await write(3, 4, $"{time:dd.MM.yyyy}");
                    await write(4, 4, $"{time:dd.MM.yyyy}");

                    //  Получение разделов документа.
                    dynamic sections = await get(() => document.Sections, $"Не удалось получить разделы документа.");

                    //  Получение первого раздела документа.
                    dynamic section = await get(() => sections[1], $"Не удалось получить первый раздел документа.");

                    //  Получение колонтитулов.
                    dynamic footers = await get(() => section.Footers, $"Не удалось получить колонтитулы документа.");

                    //  Получение нижнего колонтитула.
                    dynamic footer = await get(() => footers[1], $"Не удалось получить нижний колонтитул документа.");

                    //  Получение диапазона колонтитула.
                    dynamic range = await get(() => footer.Range, $"Не удалось получить диапазон нижнего колонтитула документа.");

                    //  Получение таблиц колонтитула.
                    tables = await get(() => range.Tables, $"Не удалось получить коллекцию таблиц нижнего колонтитула документа.");

                    //  Получение третьей таблицы
                    table = await get(() => tables[1], $"Не удалось получить таблицу нижнего колонтитула.");

                    //  Заполнение таблицы.
                    await write(2, 1, organization);
                    await write(2, 2, targetFileName);

                    //  Формирование полного пути.
                    string targetPath = Path.Combine(directoryInfo!.FullName, targetFileName + ".pdf");

                    //  Выполнение в основном потоке.
                    await invoke(delegate
                    {
                        //  Экспорт.
                        document.ExportAsFixedFormat(OutputFileName: targetPath,
                            ExportFormat: 17, OpenAfterExport: false);
                    }, "Не удалось сохранить документ в формате .pdf");

                    //  Проверка необходимости сохранения в Word.
                    if (isWord)
                    {
                        //  Формирование полного пути.
                        targetPath = Path.Combine(directoryInfo!.FullName, targetFileName + ".docx");

                        //  Выполнение в основном потоке.
                        await invoke(delegate
                        {
                            //  Сохранение.
                            document.SaveAs2(FileName: targetPath);
                        }, "Не удалось сохранить документ в формате .docx");
                    }

                    //  Выполнение в основном потоке.
                    await Heart.Invoker(delegate
                    {
                        //  Установка количества файлов.
                        CountFiles = filesCount;
                    });

                    //  Записывает значение в ячейку таблицы.
                    async Task write(int row, int column, string value)
                    {
                        //  Получение ячейки.
                        dynamic cell = await get(() => table.Cell(row, column), $"Не удалось получить ячейку.");

                        //  Получение диапазона.
                        dynamic range = await get(() => cell.Range, $"Не удалось получить диапазон ячейки.");

                        //  Установка значения.
                        await invoke(() => range.Text = value, $"Не удалось пустановить текст ячейки.");
                    }
                }
                catch (Exception ex)
                {
                    //  Выполнение в основном потоке.
                    await Heart.Invoker(delegate
                    {
                        ExceptionVisibility = Visibility.Visible;
                        ExceptionMessage = cancellationToken.IsCancellationRequested ? "Отменено" : ex.Message;
                    }).ConfigureAwait(false);

                    //  Повторный выброс иключения.
                    throw;
                }

                //  Выполнение в основном потоке.
                await Heart.Invoker(delegate
                {
                    ExceptionVisibility = Visibility.Collapsed;
                }).ConfigureAwait(false);

            }
            finally
            {
                //  Извлечение объектов из стека.
                while (stack.TryPop(out dynamic? obj))
                {
                    //  Проверка объекта.
                    if (obj is not null)
                    {
                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Выполнение в основном потоке.
                            await Heart.Invoker(delegate
                            {
                                //  Освобождение объекта.
                                Marshal.ReleaseComObject(obj);
                            });
                        }
                        catch { }
                    }
                }

                //  Сборка мусора.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //  Выполняет действие в основном потоке.
            async Task invoke(Action action, string message)
            {
                //  Блок перехвата исключений.
                try
                {
                    //  Выполнение в основном потоке.
                    await Heart.Invoker(delegate
                    {
                        //  Выполнение действия.
                        action();
                    });
                }
                catch (Exception ex)
                {
                    //  Выброс исключения.
                    throw new InvalidOperationException(message, ex);
                }
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
                    //  Выполнение в основном потоке.
                    await Heart.Invoker(delegate
                    {
                        //  Выполнение действия.
                        result = action();
                    });
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
        catch (Exception ex)
        {
            _ = ex;
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
