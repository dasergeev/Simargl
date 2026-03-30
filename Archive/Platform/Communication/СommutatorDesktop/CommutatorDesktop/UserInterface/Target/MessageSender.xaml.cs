namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет элемент управления, отправляющий сообщение.
/// </summary>
public partial class MessageSender
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="CurrentDialog"/>.
    /// </summary>
    public event EventHandler? CurrentDialogChanged;

    /// <summary>
    /// Поле для хранения текущего диалога.
    /// </summary>
    private Dialog? _CurrentDialog;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MessageSender()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка текущего диалога.
        _CurrentDialog = null;

        //  Настройка активности элементов.
        _Button.IsEnabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public MainControl? MainControl { get; set; }

    /// <summary>
    /// Возвращает или задаёт текущий диалог.
    /// </summary>
    public Dialog? CurrentDialog
    {
        get => _CurrentDialog;
        set
        {
            //  Проверка необходимости изменения значения.
            if (!ReferenceEquals(_CurrentDialog, value))
            {
                //  Установка нового значения.
                _CurrentDialog = value;

                //  Вызов события об изменении значения свойства.
                OnCurrentDialogChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="CurrentDialogChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnCurrentDialogChanged(EventArgs e)
    {
        //  Вызов события.
        CurrentDialogChanged?.Invoke(this, e);

        //  Настройка активности элементов.
        _Button.IsEnabled = CurrentDialog is not null;
    }

    /// <summary>
    /// Обрабатывает событие отправки сообщения.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //  Получение текста сообщения.
        string text = _TextBox.Text.Trim();

        //  Получение диалога.
        if (CurrentDialog is Dialog dialog)
        {
            //  Сброс текстового поля.
            _TextBox.Text = string.Empty;

            //  Асинхронная отправка сообщения.
            _ = Task.Run(async delegate
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(CancellationToken).ConfigureAwait(false);

                //  Блок перехвата всех некритических исключений.
                try
                {
                    //  Отправка сообщения.
                    await dialog.SendMessageAsync(text, CancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //  Проверка критического исключения.
                    if (ex.IsCritical())
                    {
                        //  Повторный выброс исключения.
                        throw;
                    }

                    //  Добавление информации в журнал.
                    Logger.Log("Не удалось отправить сообщение:", ex);
                }

                //  Прокрутка.
                Dispatcher.Invoke(delegate
                {
                    MainControl?.ScrollToLast();
                });

            }, CancellationToken);
        }
    }
}
