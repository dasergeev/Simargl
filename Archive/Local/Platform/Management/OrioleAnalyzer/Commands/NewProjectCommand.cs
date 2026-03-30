namespace Apeiron.Oriole.Analysis.Commands;

///// <summary>
///// Представляет команду, создающую новый проект.
///// </summary>
//public class NewProjectCommand :
//    ICommand
//{
//    /// <summary>
//    /// Происходит при изменениях, влияющих на то, должна выполняться данная команда или нет.
//    /// </summary>
//    public event EventHandler? CanExecuteChanged;

//    /// <summary>
//    /// Определяет, может ли данная команда выполняться в ее текущем состоянии.
//    /// </summary>
//    /// <param name="parameter">
//    /// Данные, используемые данной командой.
//    /// </param>
//    /// <returns>
//    /// Значение <c>true</c>, если эту команду можно выполнить;
//    /// в противном случае - значение <c>false</c>.
//    /// </returns>
//    public bool CanExecute(object? parameter)
//    {
//        _ = parameter;

//        return true;
//    }

//    /// <summary>
//    /// Метод, вызываемый при вызове данной команды.
//    /// </summary>
//    /// <param name="parameter">
//    /// Данные, используемые данной командой.
//    /// </param>
//    public void Execute(object? parameter)
//    {
//        _ = parameter;
//    }
//}
