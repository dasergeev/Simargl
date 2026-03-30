namespace Simargl.Embedded.Tunings.TuningsEditor.Core.Code.Writers;

/// <summary>
/// Представляет средство записи кода.
/// </summary>
/// <param name="indentText">
/// Текст отступа.
/// </param>
public abstract class CodeWriter(string indentText)
{
    /// <summary>
    /// Поле для хранения средства записи текста.
    /// </summary>
    private readonly StringBuilder _Builder = new();

    /// <summary>
    /// Поле для хранения текущего отступа.
    /// </summary>
    private int _Indent = 0;

    /// <summary>
    /// Поле для хранения флага новой строки.
    /// </summary>
    private bool _IsNewLine = true;

    /// <summary>
    /// Возвращает текстовое представление кода.
    /// </summary>
    /// <returns>
    /// Текстовое представление кода.
    /// </returns>
    public override string ToString()
    {
        //  Возврат текстового представления средства записи текста.
        return _Builder.ToString();
    }

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
                _Builder.AppendLine();

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
                        _Builder.Append(indentText);
                    }

                    //  Сброс флага новой строки.
                    _IsNewLine = false;
                }

                //  Добавление символа.
                _Builder.Append(c);

                //  Завершение разбора.
                break;
        }
    }

    /// <summary>
    /// Увеличивает отступ.
    /// </summary>
    public void UpIndent()
    {
        //  Увеличение отступа.
        ++_Indent;
    }

    /// <summary>
    /// Уменьшает отступ.
    /// </summary>
    public void DownIndent()
    {
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
    /// Выполняет запись текста.
    /// </summary>
    /// <param name="value">
    /// Текст для записи.
    /// </param>
    public void Write(string value)
    {
        //  Перебор символов.
        foreach (char c in value)
        {
            //  Запись символа.
            Write(c);
        }
    }

    /// <summary>
    /// Выполняет перевод каретки.
    /// </summary>
    public void WriteLine()
    {
        //  Запись символа перевода каретки.
        Write("\n");
    }

    /// <summary>
    /// Выполняет запись текста с переводом каретки.
    /// </summary>
    /// <param name="value">
    /// Текст для записи.
    /// </param>
    public void WriteLine(string value)
    {
        //  Запись текста.
        Write(value);

        //  Перевод каретки.
        WriteLine();
    }

    /// <summary>
    /// Выполняет запись блока документации с описанием.
    /// </summary>
    /// <param name="value">
    /// Описание.
    /// </param>
    public void WriteSummary(string value)
    {
        //  Запись текста.
        WriteLine("/// <summary>");
        WriteLine($"/// {EscapeXml(value)}");
        WriteLine("/// </summary>");
    }

    /// <summary>
    /// Экранирует спецсимволы XML.
    /// Недопустимые для XML 1.0 управляющие символы отбрасываются.
    /// </summary>
    private static string EscapeXml(string? value)
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
                //case '"': sb.Append("&quot;"); break;
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
}
