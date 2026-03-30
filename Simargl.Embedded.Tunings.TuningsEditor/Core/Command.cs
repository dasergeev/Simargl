using System.Windows.Input;

namespace Simargl.Embedded.Tunings.TuningsEditor.Core;

/// <summary>
/// Представляет команду.
/// </summary>
/// <param name="isEnabled">
/// Значение, определяющее включена ли команда.
/// </param>
/// <param name="action">
/// Действие, выполняемое командой.
/// </param>
public sealed class Command(bool isEnabled, Action action) :
    ICommand
{
    /// <summary>
    /// Вызывается при изменении возможности выполнения команды.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Поле для хранения значения, определяющего включена ли команда.
    /// </summary>
    private bool _IsEnabled = isEnabled;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее включена ли команда.
    /// </summary>
    public bool IsEnabled
    {
        get => _IsEnabled;
        set
        {
            //  Проверка изменения значения.
            if (_IsEnabled != value)
            {
                //  Установка нового значения.
                _IsEnabled = value;

                //  Вызов события об изменении состояния.
                Volatile.Read(ref CanExecuteChanged)?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Определяет, может ли команда выполняться в текущем состоянии.
    /// </summary>
    /// <param name="parameter">
    /// Дополнительный параметр команды (может быть null).
    /// </param>
    /// <returns>
    /// Значение true или false в зависимости от условий.
    /// </returns>
    public bool CanExecute(object? parameter) => _IsEnabled;

    /// <summary>
    /// Выполняет действие команды.
    /// Вызывается, когда пользователь нажал кнопку или вызвал команду.
    /// </summary>
    /// <param name="parameter">
    /// Дополнительный параметр команды (может быть null).
    /// </param>
    public void Execute(object? parameter)
    {
        //  Выполнение действия.
        action();
    }
}
