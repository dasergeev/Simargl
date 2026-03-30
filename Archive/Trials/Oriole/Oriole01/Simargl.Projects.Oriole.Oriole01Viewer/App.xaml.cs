using System.Configuration;
using System.Data;
using System.Windows;

namespace Simargl.Projects.Oriole.Oriole01Viewer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App :
    System.Windows.Application
{
    public App()
    {
        CancellationTokenSource source = new();
        CancellationToken = source.Token;
        Exit += (sender, e) => source.Dispose();
    }

    public CancellationToken CancellationToken { get; }
}
