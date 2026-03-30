namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет основной элемент управления.
/// </summary>
public partial class MainControl
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
    public MainControl()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка текущего диалога.
        _CurrentDialog = null;

        _MessageSender.MainControl = this;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ScrollToLast()
    {
        _DialogView.ScrollToLast();
    }

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

        //  Установка значения дочерним элементам.
        _DialogView.CurrentDialog = _CurrentDialog;
        _MessageSender.CurrentDialog = _CurrentDialog;
    }
}
