using Apeiron.Oriole.Analysis.Projects;
using Ookii.Dialogs.Wpf;
using System.Windows;
using System.Windows.Input;

namespace Apeiron.Oriole.Analysis.UserInterface;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
public partial class AnalyzerWindow :
    Window
{
    /// <summary>
    /// Свойство зависимости проекта.
    /// </summary>
    public static readonly DependencyProperty ProjectProperty =
        DependencyProperty.RegisterAttached(
            nameof(Project), typeof(Project), typeof(AnalyzerWindow));

    //new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public AnalyzerWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //Project = new(Dispatcher, @"\\railtest.ru\Data\06-НТО\RawData\Oriole\2021\0002");
        //ApplicationCommands.S

        _ProjectView.AnalyzerWindow = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        _NodeView.TreeView_SelectedItemChanged(sender, e);
    }

    /// <summary>
    /// Возвращает текущий проект.
    /// </summary>
    public Project? Project
    { 
        get => GetValue(ProjectProperty) as Project;
        set
        {
            _ProjectView.TreeView.ItemsSource = value?.Nodes;
            SetValue(ProjectProperty, value);
        }
    }

    private void New_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        VistaFolderBrowserDialog dialog = new();
        if (dialog.ShowDialog() == true)
        {
            Project = new(Dispatcher, dialog.SelectedPath);
        }


        //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
        //ofd.IsF

    }

    private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {

    }

    private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
    {

    }

    private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
    }

    private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
    }
}

/// <summary>
/// 
/// </summary>
public class WindowCommands
{
    static WindowCommands()
    {
        Exit = new RoutedCommand("Exit", typeof(AnalyzerWindow));
    }
    /// <summary>
    /// 
    /// </summary>
    public static RoutedCommand Exit { get; set; }
}