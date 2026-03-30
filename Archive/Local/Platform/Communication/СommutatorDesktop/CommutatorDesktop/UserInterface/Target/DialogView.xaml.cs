using System.Windows.Controls;
using System.Windows.Threading;

namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающй диалог.
/// </summary>
public partial class DialogView
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
    public DialogView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка текущего диалога.
        _CurrentDialog = null;

        var timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
        };

        timer.Tick += (sender, e) =>
        {
            _ = Task.Run(async delegate
            {
                var dialog = _CurrentDialog;
                if (dialog is not null)
                {
                    await dialog.UpdateAsync(default, CancellationToken).ConfigureAwait(false);
                }
            });
        };

        timer.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ScrollToLast()
    {
        if (_ListView.Items.Count > 0)
        {
            _ListView.Focus();
            _ListView.SelectedIndex = _ListView.Items.Count - 1;
            _ListView.ScrollIntoView(_ListView.Items[^1]);
        }
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

        //  Установка источника списка.
        _ListView.ItemsSource = CurrentDialog?.Messages;

        ScrollToLast();
    }
}
