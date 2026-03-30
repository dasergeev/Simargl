using Apeiron.Oriole.Analysis.Projects;
using System.IO;
using System.Windows.Controls;

namespace Apeiron.Oriole.Analysis.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для PackageFileView.xaml
    /// </summary>
    public partial class PackageFileView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public PackageFileView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public void SetNode(PackageFile file)
        {
            file.Load();
            byte[] data = File.ReadAllBytes(file.Path);
            _PackagesView.ItemsSource = file.Nodes;
            _BinaryView.Load(data);
        }
    }
}
