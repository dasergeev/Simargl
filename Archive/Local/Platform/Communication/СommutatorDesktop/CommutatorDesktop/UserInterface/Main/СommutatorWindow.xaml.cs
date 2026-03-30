namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
public partial class СommutatorWindow :
    Window
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public СommutatorWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Добавление обработчика события изменения текущего диалога.
        _Explorer.CurrentDialogChanged += (sender, e) => _MainControl.CurrentDialog = _Explorer.CurrentDialog;
    }
}
