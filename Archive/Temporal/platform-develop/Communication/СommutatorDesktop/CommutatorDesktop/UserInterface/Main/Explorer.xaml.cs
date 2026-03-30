namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет обозреватель.
/// </summary>
public partial class Explorer
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
    public Explorer()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка текущего диалога.
        _CurrentDialog = null;

        //  Установка источника данных.
        _ListView.ItemsSource = Communicator.Dialogs;
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
    }

    /// <summary>
    /// Обрабатывает событие изменения выбранного элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //  Проверка выбранного диалога.
        if (sender is ListView listView && listView.SelectedItem is Dialog dialog)
        {
            //  Установка нового значения.
            CurrentDialog = dialog;
        }
        else
        {
            //  Сброс текущего значения.
            CurrentDialog = null;
        }
    }
}
