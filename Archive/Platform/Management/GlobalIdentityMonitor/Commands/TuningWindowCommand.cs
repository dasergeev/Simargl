using Apeiron.Services.GlobalIdentity.Commands.Base;

namespace Apeiron.Services.GlobalIdentity.Commands;

/// <summary>
/// Команда для отображения диалогового окна для работы с настройками.
/// </summary>
internal class TuningWindowCommand : CommandBase
{
    private TuningWindow? _Window;

    /// <summary>
    /// Возможно выполнять, только если окно не задано
    /// </summary>
    /// <param name="parameter">Параметр</param>
    /// <returns>True если команду можно выполнять, иначе false.</returns>
    public override bool CanExecute(object? parameter) => _Window == null;

    /// <summary>
    /// Выполнение команды открытия модального окна.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    public override void Execute(object? parameter)
    {
        var window = new TuningWindow()
        {
            // Установка родительского окна.
            Owner = Application.Current.MainWindow                
        };

        _Window = window;

        // Подписываемся на событие закрытия окна.
        window.Closed += Window_Closed;
        
        window.ShowDialog();
    }

    /// <summary>
    /// Обработчик события закрытия окна.
    /// </summary>
    /// <param name="sender">Объект вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void Window_Closed(object? sender, EventArgs e)
    {
        if (sender != null)
        {
            // Отписываемся от события.
            ((Window)sender).Closed -= Window_Closed;
            _Window = null;
        }
    }
}

