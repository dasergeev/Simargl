namespace Apeiron.Platform.Communication.СommutatorDesktop.Logging;

/// <summary>
/// Представляет сообщение журнала.
/// </summary>
public sealed class LogMessage :
    Active
{
    /// <summary>
    /// Поле для хранения текста сообщения.
    /// </summary>
    private string _Text;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public LogMessage() :
        this(string.Empty)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    public LogMessage(string text)
    {
        //  Установка текста сообщения.
        _Text = IsNotNull(text, nameof(text));

        //  Установка времени создания сообщения.
        Time = DateTime.Now;

        //  Установка времени жизни сообщения.
        LifeDuration = Setting.LogMessageLifeDuration;
    }

    /// <summary>
    /// Возвращает само сообщение.
    /// </summary>
    public LogMessage Self => this;

    /// <summary>
    /// Возвращает время создания сообщения.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает время жизни сообщения.
    /// </summary>
    public TimeSpan LifeDuration { get; }

    /// <summary>
    /// Возвращает или задаёт текст сообщения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Text
    {
        get => _Text;
        set
        {
            //  Проверка значения.
            IsNotNull(value, nameof(Text));

            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_Text != value)
                {
                    //  Изменение значения.
                    _Text = value;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(Text)));
                }
            });
        }
    }
}
