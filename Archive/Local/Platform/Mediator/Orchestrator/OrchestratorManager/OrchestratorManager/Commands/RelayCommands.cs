using Apeiron.Platform.OrchestratorManager.Commands.Base;
using System;

namespace Apeiron.Platform.OrchestratorManager.Commands;

/// <summary>
/// Реализует методы для выполнения команд.
/// </summary>
internal sealed class RelayCommand : CommandBase
{
    private readonly Action<object?> _Execute;
    private readonly Func<object?, bool>? _CanExecute;

    /// <summary>
    /// Инициализирует класс.
    /// </summary>
    public RelayCommand(Action<object?> Execute, Func<object?, bool>? CanExecute = null)
    {
        _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
        _CanExecute = CanExecute;
    }

    /// <summary>
    /// Возможность выполнения команды.
    /// </summary>
    /// <param name="parameter">Параметр переданный из разметки.</param>
    /// <returns>Возвращает True, если команда может быть выполнена, иначе False.</returns>
    public override bool CanExecute(object? parameter)
    {
        if (parameter is not null)
            return _CanExecute?.Invoke(parameter) ?? true;
        else
            return _CanExecute?.Invoke(null) ?? true;
    }

    /// <summary>
    /// Выполняет комманду.
    /// </summary>
    /// <param name="parameter">Параметр переданный из разметки.</param>
    public override void Execute(object? parameter)
    {
        if (parameter is not null)
        {
            // Выполняет переданныную команду.
            _Execute(parameter);
        }
        else
        {
            _Execute(null);
        }
    }
}
