using Simargl.Concurrent;
using Simargl.Embedded.Tunings.TuningsEditor.Main;

namespace Simargl.Embedded.Tunings.TuningsEditor.Core.Code;

/// <summary>
/// Представляет генератор кода.
/// </summary>
/// <param name="heart">
/// Сердце приложения.
/// </param>
/// <param name="finalizer">
/// Метод завершающий работу.
/// </param>
/// <param name="indentText">
/// Текст отступа.
/// </param>
/// <param name="token">
/// Токен отмены.
/// </param>
public abstract class CodeGenerator(Heart heart, AsyncAction<CodeGenerator> finalizer,
    string indentText, CancellationToken token)
{
    /// <summary>
    /// Возвращает исходный код.
    /// </summary>
    public string? SourceCode { get; private set; } = null;

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    protected Heart Heart { get; } = heart;

    /// <summary>
    /// Возвращает построитель текста.
    /// </summary>
    protected StringBuilder Builder { get; } = new();

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    protected ILogger Logger { get; private set; } = null!;

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    protected CancellationToken CancellationToken { get; private set; }

    /// <summary>
    /// Возвращает проект.
    /// </summary>
    protected Project Project { get; private set; } = null!;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    public async Task InvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Создание источника связанного токена отмены.
        using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            token, cancellationToken);

        //  Замена токена отмены.
        cancellationToken = tokenSource.Token;

        //  Блок с гарантированным завершением.
        try
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Вывод информации в журнал.
                logger.LogInformation("Начало работы.");

                //  Проверка проекта.
                if (Heart.Project is Project project)
                {
                    //  Установка свойств.
                    Logger = logger;
                    CancellationToken = cancellationToken;
                    Project = project;

                    //  Выполнение основной работы.
                    await InvokeAsync().ConfigureAwait(false);
                }
                else
                {
                    //  Вывод информации в журнал.
                    logger.LogError("Не найден проект.");
                }

                //  Установка исходного текста.
                SourceCode = Builder.ToString();

                //  Вывод информации в журнал.
                logger.LogInformation("Завершение работы.");
            }
            catch
            {
                //  Проверка токена отмены.
                if (!token.IsCancellationRequested)
                {
                    //  Повторный выброс исключения.
                    throw;
                }

                //  Вывод информации в журнал.
                logger.LogInformation("Операция отменена.");
            }
        }
        finally
        {
            //  Завершение работы.
            await finalizer(this, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    protected abstract Task InvokeAsync();

    /// <summary>
    /// Поле для хранения текущего отступа.
    /// </summary>
    private int _Indent;

    /// <summary>
    /// Поле для хранения флага новой строки.
    /// </summary>
    private bool _IsNewLine = true;

    /// <summary>
    /// Выполняет запись символа.
    /// </summary>
    /// <param name="c">
    /// Символ, который требуется записать.
    /// </param>
    private void Write(char c)
    {
        //  Проверка символа.
        switch (c)
        {
            case '\r':
                //  Завершение разбора.
                break;
            case '\n':
                //  Переход на новую строку.
                Builder.AppendLine();

                //  Установка флага новой строки.
                _IsNewLine = true;

                //  Завершение разбора.
                break;
            default:
                //  Проверка флага новой строки.
                if (_IsNewLine)
                {
                    //  Цикл по отступам.
                    for (int i = 0; i < _Indent; i++)
                    {
                        //  Добавление отступа.
                        Builder.Append(indentText);
                    }

                    //  Сброс флага новой строки.
                    _IsNewLine = false;
                }

                //  Добавление символа.
                Builder.Append(c);

                //  Завершение разбора.
                break;
        }
    }

    /// <summary>
    /// Асинхронно увеличивает отступ.
    /// </summary>
    /// <returns>
    /// Задача, увеличивающая отступ.
    /// </returns>
    public async Task UpIndentAsync()
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(CancellationToken).ConfigureAwait(false);

        //  Увеличение отступа.
        ++_Indent;
    }

    /// <summary>
    /// Асинхронно уменьшает отступ.
    /// </summary>
    /// <returns>
    /// Задача, уменьшающая отступ.
    /// </returns>
    public async Task DownIndentAsync()
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(CancellationToken).ConfigureAwait(false);

        //  Уменьшение отступа.
        --_Indent;

        //  Проверка отступа.
        if (_Indent < 0)
        {
            //  Нормализация отступа.
            _Indent = 0;
        }
    }

    /// <summary>
    /// Асинхронно выполняет запись текста.
    /// </summary>
    /// <param name="value">
    /// Текст для записи.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись текста.
    /// </returns>
    protected async Task WriteAsync(string value)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(CancellationToken).ConfigureAwait(false);

        //  Перебор символов.
        foreach (char c in value)
        {
            //  Запись символа.
            Write(c);
        }
    }

    /// <summary>
    /// Асинхронно выполняет перевод каретки.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая перевод каретки.
    /// </returns>
    protected async Task WriteLineAsync()
    {
        //  Запись символа перевода каретки.
        await WriteAsync("\n").ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет запись текста с переводом каретки.
    /// </summary>
    /// <param name="value">
    /// Текст для записи.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись текста с переводом каретки.
    /// </returns>
    protected async Task WriteLineAsync(string value)
    {
        //  Запись текста.
        await WriteAsync(value).ConfigureAwait(false);

        //  Перевод каретки.
        await WriteLineAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Экранирует спецсимволы XML.
    /// Недопустимые для XML 1.0 управляющие символы отбрасываются.
    /// </summary>
    public static string EscapeXml(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var sb = new StringBuilder(value.Length);
        foreach (var ch in value)
        {
            switch (ch)
            {
                case '&': sb.Append("&amp;"); break;
                case '<': sb.Append("&lt;"); break;
                case '>': sb.Append("&gt;"); break;
                case '"': sb.Append("&quot;"); break;
                case '\'': sb.Append("&apos;"); break;
                default:
                    // Разрешённые диапазоны XML 1.0: таб, LF, CR, 0x20..0xD7FF, 0xE000..0xFFFD, 0x10000..0x10FFFF
                    if (ch == 0x9 || ch == 0xA || ch == 0xD ||
                        (ch >= 0x20 && ch <= 0xD7FF) ||
                        (ch >= 0xE000 && ch <= 0xFFFD))
                    {
                        sb.Append(ch);
                    }
                    // Символы вне допустимых диапазонов просто пропускаем.
                    break;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Асинхронно выполняет запись блока документации с описанием.
    /// </summary>
    /// <param name="value">
    /// Описание.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись.
    /// </returns>
    protected async Task WriteSummary(string value)
    {
        //  Запись текста.
        await WriteLineAsync("/// <summary>").ConfigureAwait(false);
        await WriteLineAsync($"/// {EscapeXml(value)}").ConfigureAwait(false);
        await WriteLineAsync("/// </summary>").ConfigureAwait(false);
    }
}
