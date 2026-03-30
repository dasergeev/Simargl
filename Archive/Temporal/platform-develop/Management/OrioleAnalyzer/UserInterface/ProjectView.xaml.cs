using Apeiron.Oriole.Analysis.Projects;
using System.Windows;
using System.Windows.Controls;

namespace Apeiron.Oriole.Analysis.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий проект.
/// </summary>
public partial class ProjectView :
    UserControl
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ProjectView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    public AnalyzerWindow? AnalyzerWindow { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TreeView TreeView => _TreeView;

    private void TreeView_Expanded(object sender, RoutedEventArgs e)
    {
        //  Проверка узла.
        if (e.OriginalSource is TreeViewItem item && item.Header is Node node)
        {
            //  Загрузка узла.
            node.Load();
        }

        //TreeViewItem? item = e.OriginalSource as TreeViewItem;
        //_ = item;
        //if (item is not null)
        //{
        //    if ((item.Items.Count == 1) && (item.Items[0] is string))
        //    {
        //        item.Items.Clear();

        //        DirectoryInfo expandedDir = null;
        //        if (item.Tag is DriveInfo)
        //            expandedDir = (item.Tag as DriveInfo).RootDirectory;
        //        if (item.Tag is DirectoryInfo)
        //            expandedDir = (item.Tag as DirectoryInfo);
        //        try
        //        {
        //            foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
        //                item.Items.Add(CreateTreeItem(subDir));
        //        }
        //        catch { }
        //    }
        //}
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        AnalyzerWindow?.TreeView_SelectedItemChanged(sender, e);
    }
}
