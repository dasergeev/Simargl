using System.Windows.Input;

namespace Apeiron.Services.GlobalIdentity.Commands.Base;

/// <summary>
/// Базовый класс реализующий интерфейс ICommand.
/// </summary>
internal abstract class CommandBase : ICommand
{
    /// <summary>
    /// Событие, генерируется когда CanExecute изменяет значение.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Выясняет может ли быть выполнена комманда.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>True если команда может выполниться иначе ложь.</returns>
    public abstract bool CanExecute(object? parameter);

    /// <summary>
    /// Выполняет комманду.
    /// </summary>
    /// <param name="parameter"></param>
    public abstract void Execute(object? parameter);
}

