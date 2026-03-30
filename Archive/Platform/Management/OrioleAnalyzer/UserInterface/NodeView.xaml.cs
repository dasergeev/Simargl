using Apeiron.Oriole.Analysis.Projects;
using System.Windows;
using System.Windows.Controls;

namespace Apeiron.Oriole.Analysis.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public NodeView()
        {
            InitializeComponent();

            _PackageFileView.Visibility = Visibility.Collapsed;
            _AdxlView.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //  Проверка узла.
            if (e.NewValue is Node node)
            {
                if (node is PackageFile file)
                {
                    _PackageFileView.Visibility = Visibility.Visible;
                    _PackageFileView.SetNode(file);
                }
                else
                {
                    _PackageFileView.Visibility = Visibility.Collapsed;
                }

                if (node is Package package)
                {
                    _AdxlView.Visibility = Visibility.Visible;
                    _AdxlView.Load(package.Data, 1000);
                }
                else
                {
                    _AdxlView.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
