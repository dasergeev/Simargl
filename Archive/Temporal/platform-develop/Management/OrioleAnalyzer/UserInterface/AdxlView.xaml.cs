using Apeiron.Recording.Adxl357;
using RailTest.TwoSection.DataViewer;
using System.Windows.Controls;

namespace Apeiron.Oriole.Analysis.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AdxlView.xaml
    /// </summary>
    public partial class AdxlView : UserControl
    {
        private readonly GraphicView _GraphicView;

        /// <summary>
        /// 
        /// </summary>
        public AdxlView()
        {
            InitializeComponent();

            _GraphicView = new();

            _Host.Child = _GraphicView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="sampling"></param>
        public void Load(Adxl357DataPackage channel, int sampling)
        {
            _GraphicView.Load(channel, sampling);
            _GraphicView.Invalidate();
        }

    }
}
