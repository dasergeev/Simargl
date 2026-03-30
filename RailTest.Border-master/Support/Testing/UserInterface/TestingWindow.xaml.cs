using RailTest.Compute.Cuda;
using RailTest.Exporting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RailTest.Border.Support.Testing.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для TestingWindow.xaml
    /// </summary>
    public unsafe partial class TestingWindow : Window
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public TestingWindow()
        {
            InitializeComponent();
        }

        private void WriteLine(string value)
        {
            _Paragraph.Inlines.Add(value + "\n");
        }

        private delegate CudaError cudaGetDeviceCountHandler(int* count);

        private unsafe void TestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                CudaRuntime cudaRuntime = new CudaRuntime();
                WriteLine($"Количество устройств: {cudaRuntime.GetDeviceCount()}");
                CudaDeviceProperties properties = cudaRuntime.GetDeviceProperties(0);
                WriteLine($"Имя устройства: {properties.Name}");

                int count = 134217728;
                double[] values = new double[count];
                var pointer = cudaRuntime.Malloc<double>(count);
                fixed (double* source = values)
                {
                    cudaRuntime.Memcpy(pointer, source, count, CudaMemcpyKind.HostToDevice);
                }

                WriteLine("Память выделена.");
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }
    }
}
